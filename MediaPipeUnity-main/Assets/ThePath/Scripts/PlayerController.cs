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

    public GameObject Flecha_derecha;
    public GameObject Flecha_izquierda;

    GameObject encendido_D;
    GameObject apagado_D;
    GameObject encendido_I;
    GameObject apagado_I;

    float respawn_Time = 1;
    float next_Respawn = 0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tpStart();

        encendido_D = Flecha_derecha.transform.GetChild(0).gameObject;
        apagado_D = Flecha_derecha.transform.GetChild(1).gameObject;
        encendido_I = Flecha_izquierda.transform.GetChild(0).gameObject;
        apagado_I = Flecha_izquierda.transform.GetChild(1).gameObject;

        apagado_D.SetActive(false);
        encendido_I.SetActive(false);
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

            encendido_D.SetActive(false);   encendido_D.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            apagado_D.SetActive(true);
            encendido_I.SetActive(true);    encendido_I.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            apagado_I.SetActive(false);
        }
        else if (transform.rotation.y > 0)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);

            encendido_D.SetActive(true);    encendido_D.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            apagado_D.SetActive(false);
            encendido_I.SetActive(false);   encendido_I.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            apagado_I.SetActive(true);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Estamos en ExampleCoroutine");
        gameObject.SetActive(false);
        Debug.Log("Objeto en falso");

        Debug.Log("Se empezara a esperar");
        yield return new WaitForSeconds(2);

        Debug.Log("Se ha esperado");
        tpStart();
        Debug.Log("Se ha ido al start");
        gameObject.SetActive(true);
        Debug.Log("Objeto en true");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spike")
        {
            tpStart();
        }
    }
}
