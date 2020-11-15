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
    public NeedBar nb;
    bool isRefilling;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina < 0.1f)
        {
            isRefilling = true;
            mi.canMove = false;
        }
        float staminaChange = Time.deltaTime * (mi.Speed > 0.5f ? -staminaDepletion : staminaRecovery / (isRefilling ? 2 : 1));
        if (mi.isSprinting)
            staminaChange *= 2;
        stamina += staminaChange;
        float staminaCap = nb.getNeedCap();
        stamina = Mathf.Clamp(stamina, 0, staminaCap);
        if (stamina > staminaCap - 1)
        {
            isRefilling = false;
            mi.canMove = true;
        }
        Vector3 tmp = staminaBar.transform.localScale;
        tmp.x = stamina / 100;
        staminaBar.transform.localScale = tmp;
    }

    public void RefillStamina(float value)
    {
        stamina += value;
    }
}
