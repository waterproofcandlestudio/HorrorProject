using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCamera : MonoBehaviour
{
    [SerializeField]
    InputPlayer inputPlayer;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    Canvas canvasItemFloating;
    Vector3 itemFloatingVector3D;

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
    float headBobbingHorizontalLanternAmount;

    float headBobbingValue = 0.0001f;
    float headBobbingVerticalValue = 0.04f;

    float starterHeadBobbingPositionHorizontal;
    float starterHeadBobbingPositionVertical;
    float starterHeadBobbingPositionLanternVertical;
    float starterHeadBobbingPositionLanternHorizontal;

    float time;
    [SerializeField]
    float timeToHeadBoob =0.5f;
    [SerializeField]
    float timeToHeadBoobVertical = 0.5f;
    [SerializeField]
    float timeToHeadBoobLanternHorizontal = 2f;
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

    bool changeHeadBobSideLanternHorizontal = true;
    bool checkCenterHorizontalLantern = false;
    bool changeEverythingLanternHorizontal = true;

    [Header("Zoom")]
    [SerializeField]
    bool isZooming = true;
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
    [SerializeField]
    Animator animIdle;

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
        itemFloatingVector3D = canvasItemFloating.transform.localPosition;


    }

    private void Start()
    {
        animIdle = GetComponentInChildren<Animator>();  //coge la referencia de la linterna
        animIdle.SetBool("EnableIdle", true);   //comienza la animacion
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
                StartCoroutine(HeadBobbingVerticalCenter());
            }

        }

        if (playerController.isMoving == true)
        {
            animIdle.SetBool("EnableIdle", false);
            HeadBobbingVerticalLanternCenter();

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
            else
            {
                animIdle.SetBool("EnableIdle", true);
            }

        }

        if (playerController.isMoving == true)
        {
            if (changeHeadBobSideLanternHorizontal == true)
            {

                StartCoroutine(HeadBobbingHorizontalLanternLeft());


            }
            else
            {
                StartCoroutine(HeadBobbingHorizontalLanternRight());


            }
            checkCenterHorizontalLantern = true;
        }
        else
        {

            if (checkCenterHorizontalLantern == true)
            {
                StartCoroutine(HeadBobbingHorizontalLanternCenter());
               
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
        if (changeEverythingLanternHorizontal == false)
        {
            changeEverythingLanternHorizontal = true;
        }


        eulerAngZ = cam.transform.localEulerAngles.z;





        Debug.DrawRay(cam.transform.position, Vector3.forward * distanceRayGetObject, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distanceRayGetObject, LayerMask.GetMask("Interactable Object")))
        {
            mira.GetComponent<Image>().color = Color.red;
            canvasItemFloating.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+0.3f, hit.transform.position.z);
            canvasItemFloating.transform.LookAt(cam.transform.position);


            if (Input.GetMouseButtonDown(0))
            {

                hit.transform.GetComponentInParent<ObjectInteractable>().Interact();
            }

        }
        else
        {
            mira.GetComponent<Image>().color = Color.white;
            canvasItemFloating.transform.position = itemFloatingVector3D;

        }


    }
    IEnumerator HeadBobbingHorizontalLeft()
    {
        time = 0;
        while (time<timeToHeadBoob)
        {
            time += Time.deltaTime;
            cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y, Mathf.Lerp(-0.1f, starterHeadBobbingPositionHorizontal, time / timeToHeadBoob));
            yield return null;
        }
        changeHeadBobSide = false;
        StopCoroutine(HeadBobbingHorizontalLeft()); 



    }
    IEnumerator HeadBobbingHorizontalRight()
    {


        time = 0;
        while (time < timeToHeadBoob)
        {
            time += Time.deltaTime;
            cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y, Mathf.Lerp(0.1f, starterHeadBobbingPositionHorizontal, time / timeToHeadBoob));
            yield return null;
        }
        changeHeadBobSide = true;

        StopCoroutine(HeadBobbingHorizontalRight());

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
    { /*
        float position = cam.transform.localPosition.y;
        time = 0;
        while (time < 0.25)
        {
            time += Time.deltaTime;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(position, 0.85f, time / timeToHeadBoob), cam.transform.localPosition.z);
            yield return null;
        }
        changeHeadBobSideVertical = false;
        StopCoroutine(HeadBobbingVerticalUp());
       */
        headBobbingVerticalAmount += headBobbingVerticalValue * 2 * Time.deltaTime;
        v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);


        cam.transform.localPosition = v3;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalUp());
        changeHeadBobSideVertical = false;
        
    }
    IEnumerator HeadBobbingVerticalDown()
    {
        /*
            float position = cam.transform.localPosition.y;
            time = 0;
            while (time < 0.25)
            {
                time += Time.deltaTime;
                cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(position, 0.80f, time / timeToHeadBoob), cam.transform.localPosition.z);
                yield return null;
            }
            changeHeadBobSideVertical = true;
            StopCoroutine(HeadBobbingVerticalDown());
        */

        headBobbingVerticalAmount -= headBobbingVerticalValue * 2 * Time.deltaTime;

        v3 = new Vector3(cam.transform.localPosition.x, headBobbingVerticalAmount, cam.transform.localPosition.z);

        cam.transform.localPosition = v3;

        yield return new WaitForSeconds(0.5f);
        StopCoroutine(HeadBobbingVerticalDown());

        changeHeadBobSideVertical = true;

    }
    IEnumerator HeadBobbingVerticalCenter()    //vuelve a la posicion central
    {

        float position= cam.transform.localPosition.y;
        time = 0;
        while (time < timeToHeadBoob)
        {
            time += Time.deltaTime;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x,  Mathf.Lerp(position,starterHeadBobbingPositionVertical , time / timeToHeadBoob), cam.transform.localPosition.z);
            yield return null;
        }
        checkCenterVertical = false;
        StopCoroutine(HeadBobbingVerticalCenter());
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
        Debug.Log(starterHeadBobbingPositionLanternVertical);
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

    IEnumerator HeadBobbingHorizontalLanternLeft()
    {
        time = 0;
        while (time < timeToHeadBoobLanternHorizontal)
        {
            time += Time.deltaTime;
            lantern.transform.localEulerAngles = new Vector3(lantern.transform.localEulerAngles.x, lantern.transform.localEulerAngles.y, Mathf.Lerp(-1f,
                starterHeadBobbingPositionLanternHorizontal, time / timeToHeadBoobLanternHorizontal));
            yield return null;
        }
        changeHeadBobSideLanternHorizontal = false;
        StopCoroutine(HeadBobbingHorizontalLanternLeft());



    }
    IEnumerator HeadBobbingHorizontalLanternRight()
    {


        time = 0;
        while (time < timeToHeadBoob)
        {
            time += Time.deltaTime;
            lantern.transform.localEulerAngles = new Vector3(lantern.transform.localEulerAngles.x, lantern.transform.localEulerAngles.y, Mathf.Lerp(starterHeadBobbingPositionLanternHorizontal, 1f,
                time / timeToHeadBoobLanternHorizontal));
            yield return null;
        }
        changeHeadBobSideLanternHorizontal = true;

        StopCoroutine(HeadBobbingHorizontalLanternRight());

    }
    IEnumerator HeadBobbingHorizontalLanternCenter()    //vuelve a la posicion central
    {
        float position = lantern.transform.localEulerAngles.z;
        time = 0;
        while (time < timeToHeadBoob)
        {
            time += Time.deltaTime;
            lantern.transform.localEulerAngles = new Vector3(lantern.transform.localEulerAngles.x, lantern.transform.localEulerAngles.y, Mathf.Lerp(position, starterHeadBobbingPositionLanternHorizontal, time / timeToHeadBoob));
            yield return null;
        }
        checkCenterHorizontalLantern = false;
        StopCoroutine(HeadBobbingHorizontalLanternCenter());
        /*
        if (WrapAngle(eulerAngZ) > starterHeadBobbingPositionHorizontal)
        {
            headBobbingHorizontalAmount += headBobbingValue * 2 * Time.deltaTime;
            v3 = new Vector3(0, 0, -headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);
        }
        else if (WrapAngle(eulerAngZ) < starterHeadBobbingPositionHorizontal)
        {
            headBobbingHorizontalAmount += headBobbingValue * Time.deltaTime;
            v3 = new Vector3(0, 0, headBobbingHorizontalAmount);

            cam.transform.Rotate(v3);

        }
        if (myApproximation(WrapAngle(eulerAngZ), starterHeadBobbingPositionHorizontal, toleranceValueHorizontal) == true)
        {

            checkCenter = false;
            changeEverything = false;
        }
        */




    }

    public void StartZooming()
    {
        

        if (isZooming==true&& isAlreadyZooming==false)
        {
            StartCoroutine(EnableZooming());
            isZooming = false;
        }
        else if(isZooming == false && isAlreadyZooming == false)
        {
            StartCoroutine(DisabledZooming());
            isZooming = true;

        }
    }
    IEnumerator EnableZooming()
    {
        timePassed = 0;
        isAlreadyZooming=true;
        while (timePassed<timeToZoom)
        {

            cam.fieldOfView = Mathf.Lerp(startFOV, endFOV,timePassed/timeToZoom);   
            timePassed += Time.deltaTime;                                           
            yield return null;
            
            //para cambiar el campo de vision
            //utiliza la funcion de la libreria mathf
            //para alternar entre el campo de vvision inicial y el final o objetivo mediante un numero
            //que segun timePassed incremente, este también por lo que se acercaría más a el valor final
            //empezaria en 0.01 y acabaria en 0.991 que sería ya el target final
        }
        isAlreadyZooming = false;

        StopCoroutine(EnableZooming());

    }
    IEnumerator DisabledZooming()
    {
        timePassed = 0;
        isAlreadyZooming = true;

        while (timePassed < timeToZoom)
        {

            cam.fieldOfView = Mathf.Lerp(endFOV,startFOV, timePassed / timeToZoom);
            timePassed += Time.deltaTime;
            yield return null;




        }
        isAlreadyZooming = false;

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
