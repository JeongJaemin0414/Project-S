using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    
public class ItemUI : MonoBehaviour
{
    public Image backImage;
    public Image itemImage;
    public TextMeshProUGUI itemCount;

    public void SetItemUI(ItemType itemType, string itemImageName, int itemCountValue)
    {
        backImage.gameObject.SetActive(true);

        switch (itemType)
        {
            case ItemType.None:
                {
                    itemImage.gameObject.SetActive(false);
                    itemCount.gameObject.SetActive(false);
                }
                break;
            case ItemType.Goods:
            case ItemType.Food:
            case ItemType.Seed:
                {
                    itemImage.gameObject.SetActive(true);
                    AddressbleManager.Instance.SetSprite(itemImage, itemImageName);

                    if (itemCountValue > 1)
                    {
                        itemCount.gameObject.SetActive(true);
                        itemCount.text = itemCountValue.ToString();
                    }
                    else
                        itemCount.gameObject.SetActive(false);
                }
                break;
            case ItemType.Tool:
            case ItemType.Deco:
                {
                    itemImage.gameObject.SetActive(true);
                    itemCount.gameObject.SetActive(false);

                    AddressbleManager.Instance.SetSprite(itemImage, itemImageName);
                }
                break;
        }
    }
}
