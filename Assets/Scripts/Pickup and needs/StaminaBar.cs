using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    float stamina = 100;
    Image staminaBar;
    public float staminaDepletion, staminaRecovery;
    public MovementInput mi;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina += Time.deltaTime * (mi.Speed > 0.5f ? -staminaDepletion : staminaRecovery) * (mi.isSprinting ? 2 : 1);
        stamina = Mathf.Clamp(stamina, 0, 100);
        Vector3 tmp = staminaBar.transform.localScale;
        tmp.x = stamina / 100;
        staminaBar.transform.localScale = tmp;
    }

    public void RefillStamina(float value)
    {
        stamina += value;
    }
}
