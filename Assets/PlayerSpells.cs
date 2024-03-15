using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private float manaMax, manaCurrent, manaRechargeTime;

    private float rechargeRate;

    private void Start()
    {
        rechargeRate = manaMax / manaRechargeTime;
    }

    private void Update()
    {
        manaCurrent = Mathf.Clamp(manaCurrent + rechargeRate * Time.deltaTime, 0, manaMax);

        UI.uiInstance.SetManaBar(manaCurrent / manaMax);
    }

    public bool ManaSpend(float cost)
    {
        if (manaCurrent - cost < 0)
        {
            return false;
        }
        else
        {
            manaCurrent = Mathf.Clamp(manaCurrent - cost, 0, manaMax);
            return true;
        }
    }
}
