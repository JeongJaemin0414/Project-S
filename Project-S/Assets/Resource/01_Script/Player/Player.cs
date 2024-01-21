using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform pivotTooltr;
    private GameObject toolObj;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnTool(PlayerToolType playerToolType)
    {
        GameObject Obj = ToolManager.Instance.GetTool(playerToolType);

        if(toolObj != null)
        {
            Destroy(toolObj);
            toolObj = null;
        }

        if(toolObj != Obj)
            toolObj = Instantiate(Obj, pivotTooltr);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "item")
        {
            ItemBase itemBase = hit.transform.GetComponent<ItemBase>();

            InventoryItemData inventoryItemData = new()
            {
                itemIndex = itemBase.Itemindex,
                itemCount = 1
            };

            InventoryManager.Instance.AddInventoryItemData(inventoryItemData);

            Destroy(hit.gameObject);
        }
    }
}
