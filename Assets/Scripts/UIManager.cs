using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Text textAmmo;

    [SerializeField]
    private Slider hpBar;

    [SerializeField]
    private Text textScore;

    public void UpdateDisplayAmmunition()
    {
        textAmmo.text = playerController.ammoClip + "/" + playerController.ammunition;
    }

    public void UpdateDisplayHP()
    {
        hpBar.value = playerController.playerHP;
    }

    public void UpdateDisplayScore()
    {
        textScore.text = DataBaseManager.instance.score.ToString();
    }
}
