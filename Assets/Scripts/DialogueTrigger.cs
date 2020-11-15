using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    private InterfaceManager ui;
    private Villager currentVillager = null;
    private Villager goalVillager;
    private MovementInput mi;
    CanvasGroup talkTipCG;
    // Start is called before the first frame update
    void Start()
    {
        talkTipCG = GameObject.FindGameObjectWithTag("TalkTip").GetComponent<CanvasGroup>();
        mi = GetComponent<MovementInput>();
        ui = InterfaceManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ui.inDialogue && currentVillager != null)
        {
            talkTipCG.alpha = 0;
            mi.canMove = false;
            
            ui.StartInteraction(currentVillager);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            talkTipCG.alpha = 1;
            currentVillager = other.GetComponent<Villager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            talkTipCG.alpha = 0;
            currentVillager = null;
        }
    }
}
