using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayerMoveStop = false;

    public void SetPlayerMoveStop(bool isStop)
    {
        isPlayerMoveStop = isStop;
    }


}
