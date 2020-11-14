using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private InterfaceManager ui;
    private Villager currentVillager = null;
    // Start is called before the first frame update
    void Start()
    {
        ui = InterfaceManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ui.inDialogue && currentVillager != null)
        {
            ui.SetDialogueData(currentVillager);
            ui.ClearText();
            ui.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            currentVillager = other.GetComponent<Villager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            currentVillager = null;
        }
    }
}
