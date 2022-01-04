using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    private CanvasGroup canvas;

   
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

    public IEnumerator GameStart()
    {
        canvas.alpha = 0;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvas.DOFade(1.0f, 3.0f));
        sequence.Append(canvas.DOFade(0.0f, 3.0f));
        yield return new WaitForSeconds(5.0f);
    }
}
