using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CropsData
{
    public string[] cropsFileName;
    public string name;
    public int[] growthDay;
    public int[] growthSeason;
    public int harvestItem;
}

public class FarmManager : Singleton<FarmManager>
{
    public Crops Crops;

    public Dictionary<int, CropsData> cropsDatas = new Dictionary<int, CropsData>();

    public Dictionary<Vector3, Crops> cropsObjs = new Dictionary<Vector3, Crops>();

    public override void Init()
    {
        List<CropsTableEntity> cropsTableEntities = ExcelManager.Instance.GetExcelData<CropsTable>().crops;

        foreach (CropsTableEntity cropsTableEntity in cropsTableEntities)
        {
            CropsData cropsData = new()
            {
                cropsFileName = Utilities.GetArrayDataString(cropsTableEntity.fileName),
                //name = LanguageManager.Instance.GetString(cropsTableEntity.name),
                growthDay = Utilities.GetArrayDataInt(cropsTableEntity.growthDay),
                growthSeason = Utilities.GetArrayDataInt(cropsTableEntity.growthSeason),
                harvestItem = cropsTableEntity.harvestItem
            };

            cropsDatas.Add(cropsTableEntity.index, cropsData);
        }
    }

    public void CreateCrops(Vector3 cropsPos)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0, cropsPosZ);

        Debug.Log("CropsPos : " + cropsPos);

        if(!cropsObjs.ContainsKey(cropsPos))
        {
            Crops crops = Instantiate(Crops, cropsPos, Quaternion.identity);
            cropsObjs[cropsPos] = crops;
        }
        else
        {
            Debug.Log("Already Crops!");
        }
    }

    public void SetCrops(Vector3 cropsPos, int cropsIndex)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0, cropsPosZ);

        Debug.Log("CropsPos : " + cropsPos);

        if(cropsObjs.TryGetValue(cropsPos, out Crops crops))
        {
            if (cropsDatas.TryGetValue(cropsIndex, out CropsData cropsData))
            {
                crops.SetCrops(cropsData);
            }
        }
        else
        {
            Debug.Log("Empty Crops!");
        }
    }

    public void OnCrops(Vector3 cropsPos)
    {
        SetCrops(cropsPos, 1001);
    }
}
