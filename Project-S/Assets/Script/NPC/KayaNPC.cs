using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaNPC : NPCBase
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);
        }   
        else if (Input.GetKeyDown(KeyCode.I))
        {
            anim.CrossFade(playerAnim.walk.name, 0.3f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            anim.CrossFade(playerAnim.run.name, 0.3f);
        }
    }

}
