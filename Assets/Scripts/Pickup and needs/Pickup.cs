using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public float[] healValue;
    bool canBeEaten;
    Outline outline;
    CanvasGroup tipCG;
    // Start is called before the first frame update
    void Start()
    {
        tipCG = GameObject.FindGameObjectWithTag("ConsumeTip").GetComponent<CanvasGroup>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBeEaten)
        {
            if (healValue[0] > 0)
                FindObjectOfType<StaminaBar>().RefillStamina(healValue[0]);
            FindObjectOfType<NeedBar>().RefillNeed(healValue);
            tipCG.alpha = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tipCG.alpha = 1;
        outline.enabled = true;
        canBeEaten = true;
    }

    private void OnTriggerExit(Collider other)
    {
        tipCG.alpha = 0;
        outline.enabled = false;
        canBeEaten = false;
    }
}
