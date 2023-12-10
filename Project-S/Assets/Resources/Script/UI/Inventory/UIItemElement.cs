using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Spine.Unity.Examples.MixAndMatchSkinsExample;

public class UIItemElement : UIView
{
    [SerializeField] private Image backImage;
    [SerializeField] TextMeshProUGUI itemCount;
    private Image itemImage;


    private int count = 0;
    protected override void OnLocalize() { }

    public void SetData(ItemData data, int count)
    {
        this.count = count;
        backImage.gameObject.SetActive(true);

        switch (data.itemType)
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
                    //단 data 가 프리펩을 말하는지 아이콘을 말하는지는 아직 모름 수정 예정
                    AddressbleManager.Instance.SetSprite(itemImage, data.resourceName);

                    if (this.count > 1)
                    {
                        itemCount.gameObject.SetActive(true);
                        itemCount.text = this.count.ToString();
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

                    AddressbleManager.Instance.SetSprite(itemImage, data.resourceName);
                }
                break;
        }
    }
}