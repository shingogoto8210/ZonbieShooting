using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Quaternion cameraRot, characterRot;

    float Xsensitivity = 3f, Ysensitivity = 3f;

    bool cursorLock = true;

    float minX = -90f, maxX = 90f;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Animator anim;

    private AnimationManager animationManager;

    public int ammunition = 50, ammoClip = 10, maxAmmoClip = 10;

    public int playerHP = 100, maxPlayerHP = 100;

    public GameObject mainCamera, subCamera;

    private Camera camCom;

    public AudioSource playerFootStep;

    public AudioClip walkFootStepSE;

    public bool isReloading;

    private UIManager uiManager;

    private void Update()
    {

        //マウスを動かして視点切りかえ
        float xRot = Input.GetAxis("Mouse X") * Ysensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensitivity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        mainCamera.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        //カーソルの表示切替
        UpdateCursorLock();

        //左クリックすると銃を撃てる
        if (Input.GetMouseButton(0) && Weapon.instance.canShoot)
        {
            //マガジン内に弾が入っているとき
            if (ammoClip > 0)
            {
                //animationManager.PlayAnimation(anim, CharacterState.Walk,false);
                animationManager.PlayAnimation(anim, CharacterState.Fire);

                Weapon.instance.canShoot = false;

                //マガジン内の弾を減らす
                ammoClip--;

                //UI更新
                uiManager.UpdateDisplayAmmunition();
            }
            else
            {
                //Debug.Log("弾切れ");

                //Trigger音
                Weapon.instance.TriggerSE();
            }
        }

        //イベント中にRボタンを押すとリロード
        if (Input.GetKeyDown(KeyCode.R))
        {

            //マガジン内に弾が追加で何発入るか計算
            int amountNeed = maxAmmoClip - ammoClip;

            //マガジン内に入れるべき弾数と所持弾薬数を比較する
            int ammoAvailable = amountNeed < ammunition ? amountNeed : ammunition;

            if (amountNeed != 0 && ammunition != 0)
            {
                isReloading = true;
                animationManager.PlayAnimation(anim, CharacterState.Reload);
                ammunition -= ammoAvailable;
                ammoClip += ammoAvailable;
                uiManager.UpdateDisplayAmmunition();
            }
        }

        //右クリックしている間，覗き込み
        if (Input.GetMouseButton(1) && isReloading == false)
        {
            subCamera.SetActive(true);
            camCom.enabled = false;
        }
        else if (subCamera.activeSelf)
        {
            subCamera.SetActive(false);
            camCom.enabled = true;
        }


    }

    /// <summary>
    /// プレイヤーの初期設定
    /// </summary>
    public void SetUpPlayerController(UIManager uiManager)
    {
        animationManager = GetComponent<AnimationManager>();
        camCom = mainCamera.GetComponent<Camera>();
        this.uiManager = uiManager;

        //カメラとキャラクターの角度を取得
        cameraRot = mainCamera.transform.localRotation;
        characterRot = transform.localRotation;

        //銃を撃てる状態にする
        Weapon.instance.canShoot = true;

        //UIの初期設定設定
        uiManager.UpdateDisplayHP();
        uiManager.UpdateDisplayAmmunition();
    }


    /// <summary>
    /// マウスカーソルの表示切替
    /// </summary>
    public void UpdateCursorLock()
    {
        //Escapeを押すと，カーソルが表示される
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        //左クリックでカーソルを非表示にする
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// 視点移動の角度制限
    /// </summary>
    /// <returns></returns>
    public Quaternion ClampRotation(Quaternion q)
    {

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;

    }

    /// <summary>
    /// プレイヤーのアニメーションの管理
    /// </summary>
    /// <param name="isMove"></param>
    public void MoveAnimation(bool isMove)
    {
        animationManager.PlayAnimation(anim, CharacterState.Walk, isMove);
    }

    /// <summary>
    /// 足音を鳴らす
    /// </summary>
    public void PlayerWalkFootStep()
    {
        playerFootStep.loop = true;

        playerFootStep.pitch = 1f;

        playerFootStep.clip = walkFootStepSE;

        playerFootStep.Play();

    }

    /// <summary>
    /// 足音を止める
    /// </summary>
    public void StopFootStep()
    {
        playerFootStep.Stop();

        playerFootStep.loop = false;
    }

    /// <summary>
    /// プレイヤーがダメージを受ける
    /// </summary>
    /// <param name="damage"></param>

    public void TakeHit(float damage)
    {
        playerHP = (int)Mathf.Clamp(playerHP - damage, 0, maxPlayerHP);

        uiManager.UpdateDisplayHP();

        if (playerHP < 0 && gameManager.currentGameState != GameState.GameOver)
        {
            gameManager.GameOver();
        }
    }

}
