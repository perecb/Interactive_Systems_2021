using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject Dedo1GO;
    public GameObject Dedo2GO;
    public GameObject PlayButton;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside(Dedo1GO.transform.position)  && isInside(Dedo2GO.transform.position))
        {
            //Debug.Log("Dedos dentro");
            tpNextLvl();
        }
    }

    private bool isInside(Vector3 position)
    {
        Vector3 playPos = PlayButton.transform.position;

        if ( (position.x < playPos.x + 12 && position.x > playPos.x - 12) && (position.y < playPos.y + 15 && position.y > playPos.y + 8))
        {
            return true;
        }

        return false;
    }

    private void tpNextLvl()
    {
        SceneManager.LoadScene(nextScene);
    }
}
