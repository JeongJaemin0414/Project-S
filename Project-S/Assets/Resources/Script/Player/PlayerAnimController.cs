using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip run;
    public AnimationClip dig;
    public AnimationClip gather;
    public AnimationClip pull;
    public AnimationClip seed;
    public AnimationClip fishingIdle;
    public AnimationClip fishing;
    public AnimationClip ground; 
    public AnimationClip water; 
}

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField]
    private PlayerAnim playerAnim;

    [Header("Animation Component")]
    public Animation anim;

    [SerializeField] 
    private SerializableDictionary<PlayerToolType, string> playerHeadAnimName;
    public void Init()
    {
        if(anim != null)
        {
            anim.AddClip(playerAnim.idle, playerAnim.idle.name);
            anim.AddClip(playerAnim.walk, playerAnim.walk.name);
            anim.AddClip(playerAnim.run, playerAnim.run.name);
            anim.AddClip(playerAnim.dig, playerAnim.dig.name);
            anim.AddClip(playerAnim.gather, playerAnim.gather.name);
            anim.AddClip(playerAnim.pull, playerAnim.pull.name);
            anim.AddClip(playerAnim.seed, playerAnim.seed.name);
            anim.AddClip(playerAnim.fishingIdle, playerAnim.fishingIdle.name);
            anim.AddClip(playerAnim.fishing, playerAnim.fishing.name);
            anim.AddClip(playerAnim.ground, playerAnim.ground.name);
            anim.AddClip(playerAnim.water, playerAnim.water.name);
        }
    }

    public void PlayAnimCrossFade(string animName, float fadeLength, PlayerToolType playerToolType = PlayerToolType.Idle)
    {
        string totalAnimName = "Aro_" + playerHeadAnimName[playerToolType] + animName;

        Debug.Log("AnimName : " + totalAnimName);

        anim.CrossFade(totalAnimName, fadeLength);
    }
}
