using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip run;
    public AnimationClip fishingIdle;
    public AnimationClip fishing;
    public AnimationClip ground; 
    public AnimationClip water; 
}

public class PlayerAnimController : MonoBehaviour
{
    public PlayerAnim playerAnim;

    [Header("Animation Component")]
    public Animation anim;

    public void Init()
    {
        if(anim != null)
        {
            anim.AddClip(playerAnim.idle, playerAnim.idle.name);
            anim.AddClip(playerAnim.walk, playerAnim.walk.name);
            anim.AddClip(playerAnim.run, playerAnim.run.name);
            anim.AddClip(playerAnim.fishingIdle, playerAnim.fishingIdle.name);
            anim.AddClip(playerAnim.fishing, playerAnim.fishing.name);
            anim.AddClip(playerAnim.ground, playerAnim.ground.name);
            anim.AddClip(playerAnim.water, playerAnim.water.name);
        }
    }

    public void PlayAnimCrossFade(string animName, float fadeLength)
    {
        anim.CrossFade(animName, fadeLength);
    }
}
