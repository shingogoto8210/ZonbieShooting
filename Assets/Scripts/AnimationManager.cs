using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    /// <summary>
    /// アニメーションの操作
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="state"></param>
    /// <param name="isSwitch"></param>
    public void PlayAnimation(Animator anim, CharacterState state, bool isSwitch = false)
    {
        switch (state)
        {
            case CharacterState.Fire:
            case CharacterState.Reload:

                anim.SetTrigger(state.ToString());

                break;

            case CharacterState.Attack:
            case CharacterState.Dead:
            case CharacterState.Run:
            case CharacterState.Walk:

                anim.SetBool(state.ToString(), isSwitch);

                break;

        }
    }

    /// <summary>
    /// リセットアニメーション
    /// </summary>
    public void TurnOffTrigger(Animator anim)
    {
        anim.SetBool(CharacterState.Walk.ToString(), false);
        anim.SetBool(CharacterState.Attack.ToString(), false);
        anim.SetBool(CharacterState.Run.ToString(), false);
    }




}
