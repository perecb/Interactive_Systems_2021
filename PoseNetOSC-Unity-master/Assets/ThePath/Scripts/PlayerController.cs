using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateMovement();
        CharacterController controller = GetComponent<CharacterController>();
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = movementSpeed * Input.GetAxis("Vertical");
        if (curSpeed > 0)
        {
            animator.SetInteger("walk", 1);
        }
        else
        {
            animator.SetInteger("walk", 0);
        }
        if (curSpeed >= 0)
        {
            controller.SimpleMove(forward * curSpeed);
        }
    }


    /*private void UpdateMovement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical"); // 1

        if (verticalInput < 0) // 1
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (verticalInput > 0) // 2
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }*/
}
