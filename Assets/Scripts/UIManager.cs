using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public Text textAmmo;

    public Slider hpBar;

    public void UpdateDisplayAmmunition()
    {
        textAmmo.text = playerController.ammoClip + "/" + playerController.ammunition;
    }

    public void UpdateDisplayHP()
    {
        hpBar.value = playerController.playerHP;
    }
}
