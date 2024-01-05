using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    private CropsData cropsData;
    private int cropsCount = 0;
    private bool isWatering = false;
    private bool isGrowUp = false;

    private GameObject currentCropsPlant;

    public Material cropsWaterMaterial;

    public void SetCrops(CropsData newCropsData)
    {
        if (!isWatering)
        {
            cropsData = newCropsData;
            isWatering = true;

            gameObject.GetComponent<MeshRenderer>().material = cropsWaterMaterial;

            int growthTime = Utilities.ConvertDayToTime(cropsData.growthDay[cropsCount]);
            //TimeManager.Instance.AddTimer(1, UpdateCrops);

            Debug.Log("Set Crops!");
        }
        else
        {
            Debug.Log("Already Set Crops !");
        }
    }

    public void UpdateCrops()
    {
        string cropsFileName = cropsData.cropsFileName[cropsCount];

        if (currentCropsPlant != null)
        {
            Destroy(currentCropsPlant);
            currentCropsPlant = null;
        }

        currentCropsPlant = AddressbleManager.Instance.LoadAsset<GameObject>(cropsFileName, gameObject.transform);

        cropsCount++;

        if (cropsCount < 3)  //max Count
        {
            int growthTime = Utilities.ConvertDayToTime(cropsData.growthDay[cropsCount]);
            SeasonType growthSeason = (SeasonType)cropsData.growthSeason[cropsCount];

            //TimeManager.Instance.AddTimer(1, UpdateCrops);

            switch (cropsCount)
            {
                case 2:
                    {
                        isGrowUp = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void HarvestCrops()
    {
        if (isGrowUp)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 randomPos = new(currentCropsPlant.transform.position.x + Random.Range(-1f, 1f), currentCropsPlant.transform.position.y + 0.5f, currentCropsPlant.transform.position.z + Random.Range(-1f, 1f));

                ItemManager.Instance.CreateItem(1020000, randomPos); //tomato
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not GrowUp !");
        }
    }
}
