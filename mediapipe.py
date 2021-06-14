import socket
import cv2
import mediapipe as mp
mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands
mp_pose = mp.solutions.pose

UDP_IP = "127.0.0.1"
UDP_PORT = 21900

sock = socket.socket(socket.AF_INET, #Internet
                     socket.SOCK_DGRAM) #UDP

# For webcam input:
hands = mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5,max_num_hands=2)
pose = mp_pose.Pose( min_detection_confidence=0.5, min_tracking_confidence=0.5)
cap = cv2.VideoCapture(0)

while cap.isOpened():
  success, image = cap.read()
  if not success:
    print("Ignoring empty camera frame.")
    # If loading a video, use 'break' instead of 'continue'.
    continue

  # Flip the image horizontally for a later selfie-view display, and convert
  # the BGR image to RGB.
  image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
  # To improve performance, optionally mark the image as not writeable to
  # pass by reference.
  image.flags.writeable = False
  resultsHands = hands.process(image)
  resultsPose = pose.process(image)

  #Send landmarks using UDP
  detectionsHands = resultsHands.multi_hand_landmarks
  detectionsPose = resultsPose.pose_landmarks

  numHands = 0

  if detectionsHands is not None and detectionsPose is not None:

      msg = ""

      for detection in detectionsHands:
          numHands = numHands + 1
          msg+=str(round(detection.landmark[8].x,3))+','+str(round(detection.landmark[8].y,3))+','

      msg+=str(round(detectionsPose.landmark[0].x,3))+','

      if numHands == 2:
          sockmsg=bytearray(msg, 'utf-8')
          sock.sendto(sockmsg, (UDP_IP,UDP_PORT))
          #print(msg)

  # Draw the hand annotations on the image.
  image.flags.writeable = True
  image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
  if resultsHands.multi_hand_landmarks:
    for hand_landmarks in resultsHands.multi_hand_landmarks:
      mp_drawing.draw_landmarks(image, hand_landmarks, mp_hands.HAND_CONNECTIONS)

  if resultsPose.pose_landmarks:
    mp_drawing.draw_landmarks( image, resultsPose.pose_landmarks, mp_pose.POSE_CONNECTIONS)
  
  cv2.imshow('MediaPipe Hands & Pose', image)
  if cv2.waitKey(5) & 0xFF == 27:
    break
hands.close()
pose.close()
cap.release()
