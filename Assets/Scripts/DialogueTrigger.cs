using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private InterfaceManager ui;
    private Villager currentVillager = null;
    private Villager goalVillager;
    private MovementInput mi;
    // Start is called before the first frame update
    void Start()
    {
        mi = GetComponent<MovementInput>();
        ui = InterfaceManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ui.inDialogue && currentVillager != null)
        {

            mi.canMove = false;
            
            ui.StartInteraction(currentVillager);
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
