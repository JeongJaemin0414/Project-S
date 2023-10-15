using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip run;
}

public class NPCBase : MonoBehaviour
{
    public PlayerAnim playerAnim;

    protected Animation anim;

    protected virtual void Start()
    {
        Init();
    }

    public void Init()
    {
        anim = GetComponent<Animation>();

        anim.AddClip(playerAnim.idle, playerAnim.idle.name);
        anim.AddClip(playerAnim.walk, playerAnim.walk.name);
        anim.AddClip(playerAnim.run, playerAnim.run.name);

        anim.clip = playerAnim.idle;
        anim.Play();
    }
}
                 