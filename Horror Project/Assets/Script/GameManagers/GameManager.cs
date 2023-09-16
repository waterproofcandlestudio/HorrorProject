using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//LUCAS GARCÍA SCRIPT//
public class GameManager : MonoBehaviour
{
    static GameManager instance=null;
    static int actualLevel = 0;
    public bool isValidatedKey=false;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            actualLevel = 1;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void ChangeLevel()
    {
        if(isValidatedKey==true&&actualLevel<3)
        {
            actualLevel++;
            isValidatedKey = false;

        }

        if (actualLevel==1)
        {
            SceneManager.LoadScene("Escenario 1");

        }
        else if (actualLevel==2)
        {
            SceneManager.LoadScene("Escenario 2");

        }
        else if (actualLevel == 3)
        {
            SceneManager.LoadScene("Escenario 3");
        }

    }
    public void IsOver()
    {
        SceneManager.LoadScene("Credits");

    }


}
