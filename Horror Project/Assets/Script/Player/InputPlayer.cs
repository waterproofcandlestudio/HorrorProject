using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    public float sensibilityObjectViewerDrag;
    public float mouseX;
    public float mouseY;
    public float x;
    public float y;
    bool check=true;
    InteractionMode interactionMode;
    FPSCamera fpsCam;
    public KeyCode keyInspector = KeyCode.Q;
    public KeyCode keyGetElement = KeyCode.X;
    public KeyCode keyDontGetElement = KeyCode.E;
    public KeyCode keyZoom = KeyCode.Space;



    // Start is called before the first frame update
    void Start()
    {
        interactionMode = GetComponent<InteractionMode>();
        fpsCam = GetComponent<FPSCamera>();

        PlayerMusicManager.instance.PlayMusic("backgroundAmbient");
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        mouseY = -mouseY;
        if(interactionMode.isInInspector == false && interactionMode.isInMiniGame == false &&
        interactionMode.isInPreviewItemInventary == false)
        {
            if(Input.GetKeyDown(keyZoom))
            {
                fpsCam.StartZooming();
            }
        }

        if(interactionMode.isInInspector==false&&Input.GetKeyDown(keyInspector))
        {
            if(check==true)
            {
                interactionMode.AcessInventaryMode();
                check = false;
                PlayerSFXManager.instance.PlaySFX("openBag");
            }
            else if( interactionMode.itemInventaryActivated == true)
            {
                interactionMode.ExitPreviewItemInventaryMode();

            }
            else if(check==false)
            {
                interactionMode.ExitInventaryMode();
                check = true;
                PlayerSFXManager.instance.PlaySFX("saveInInventory");
            }

        }
        if(interactionMode.isInMiniGame==true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                interactionMode.isClickedInMiniGame = true;
            }
            else
            {
                interactionMode.isClickedInMiniGame = false;

            }
        }
    }
}
