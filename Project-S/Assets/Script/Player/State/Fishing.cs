using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : State
{

    private Vector3 createCropsPos;
    public override void EnterState()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Debug.Log("Mouse Pos : " + hit.point);

                float maxOffset = 4f;

                float xPos = Mathf.Clamp(hit.point.x, transform.position.x - maxOffset, transform.position.x + maxOffset);
                float zPos = Mathf.Clamp(hit.point.z, transform.position.z - maxOffset, transform.position.z + maxOffset);

                Debug.Log("xPos : " + xPos);
                Debug.Log("zPos : " + zPos);

                createCropsPos = new Vector3(xPos, hit.point.y, zPos);
                // Use restrictedPos for whatever purpose it serves
            }
        }
    }

    public override void UpdateState()
    {
        if (!stateData.anim.isPlaying)
        {
            FarmManager.Instance.CreateCrops(createCropsPos);
        
            stateData.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }


}