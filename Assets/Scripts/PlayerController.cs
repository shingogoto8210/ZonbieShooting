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

        //�}�E�X�𓮂����Ď��_�؂肩��
        float xRot = Input.GetAxis("Mouse X") * Ysensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensitivity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        mainCamera.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        //�J�[�\���̕\���ؑ�
        UpdateCursorLock();

        //���N���b�N����Əe�����Ă�
        if (Input.GetMouseButton(0) && Weapon.instance.canShoot)
        {
            //�}�K�W�����ɒe�������Ă���Ƃ�
            if (ammoClip > 0)
            {
                //animationManager.PlayAnimation(anim, CharacterState.Walk,false);
                animationManager.PlayAnimation(anim, CharacterState.Fire);

                Weapon.instance.canShoot = false;

                //�}�K�W�����̒e�����炷
                ammoClip--;

                //UI�X�V
                uiManager.UpdateDisplayAmmunition();
            }
            else
            {
                //Debug.Log("�e�؂�");

                //Trigger��
                Weapon.instance.TriggerSE();
            }
        }

        //�C�x���g����R�{�^���������ƃ����[�h
        if (Input.GetKeyDown(KeyCode.R))
        {

            //�}�K�W�����ɒe���ǉ��ŉ������邩�v�Z
            int amountNeed = maxAmmoClip - ammoClip;

            //�}�K�W�����ɓ����ׂ��e���Ə����e�򐔂��r����
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

        //�E�N���b�N���Ă���ԁC�`������
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
    /// �v���C���[�̏����ݒ�
    /// </summary>
    public void SetUpPlayerController(UIManager uiManager)
    {
        animationManager = GetComponent<AnimationManager>();
        camCom = mainCamera.GetComponent<Camera>();
        this.uiManager = uiManager;

        //�J�����ƃL�����N�^�[�̊p�x���擾
        cameraRot = mainCamera.transform.localRotation;
        characterRot = transform.localRotation;

        //�e�����Ă��Ԃɂ���
        Weapon.instance.canShoot = true;

        //UI�̏����ݒ�ݒ�
        uiManager.UpdateDisplayHP();
        uiManager.UpdateDisplayAmmunition();
    }


    /// <summary>
    /// �}�E�X�J�[�\���̕\���ؑ�
    /// </summary>
    public void UpdateCursorLock()
    {
        //Escape�������ƁC�J�[�\�����\�������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        //���N���b�N�ŃJ�[�\�����\���ɂ���
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
    /// ���_�ړ��̊p�x����
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
    /// �v���C���[�̃A�j���[�V�����̊Ǘ�
    /// </summary>
    /// <param name="isMove"></param>
    public void MoveAnimation(bool isMove)
    {
        animationManager.PlayAnimation(anim, CharacterState.Walk, isMove);
    }

    /// <summary>
    /// ������炷
    /// </summary>
    public void PlayerWalkFootStep()
    {
        playerFootStep.loop = true;

        playerFootStep.pitch = 1f;

        playerFootStep.clip = walkFootStepSE;

        playerFootStep.Play();

    }

    /// <summary>
    /// �������~�߂�
    /// </summary>
    public void StopFootStep()
    {
        playerFootStep.Stop();

        playerFootStep.loop = false;
    }

    /// <summary>
    /// �v���C���[���_���[�W���󂯂�
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
