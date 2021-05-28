using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBridge : MonoBehaviour
{
    public GameObject Dedo2;
    public GameObject Cubo;

    public bool active_bridge = false;

    Vector3 PosicionDedo1;
    Vector3 PosicionDedo2;
    Vector3 Centro;

    float Angulo_Radianes;
    float Angulo_Grados;
    float cont;
    float activatio_Rate = 1;
    float next_Activation = 0;

    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        Instanciar_puente();
        Cubo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(active_bridge == true)
        {
            if(cont == 0)
            {
                Cubo.SetActive(true);
                cont = 1;
            }
            Calc_PR();
        }

        if(active_bridge == false && cont == 1)
        {
            cont = 0;
            Cubo.SetActive(false);
        }
    }

    void Instanciar_puente()
    {
        Calc_PR();
        Cubo = Instantiate(Cubo, new Vector3(Centro.x, Centro.y, 0), Quaternion.identity, transform);
        Cubo.transform.parent = null;
    }

    void Calc_PR()
    {
        PosicionDedo1 = transform.position;
        PosicionDedo2 = Dedo2.transform.position;

        Centro = (PosicionDedo1 + PosicionDedo2) / 2;

        float hipotenusa = Mathf.Sqrt(Mathf.Pow(PosicionDedo2.x - PosicionDedo1.x, 2) + Mathf.Pow(PosicionDedo2.y - PosicionDedo1.y, 2));
        Angulo_Radianes = Mathf.Asin((PosicionDedo2.y - PosicionDedo1.y) / hipotenusa);
        Angulo_Grados = Angulo_Radianes * Mathf.Rad2Deg;

        Cubo.transform.rotation = Quaternion.Euler(0, 0, Angulo_Grados);
        Cubo.transform.position = new Vector3(Centro.x, Centro.y, 0);
        Cubo.transform.localScale = new Vector3(hipotenusa, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cont == 0 && Time.time > next_Activation)
        {
            active_bridge = true;
            next_Activation = Time.time + activatio_Rate;
        }
        if (cont == 1 && Time.time > next_Activation)
        {
            active_bridge = false;
            next_Activation = Time.time + activatio_Rate;
        }
    }
}
