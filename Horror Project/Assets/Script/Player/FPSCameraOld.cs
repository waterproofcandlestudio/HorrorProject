using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCameraOld : MonoBehaviour
{
    [SerializeField]
    InputPlayer inputPlayer;
    [SerializeField]
    PlayerController playerController;

    float flipCameraY;
    [SerializeField]
    float distanceRayGetObject;
    [SerializeField]
    GameObject mira;
    // Sensibilidad 
    public Slider sensivitySlider;
    [SerializeField]
    float sensivity = 2;
    GameObject lantern;
    [SerializeField]
    Camera cam;
    Vector3 v3;
    [Header("HeadBob")]
    [SerializeField]
    float headBobbingHorizontalAmount =0f;
    float headBobbingVerticalAmount;
    float headBobbingVerticalLanternAmount;

    float headBobbingValue = 0.0001f;
    float headBobbingVerticalValue = 0.04f;

    float starterHeadBobbingPositionHorizontal;
    float starterHeadBobbingPositionVertical;
    float starterHeadBobbingPositionLanternVertical;

    [SerializeField]
    float toleranceValueHorizontal;
    [SerializeField]
    float toleranceValueVertical;
    bool changeHeadBobSide = true;
    bool onlyOnce = true;
    bool checkCenter = false;
    bool changeEverything = true;
    bool changeHeadBobSideVertical = true;
    bool checkCenterVertical = false;
    bool changeEverythingVertical = true;

    bool changeHeadBobSideLanternVertical = true;
    bool checkCenterVerticalLantern = false;
    bool changeEverythingLanternVertical = true;

    [Header("Zoom")]
    [SerializeField]
    bool isZooming = false;
    bool isAlreadyZooming = false;

    [SerializeField]
    float startFOV;
    [SerializeField]
    float endFOV;
    float timePassed;
    [SerializeField]
    float timeToZoom;

    [Header("Angles")]
    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        flipCameraY = 0f;
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        starterHeadBobbingPositionHorizontal = cam.transform.localEulerAngles.z;
        starterHeadBobbingPositionVertical = cam.transform.localPosition.y;
        starterHeadBobbingPositionLanternVertical = lantern.transform.localPosition.y;
        headBobbingVerticalAmount = starterHeadBobbingPositionVertical;
        headBobbingVerticalLanternAmount= starterHeadBobbingPositionLanternVertical;
        startFOV = cam.fieldOfView;
    }

    private void Start()
    {
        // Hace q el raton no se pueda salir de la ventana del juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Barra d sensibilidad menú
        //sensivitySlider.value = PlayerPrefs.GetFloat("sensivity");
    }

    // Update is called once per frame
    void Update()
    {
        v3 = new Vector3(0, inputPlayer.mouseX , 0) ;
        transform.Rotate(v3);


        //sensivity = PlayerPrefs.GetFloat("sensivity");


        flipCameraY -= inputPlayer.mouseY;

        if (playerController.isMoving == true)
        {
            v3 = new Vector3(inputPlayer.mouseY, 0, 0)  ;

        }
        else
        {
            v3 = new Vector3(inputPlayer.mouseY, 0, 0);

        }

        if (flipCameraY > 90)
        {
            flipCameraY = 90f;
        }
        else if (flipCameraY < -90)
        {
            flipCameraY = -90f;
        }
        else
        {
            cam.transform.Rotate(v3);

        }
        ////horizontal headbob
        if (playerController.isMoving == true)  //si esta moviendose se gira la camara para la derecha y para la izquierda
        {
            if (changeHeadBobSide == true)
            {


                StartCoroutine(HeadBobbingHorizontalLeft());

            }
            else
            {
                StartCoroutine(HeadBobbingHorizontalRight());


            }
            checkCenter = true;
        }
        else
        {

            if (checkCenter == true)
            {
                HeadBobbingHorizontalCenter();  //si no se mueve y se comprueba si hay que recolocar la camara,  gira 
                                                //hacia el centro de nuevo
            }

        }

        //vertical headbob
        if (playerController.isMoving == true)
        {
            if (changeHeadBobSideVertical == true)
            {


                StartCoroutine(HeadBobbingVerticalUp());

            }
            else
            {
                StartCoroutine(HeadBobbingVerticalDown());


            }
            checkCenterVertical = true;
        }
        else
        {

            if (checkCenterVertical == true)
            {
                HeadBobbingVerticalCenter();
            }

        }

        if (playerController.isMoving == true)
        {
            if (changeHeadBobSideLanternVertical == true)
            {


                StartCoroutine(HeadBobbingVerticalLanternUp());

            }
            else
            {
                StartCoroutine(HeadBobbingVerticalLanternDown());


            }
            checkCenterVerticalLantern = true;
        }
        else
        {

            if (checkCenterVerticalLantern == true)
            {
                HeadBobbingVerticalLanternCenter();
            }

        }
        if (changeEverything == false)
        {
            headBobbingHorizontalAmount = 0;

            changeEverything = true;
        }

        if (changeEverythingVertical == false)
        {
            headBobbingVerticalAmount = starterHeadBobbingPositionVertical;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, starterHeadBobbingPositionVertical, cam.transform.localPosition.z);
            changeEverythingVertical = true;
        }
        if (changeEverythingLanternVertical == false)
        {
            headBobbingVerticalLanternAmount = starterHeadBobbingPositionLanternVertical;
            changeEverythingLanternVertical = true;
        }



        eulerAngZ = cam.transform.localEulerAngles.z;





        Debug.DrawRay(cam.transform.position, Vector3.forward * distanceRayGetObject, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distanceRayGetObject, LayerMask.GetMask("Interactable Object")))
        {
            mira.GetComponent<Image>().color = Color.red;

            if (Input.GetMouseButtonDown(0))
            {

                hit.transform.GetComponentInParent<ObjectInteractable>().Interact();
                
            }

        }
        else
        {
            mira.GetComponent<Image>().color = Color.white;
        }


    }
    IEnumerator HeadBobbingHorizontalLeft()
    {
        if(onlyOnce==true&&cam.transform.localEulerAngles.z==0)
        {
            headBobbingHorizontalAmount += headBobbingValue * Time.deltaTime;
            v3 = new Vector3(0, 0, headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);


            yield return new WaitForSeconds(0.5f);
            StopCoroutine(HeadBobbingHorizontalLeft());
            changeHeadBobSide = false;
            onlyOnce = false;   
        }
        else
        {
            headBobbingHorizontalAmount += headBobbingValue * 2 * Time.deltaTime;
            v3 = new Vector3(0, 0, headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);


            yield return new WaitForSeconds(0.5f);
            StopCoroutine(HeadBobbingHorizontalLeft());
            changeHeadBobSide = false;
        }


    }
    IEnumerator HeadBobbingHorizontalRight()
    {


        headBobbingHorizontalAmount += headBobbingValue * 2 * Time.deltaTime;
        v3 = new Vector3(0, 0, -headBobbingHorizontalAmount);

        cam.transform.Rotate(v3);

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingHorizontalRight());

        changeHeadBobSide = true;

    }
    void HeadBobbingHorizontalCenter()    //vuelve a la posicion central
    {
        if (WrapAngle(eulerAngZ)> starterHeadBobbingPositionHorizontal)
        {
            headBobbingHorizontalAmount += headBobbingValue * 2 * Time.deltaTime;
            v3 = new Vector3(0, 0, -headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);
        }
        else if(WrapAngle(eulerAngZ) < starterHeadBobbingPositionHorizontal)
        {
            headBobbingHorizontalAmount += headBobbingValue * Time.deltaTime;
            v3 = new Vector3(0, 0, headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);

        }
        if (myApproximation(WrapAngle(eulerAngZ), starterHeadBobbingPositionHorizontal, toleranceValueHorizontal) ==true)
        {

            checkCenter = false;
            changeEverything = false;
        }





    }
    IEnumerator HeadBobbingVerticalUp()
    {
        headBobbingVerticalAmount += headBobbingVerticalValue *2* Time.deltaTime;
        v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);


        cam.transform.localPosition = v3;
        
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalUp());
        changeHeadBobSideVertical = false;

    }
    IEnumerator HeadBobbingVerticalDown()
    {


        headBobbingVerticalAmount -= headBobbingVerticalValue * 2 * Time.deltaTime;

        v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);

        cam.transform.localPosition=v3;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalDown());

        changeHeadBobSideVertical = true;

    }
    void HeadBobbingVerticalCenter()    //vuelve a la posicion central
    {
        if ((cam.transform.localPosition.y) > starterHeadBobbingPositionVertical)
        {
            headBobbingVerticalAmount -= headBobbingVerticalValue * 2 * Time.deltaTime;
            v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);
            cam.transform.localPosition = v3;
        }
        else if ((cam.transform.localPosition.y) < starterHeadBobbingPositionVertical)
        {
            headBobbingVerticalAmount += headBobbingVerticalValue *2* Time.deltaTime;
            v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);
            cam.transform.localPosition = v3;
        }
        if (myApproximation(cam.transform.localPosition.y, starterHeadBobbingPositionVertical, toleranceValueVertical) == true)
        {
            checkCenterVertical = false;
            changeEverythingVertical = false;
        }





    }
    IEnumerator HeadBobbingVerticalLanternUp()
    {
        headBobbingVerticalLanternAmount += headBobbingVerticalValue * 2 * Time.deltaTime;
        v3 = new Vector3(lantern.transform.localPosition.x, headBobbingVerticalLanternAmount, lantern.transform.localPosition.z);


        lantern.transform.localPosition = v3;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalLanternUp());
        changeHeadBobSideLanternVertical = false;

    }
    IEnumerator HeadBobbingVerticalLanternDown()
    {


        headBobbingVerticalLanternAmount -= headBobbingVerticalValue * 2 * Time.deltaTime;

        v3 = new Vector3(lantern.transform.localPosition.x, headBobbingVerticalLanternAmount, lantern.transform.localPosition.z);

        lantern.transform.localPosition = v3;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalLanternDown());

        changeHeadBobSideLanternVertical = true;

    }
    void HeadBobbingVerticalLanternCenter()    //vuelve a la posicion central
    {
        if ((lantern.transform.localPosition.y) > starterHeadBobbingPositionLanternVertical)
        {
            headBobbingVerticalLanternAmount -= headBobbingVerticalValue * 2 * Time.deltaTime;
            v3 = new Vector3(lantern.transform.localPosition.x, headBobbingVerticalLanternAmount, lantern.transform.localPosition.z);
            lantern.transform.localPosition = v3;
        }
        else if ((lantern.transform.localPosition.y) < starterHeadBobbingPositionLanternVertical)
        {
            headBobbingVerticalLanternAmount += headBobbingVerticalValue * 2 * Time.deltaTime;
            v3 = new Vector3(lantern.transform.localPosition.x, headBobbingVerticalLanternAmount, lantern.transform.localPosition.z);
            lantern.transform.localPosition = v3;
        }
        if (myApproximation(lantern.transform.localPosition.y, starterHeadBobbingPositionLanternVertical, toleranceValueVertical) == true)
        {
            checkCenterVerticalLantern = false;
            changeEverythingLanternVertical = false;
        }





    }
    public void StartZooming()
    {
        

        if (isZooming==true&& isAlreadyZooming==false)
        {
            StartCoroutine(EnableZooming());
            isAlreadyZooming = true;
            isZooming = false;
        }
        else if(isZooming == false && isAlreadyZooming == false)
        {
            StartCoroutine(DisabledZooming());
            isAlreadyZooming = true;
            isZooming = true;

        }
    }
    IEnumerator EnableZooming()
    {
        timePassed = 0;
        while (timePassed<timeToZoom)
        {

            cam.fieldOfView = Mathf.Lerp(startFOV, endFOV,timePassed/timeToZoom);   
            Debug.Log(timePassed / timeToZoom);                            
            timePassed += Time.deltaTime;                                           
            yield return null;

            //para cambiar el campo de vision
            //utiliza la funcion de la libreria mathf
            //para alternar entre el campo de vvision inicial y el final o objetivo mediante un numero
            //que segun timePassed incremente, este también por lo que se acercaría más a el valor final
            //empezaria en 0.01 y acabaria en 0.991 que sería ya el target final
        }
        StopCoroutine(EnableZooming());

    }
    IEnumerator DisabledZooming()
    {
        timePassed = 0;
        while (timePassed < timeToZoom)
        {

            cam.fieldOfView = Mathf.Lerp(endFOV, startFOV, timePassed / timeToZoom);
            timePassed += Time.deltaTime;
            yield return null;




        }
        StopCoroutine(DisabledZooming());
        yield return null;
    }
    private bool myApproximation(float a, float b, float tolerance)
    {
        //Debug.Log(Mathf.Abs(a - b));
        return (Mathf.Abs(a - b) < tolerance);  //en valor absoluto ya que nos da igual si la rotacion de la camara es <0,
                                       //lo que queremos saber es si la diferencia entre la rotacion de la 
                                                //camara y la posicion inicial es menor que la tolerancia asignada
                                                
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }


}
