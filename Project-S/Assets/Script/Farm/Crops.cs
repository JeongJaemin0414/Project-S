using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    public GameObject tomatoPlant;
    public GameObject tomato;

    public GameObject currentTomatoPlant;
    public void SetCrops(CropsData cropsData)
    {
        //cropsData.growthDay[0]
        int time = Utilities.ConvertDayToTime(1);
        TimeManager.Instance.AddTimer(time, OnGrowthCrops);

        Debug.Log("Set Crops!");
    }

    public void OnGrowthCrops()
    {
        currentTomatoPlant = Instantiate(tomatoPlant, gameObject.transform.position, Quaternion.identity);
        currentTomatoPlant.transform.SetParent(gameObject.transform);
    }

    public void HarvestCrops()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPos = new(currentTomatoPlant.transform.position.x + Random.Range(-1f, 1f), currentTomatoPlant.transform.position.y + 0.5f, currentTomatoPlant.transform.position.z + Random.Range(-1f, 1f));
            Instantiate(tomato, randomPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
