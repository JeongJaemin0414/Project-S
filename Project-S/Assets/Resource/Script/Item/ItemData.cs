//아이템 데이터. 만약 아이템 자체에 대한 정보라면 ItemData 보다 ItemInfo가 더 좋을것같음.
public struct ItemData
{
    public string name;
    public string desc;
    public string iconResourceName;
    public string modelResourceName;
    public ItemType itemType;
    public int[] craftMaterial;
    public int[] materialValue;
    public int buyGold;
    public bool salePossible;
    public int saleGold;
    public int invenMaxCount;
}