using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float healValue;
    public string healType;
    bool canBeEaten;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBeEaten)
        {
            if (healType == "stamina")
                FindObjectOfType<StaminaBar>().RefillStamina(healValue);
            else
                FindObjectOfType<NeedBar>().RefillNeed(healType, healValue);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canBeEaten = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canBeEaten = false;
    }
}
