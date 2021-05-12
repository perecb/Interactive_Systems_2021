using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
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
        UpdateMovement();
    }


    private void UpdateMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput < 0)
        {
            animator.SetBool("walk", true);
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,-90,0);
        }
        else if (verticalInput > 0)
        {
            animator.SetBool("walk", true);
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }
}
