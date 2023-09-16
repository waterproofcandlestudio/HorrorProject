using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    EnterTriggerByBall triggerScript;
    InteractionMode interactionMode;
    [SerializeField]
    List<SafeDepositBox> safeDepositBoxList;
    [SerializeField]
    SafeDepositBox safeDepositBox;

    public bool isClicked;
    void Awake()
    {
        triggerScript = gameObject.GetComponentInChildren<EnterTriggerByBall>();
        interactionMode=GameObject.Find("Player").GetComponentInChildren<InteractionMode>();
        safeDepositBoxList = new List<SafeDepositBox>();
        safeDepositBoxList.AddRange(FindObjectsOfType<SafeDepositBox>());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isClicked = interactionMode.isClickedInMiniGame;

        if(triggerScript.isIn==true&& isClicked == true)
        {
            interactionMode.ExitMiniGameMode();
            ChangeStatusSafeDepositBoxSelected();

        }

    }
    void ChangeStatusSafeDepositBoxSelected()
    {
        safeDepositBox = safeDepositBoxList.Find(j => j.isInteracted == true);
        safeDepositBox.isInteracted = false;
        safeDepositBox.isOpened = true;
        safeDepositBox.Open();

    }
}
