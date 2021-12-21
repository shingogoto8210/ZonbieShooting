using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent agent;

    private PlayerController target;

    private CapsuleCollider enemyCol;

    private Animator anim;

    private AnimationManager animationManager;

    private GameManager gameManager;

    private RailMoveController railMoveController;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip voiceSE,attackSE,runSE;

    private CharacterState state = CharacterState.Idle;

    private float walkSpeed = 1.0f, runSpeed = 3.0f;

    public int attackDamage;

    public int point;

    private UIManager uiManager;

    private void Start()
    {
        if (!TryGetComponent(out enemyCol)) Debug.Log("collider���擾");
        if (!TryGetComponent(out agent)) Debug.Log("NavMeshAgent���擾");
        if (!TryGetComponent(out anim)) Debug.Log("Animator���擾");
        if (!TryGetComponent(out animationManager)) Debug.Log("AnimationManager���擾");
        if (!GameObject.FindGameObjectWithTag("Player").TryGetComponent(out target)) Debug.Log("AnimationManager���擾");
        railMoveController = target.transform.gameObject.GetComponent<RailMoveController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        switch (state)
        {
            case CharacterState.Idle:

                //�A�j���[�V���������Z�b�g
                animationManager.TurnOffTrigger(anim);
                //�ړI�n���Z�b�g
                agent.ResetPath();

                //�m���ŏ�Ԃ�WALK�ɕύX
                if (Random.Range(0, 5000) < 5)
                {
                    state = CharacterState.Walk;
                }

                //�G�̋�����15��菬�����Ƃ������Ēǐ�
                if (DistanceToPlayer() < 30)
                {
                    PlayZombieSE(voiceSE);
                    PlayZombieRunSE();
                    state = CharacterState.Run;
                }

                break;

            case CharacterState.Walk:

                //�m���ŏ�Ԃ�IDLE�ɕύX
                if (Random.Range(0, 5000) < 5)
                {
                    state = CharacterState.Idle;
                }

                //�G�̋�����15��菬�����Ƃ������Ēǐ�
                if (DistanceToPlayer() < 30)
                {
                    agent.ResetPath();
                    PlayZombieSE(voiceSE);
                    PlayZombieRunSE();
                    state = CharacterState.Run;
                }

                //�G�̋�����15���傫���Ƃ��̓v���C���[��������
                else
                {
                    //�G�̖ړI�n���ݒ肳��Ă��Ȃ��Ƃ�
                    if (!agent.hasPath)
                    {
                        //�V���ȖړI�n�������_���Őݒ�
                        Vector3 nextPos = new Vector3(transform.position.x + Random.Range(-30, 30), transform.position.y, transform.position.z + Random.Range(-30, 30));
                        agent.SetDestination(nextPos);
                        agent.speed = walkSpeed;
                        animationManager.TurnOffTrigger(anim);
                        animationManager.PlayAnimation(anim, CharacterState.Walk, true);
                    }

                }
                break;

            case CharacterState.Run:


                //�G�̋������R��菬�����Ƃ�
                if (DistanceToPlayer() < agent.stoppingDistance)
                {
                    Debug.Log("�G�ɕ߂܂���");

                    //�v���C���[�������Ă���Ƃ�
                    if (railMoveController.isStop == false)
                    {
                        railMoveController.Stop();
                    }
                    StopZombieRunSE();
                    PlayZombieSE(voiceSE);
                    state = CharacterState.Attack;
                }
                else if (DistanceToPlayer() < 30)
                {
                    animationManager.TurnOffTrigger(anim);
                    agent.SetDestination(target.gameObject.transform.position);
                    animationManager.PlayAnimation(anim, CharacterState.Run, true);
                    agent.speed = runSpeed;
                }
                else
                {
                    StopZombieRunSE();
                    agent.ResetPath();
                    state = CharacterState.Walk;
                }
                break;


            case CharacterState.Attack:

                if (railMoveController.isStop == false)
                {
                    railMoveController.Stop();
                }
                animationManager.TurnOffTrigger(anim);
                animationManager.PlayAnimation(anim, CharacterState.Attack, true);

                if (DistanceToPlayer() >= agent.stoppingDistance)
                {
                    state = CharacterState.Run;
                }

                break;

            case CharacterState.Dead:

                StopZombieSE();
                Destroy(agent);

                break;

        }
    }

    /// <summary>
    /// �G�̐ݒ�
    /// </summary>
    //public void SetUpEnemy()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    anim = GetComponent<Animator>();
    //    animationManager = GetComponent<AnimationManager>();
    //    target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //    enemyCol = GetComponent<CapsuleCollider>();
    //}

    /// <summary>
    /// �G��|��
    /// </summary>
    /// <param name="gameManager"></param>
    public IEnumerator DestroyEnemy(GameManager gameManager,int point)
    {
        enemyCol.enabled = false;
        animationManager.TurnOffTrigger(anim);
        animationManager.PlayAnimation(anim, CharacterState.Dead, true);
        state = CharacterState.Dead;
        DataBaseManager.instance.score += point;
        uiManager.UpdateDisplayScore();

        //�C�x���g�ł͂Ȃ��A�v���C���[���~�܂��Ă���Ƃ��i�G�ɕ߂܂��Ă���Ƃ��j
        if (railMoveController.isStop == true && gameManager.currentGameState == GameState.Move)
        {
            yield return new WaitForSeconds(1.0f);
            //�G��|������܂�������
            railMoveController.Resume();
        }

        if (gameManager.currentGameState == GameState.Event)
        {
            gameManager.enemiesList.Remove(this);
            StartCoroutine(gameManager.CheckFinishEvent());
        }
    }

    /// <summary>
    /// �v���C���[�Ƀ_���[�W��^����
    /// </summary>
    public void DamagePlayer()
    {
        if (target != null)
        {
            PlayZombieSE(attackSE);
            target.GetComponent<PlayerController>().TakeHit(attackDamage);
        }
    }

    /// <summary>
    /// �G�ƃv���C���[�̊Ԃ̋������v�Z����
    /// </summary>
    /// <returns></returns>
    private float DistanceToPlayer()
    {
        return Vector3.Distance(target.gameObject.transform.position, transform.position);
    }

    private void PlayZombieSE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayZombieRunSE()
    {
        audioSource.loop = true;

        audioSource.pitch = 1f;

        audioSource.clip = runSE;

        audioSource.Play();

    }

    public void StopZombieRunSE()
    {
        audioSource.Stop();

        audioSource.loop = false;
    }

    private void StopZombieSE()
    {
        audioSource.Stop();
    }

}
