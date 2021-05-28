using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    Animator animator;

    public GameObject startPlatform;
    public GameObject endPlatform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tpStart();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();

        if (transform.position.x > endPlatform.transform.position.x - 0.5f && transform.position.x < endPlatform.transform.position.x + 0.5f)
        {
            tpNextLvl();
        }
    }

    private void tpStart()
    {
        transform.position = new Vector3(startPlatform.transform.position.x, startPlatform.transform.position.y + 0.1f, 0);
    }

    private void tpNextLvl()
    {
        SceneManager.LoadScene("Lvl 2");
    }

    private void UpdateMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetBool("walk", true);
        transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 90, 0);

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
            //animator.SetBool("walk", false);
        }
    }
}
