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
    public string nextScene;

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
        SceneManager.LoadScene(nextScene);
    }

    private void UpdateMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float verticalInput = Input.GetAxisRaw("Vertical");
        animator.SetBool("walk", true);

        if (transform.rotation.y < 0)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
        else if (transform.rotation.y > 0)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
    }
}
