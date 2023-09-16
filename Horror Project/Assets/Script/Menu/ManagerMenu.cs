using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour  // lo uso tanto para desbloquear niveles como para cargarlos, como para volver al menu desde el juego
{
    public GameObject image;
    static public int nivelesDesbloqueados;
    public int nivelActual;
    public Button[] botonesMenu;
    
    void Start()
    {
        
     
        if(SceneManager.GetActiveScene().name=="EleccionNiveles")
        {
            ActualizarBotones();
        }

    }
    public void CambiarNivel(int nivel)
    {

           
        
        if(nivel == 0)
        {


            image.SetActive(true);

            StartCoroutine(TransicionesNivel(nivel));

        }
        else if(nivel==2)
        {
            image.SetActive(true);
            StartCoroutine(TransicionesNivel(nivel));


        }
        else if (nivel == 3)
        {
            image.SetActive(true);
            StartCoroutine(TransicionesNivel(nivel));


        }
        else if(nivel==4)
        {
            image.SetActive(true);
            StartCoroutine(TransicionesNivel(nivel));
        }
        else if (nivel == 5)
        {
            image.SetActive(true);
            StartCoroutine(TransicionesNivel(nivel));
        }

        else
        {    
            image.SetActive(true);
            StartCoroutine(TransicionesNivel(nivel));
           

            
        }
    }

    public void DesbloquearNivel()
    {
        if(nivelesDesbloqueados<nivelActual)
        {
            nivelesDesbloqueados = nivelActual;
            nivelActual++;
        }


    }
    public void VolverAlMenu()
    {
        CambiarNivel(0);
    }
    public void ActualizarBotones()
    {
        for (int i = 0; i < nivelesDesbloqueados+1; i++)
        {
            botonesMenu[i].interactable = true;
        }
    }
    IEnumerator TransicionesNivel(int nivel)
    {
        if (nivel == 0)
        {

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("Menu");

        }
        else if (nivel == 1)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Escenario 1");


        }
        else if (nivel == 10)
        {
            Application.Quit();

        }

    }





    

}
