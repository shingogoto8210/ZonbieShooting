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

    private CharacterState state = CharacterState.Idle;

    private float walkSpeed = 1.0f, runSpeed = 3.0f;

    public int attackDamage;

    private void Start()
    {
        if (!TryGetComponent(out enemyCol)) Debug.Log("collider未取得");
        if (!TryGetComponent(out agent)) Debug.Log("NavMeshAgent未取得");
        if (!TryGetComponent(out anim)) Debug.Log("Animator未取得");
        if (!TryGetComponent(out animationManager)) Debug.Log("AnimationManager未取得");
        if (!GameObject.FindGameObjectWithTag("Player").TryGetComponent(out target)) Debug.Log("AnimationManager未取得");
        railMoveController = target.transform.gameObject.GetComponent<RailMoveController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        switch (state)
        {
            case CharacterState.Idle:

                //アニメーションをリセット
                animationManager.TurnOffTrigger(anim);
                //目的地リセット
                agent.ResetPath();

                //確率で状態をWALKに変更
                if (Random.Range(0, 5000) < 5)
                {
                    state = CharacterState.Walk;
                }

                //敵の距離が15より小さいとき走って追跡
                if (DistanceToPlayer() < 30)
                {
                    state = CharacterState.Run;
                }

                break;

            case CharacterState.Walk:

                //確率で状態をIDLEに変更
                if (Random.Range(0, 5000) < 5)
                {
                    state = CharacterState.Idle;
                }

                //敵の距離が15より小さいとき走って追跡
                if (DistanceToPlayer() < 30)
                {
                    agent.ResetPath();
                    state = CharacterState.Run;
                }

                //敵の距離が15より大きいときはプレイヤーを見失う
                else
                {
                    //敵の目的地が設定されていないとき
                    if (!agent.hasPath)
                    {
                        //新たな目的地をランダムで設定
                        Vector3 nextPos = new Vector3(transform.position.x + Random.Range(-30, 30), transform.position.y, transform.position.z + Random.Range(-30, 30));
                        agent.SetDestination(nextPos);
                        agent.speed = walkSpeed;
                        animationManager.TurnOffTrigger(anim);
                        animationManager.PlayAnimation(anim, CharacterState.Walk, true);
                    }

                }
                break;

            case CharacterState.Run:


                //敵の距離が３より小さいとき
                if (DistanceToPlayer() < agent.stoppingDistance)
                {
                    Debug.Log("敵に捕まった");

                    //プレイヤーが動いているとき
                    if (railMoveController.isStop == false)
                    {
                        railMoveController.Stop();
                    }
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
                    agent.ResetPath();
                    state = CharacterState.Walk;
                }
                break;


            case CharacterState.Attack:

                animationManager.TurnOffTrigger(anim);
                animationManager.PlayAnimation(anim, CharacterState.Attack, true);

                if (DistanceToPlayer() >= agent.stoppingDistance)
                {
                    state = CharacterState.Run;
                }

                break;

            case CharacterState.Dead:

                

                Destroy(agent);

                break;

        }
    }

    /// <summary>
    /// 敵の設定
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
    /// 敵を倒す
    /// </summary>
    /// <param name="gameManager"></param>
    public void DestroyEnemy(GameManager gameManager)
    {
        enemyCol.enabled = false;
        animationManager.TurnOffTrigger(anim);
        animationManager.PlayAnimation(anim, CharacterState.Dead, true);
        state = CharacterState.Dead;

        //イベントではなく、プレイヤーが止まっているとき（敵に捕まっているとき）
        if (railMoveController.isStop == true && gameManager.currentGameState == GameState.Move)
        {
            //敵を倒したらまた動ける
            railMoveController.Resume();
        }

        if (gameManager.currentGameState == GameState.Event)
        {
            gameManager.enemiesList.Remove(this);
            gameManager.CheckFinishEvent();
        }
    }

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    public void DamagePlayer()
    {
        if (target != null)
        {
            target.GetComponent<PlayerController>().TakeHit(attackDamage);
        }
    }

    /// <summary>
    /// 敵とプレイヤーの間の距離を計算する
    /// </summary>
    /// <returns></returns>
    private float DistanceToPlayer()
    {
        return Vector3.Distance(target.gameObject.transform.position, transform.position);
    }

}
