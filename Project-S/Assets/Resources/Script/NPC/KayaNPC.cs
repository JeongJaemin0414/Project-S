using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaNPC : NPCBase
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.CrossFade(npcAnim.idle.name, 0.3f);
        }   
        else if (Input.GetKeyDown(KeyCode.I))
        {
            anim.CrossFade(npcAnim.walk.name, 0.3f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            anim.CrossFade(npcAnim.run.name, 0.3f);
        }
    }

}
