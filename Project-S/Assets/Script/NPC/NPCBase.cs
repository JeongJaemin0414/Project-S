using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NPCAnim
{
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip run;
}

public class NPCBase : MonoBehaviour
{
    public NPCAnim npcAnim;

    protected Animation anim;

    protected virtual void Start()
    {
        Init();
    }

    public void Init()
    {
        anim = GetComponent<Animation>();

        anim.AddClip(npcAnim.idle, npcAnim.idle.name);
        anim.AddClip(npcAnim.walk, npcAnim.walk.name);
        anim.AddClip(npcAnim.run, npcAnim.run.name);

        anim.clip = npcAnim.idle;
        anim.Play();
    }
}
                 