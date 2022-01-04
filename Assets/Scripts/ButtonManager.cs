using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnGameStart;

    private void Start()
    {
        btnGameStart?.OnClickAsObservable()
            .Subscribe(_ => OnClickStart());
    }

    private void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
