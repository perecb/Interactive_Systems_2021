using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

public class MediapipeReceiver : MonoBehaviour
{

	public float multiplier; 

	//Thread to receive by UDP from the Phone 
	bool on = false;
	private Thread lThread;

	//UDP Receiver definitions
	private UdpClient client;
	private IPEndPoint remoteIpEndPoint;

	//Receiver port
	int port = 21900;

	// Hand landmarks positions
	// https://google.github.io/mediapipe/solutions/hands.html
	Vector2 Dedo1; //0
	Vector2 Dedo2; //1

	//Hand landmarks representations as GameObjects
	public GameObject Dedo1GO;
	public GameObject Dedo2GO;

	private void Start()
	{
		Listen();
	}

	public void Listen()
	{
		client = new UdpClient(port);
		remoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);

		lThread = new Thread(new ThreadStart(ListenUDPThread));
		lThread.Name = "Receiver UDP listen thread";
		lThread.Start();
	}

	private void ListenUDPThread()
	{
		on = true;      

		Debug.Log("Receiver: listening on port " + port);

		//Forces to use "." as decimal separator
		CultureInfo info = new CultureInfo("es-ES");
		info.NumberFormat.NumberDecimalSeparator = ".";
		Thread.CurrentThread.CurrentCulture = info;
		Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

		while (on)
		{
			try
			{
				byte[] packet = client.Receive(ref remoteIpEndPoint);

				// read message
				if (packet != null && packet.Length > 0)
				{
					string message = ExtractString(packet, 0, packet.Length);
					string[] messageSplit = message.Split(new Char[] { ',' });

					Dedo1 = new Vector2(float.Parse(messageSplit[0].ToString()),
										float.Parse(messageSplit[1].ToString()) * -1); //Reverse the Y axis

					Dedo2 = new Vector2(float.Parse(messageSplit[2].ToString()),
											float.Parse(messageSplit[3].ToString()) * -1); //Reverse the Y axis

					//Debug.Log("Receiver: "+message);
				}
				if (packet != null && packet.Length == 0)
				{
					Debug.Log("Received packet is empty");
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		}
	}

	public void Close()
	{
		on = false;
		lThread.Abort();
		if (client != null) client.Close();
	}

	private string ExtractString(byte[] packet, int start, int length)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < packet.Length; i++) sb.Append((char)packet[i]);
		return sb.ToString();
	}

	private void Update()
	{
		//Vector3 PosDedo1 = new Vector3(((Dedo1.x * 2) - 1) * multiplier, ((Dedo1.y * 2) + 1) * multiplier, -5);
		//Vector3 PosDedo2 = new Vector3(((Dedo2.x * 2) - 1) * multiplier, ((Dedo2.y * 2) + 1) * multiplier, -5);

		//Assign the positions received in the socket to the objects
		Dedo1GO.transform.position = new Vector3(((Dedo1.x * 2) - 1) * multiplier, ((Dedo1.y * 2) + 1) * multiplier, -5);
		Dedo2GO.transform.position = new Vector3(((Dedo2.x * 2) - 1) * multiplier, ((Dedo2.y * 2) + 1) * multiplier, -5);
	}

	private void OnDestroy()
	{
		Close();
	}
}
