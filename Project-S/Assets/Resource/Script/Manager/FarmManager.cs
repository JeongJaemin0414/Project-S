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

    private Dictionary<int, CropsData> cropsDatas = new ();

    private Dictionary<Vector3, Crops> cropsObjs = new ();

    public override void Init()
    {
        InitCropsData();
    }

    public void InitCropsData()
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

    public bool IsCreateCropsAble(Vector3 cropsPos)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0f, cropsPosZ);

        return !cropsObjs.ContainsKey(cropsPos);
    }

    public void CreateCrops(Vector3 cropsPos)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0f, cropsPosZ);

        Debug.Log("< Create > CropsPos : " + cropsPos);

        if (!cropsObjs.ContainsKey(cropsPos))
        {
            Crops crops = Instantiate(Crops, cropsPos, Quaternion.Euler(0, 180f, 0));
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
        cropsPos = new Vector3(cropsPosX, 0f, cropsPosZ);

        Debug.Log("< Set > CropsPos : " + cropsPos);

        if (cropsObjs.TryGetValue(cropsPos, out Crops crops))
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

    public void HarvestCrops(Vector3 cropsPos)
    {
        float cropsPosX = Mathf.Round(cropsPos.x);
        float cropsPosZ = Mathf.Round(cropsPos.z);
        cropsPos = new Vector3(cropsPosX, 0f, cropsPosZ);

        Debug.Log("< Harvest > CropsPos : " + cropsPos);

        if (cropsObjs.TryGetValue(cropsPos, out Crops crops))
        {
            crops.HarvestCrops();
            cropsObjs.Remove(cropsPos);
        }
        else
        {
            Debug.Log("Empty Crops!");
        }
    }
}
