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
    private CanvasGroup canvasGameStart;

    [SerializeField]
    private CanvasGroup canvasGameOver;

   
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

    public IEnumerator GameStartLogo()
    {
        canvasGameStart.alpha = 0;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGameStart.DOFade(1.0f, 3.0f));
        sequence.Append(canvasGameStart.DOFade(0.0f, 3.0f));
        yield return new WaitForSeconds(5.0f);
    }

    public void GameOverLogo()
    {
        canvasGameOver.alpha = 0;

        canvasGameOver.DOFade(1.0f, 3.0f);
    }
}
