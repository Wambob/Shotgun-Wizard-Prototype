using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image manaBar;

    public static UI uiInstance;

    private void Awake()
    {
        if (uiInstance == null)
        {
            uiInstance = this;
        }
        else if (uiInstance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetManaBar(float val)
    {
        manaBar.fillAmount = val;
    }
}
