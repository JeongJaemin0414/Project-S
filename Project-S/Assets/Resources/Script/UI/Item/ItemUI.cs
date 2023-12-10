using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

    
public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemCount;

    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void SetItemUI(ItemType itemType, string itemImageName, int itemCountValue)
    {
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
