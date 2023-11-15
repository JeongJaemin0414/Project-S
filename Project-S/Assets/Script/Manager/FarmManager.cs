using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CropsData
{
    public GameObject[] crops;
    public string name;
    public int growhDay;
    public int[] growthSeason;
    public int harvestItem;
}

public class FarmManager : Singleton<FarmManager>
{
    public GameObject Crops;
    public override void Init()
    {

    }

    public void CreateCrops(Vector3 cropsPos)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0, cropsPosZ);

        Debug.Log("CropsPos : " + cropsPos);

        Instantiate(Crops, cropsPos, Quaternion.identity);
    }
}
