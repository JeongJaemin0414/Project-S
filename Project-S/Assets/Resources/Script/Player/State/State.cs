using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct StateData
{
    public Animation anim;
    public Action onActionEnd;
}

public abstract class State : MonoBehaviour
{
    protected StateData stateData;

    public void Init(StateData _stateData)
    {
        stateData = _stateData;
    }

    public Vector3 GetMousePointinDistance()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float maxOffset = 4f;

                float xPos = Mathf.Clamp(hit.point.x, transform.position.x - maxOffset, transform.position.x + maxOffset);
                float zPos = Mathf.Clamp(hit.point.z, transform.position.z - maxOffset, transform.position.z + maxOffset);

                Debug.Log("xPos : " + xPos);
                Debug.Log("zPos : " + zPos);

                return new Vector3(xPos, hit.point.y, zPos);
            }
        }

        return Vector3.zero;
    }

    public void LookTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
