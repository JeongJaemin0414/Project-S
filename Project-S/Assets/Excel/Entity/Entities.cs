using System;

[Serializable]
public class NpcTableEntity
{
    public int index;
    public int charName;
    public string illustFileName;
}

[Serializable]
public class DialogTableEntity
{
    public int index;
    public int group;
    public int charIndex;
    public int dec;
    public int illustLocation;
    public int illustAppear;
}

[Serializable]
public class LanguageTableEntity
{
    public int index;
    public string korLanguage;
    public string engLanguage;
}

[Serializable]
public class TimeTableEntity
{
    public int index;
    public int timePass;
    public int maxDay;
    public string weatherType; //[]
    public string weatherPercent; //[]
}

[Serializable]
public class ToolTableEntity
{
    public int index;
    public int toolType;
    public bool volumeToolType;
    public float volumeMax; 
    public float consumeValue;
    public float chargeValue;
}

[Serializable]
public class ItemTableEntity
{
    public int index;
    public int name;
    public int desc;
    public int itemType;
    public string craftMaterial;
    public string materialValue;
    public int buyGold;
    public bool salePossible;
    public int saleGold;
}

[Serializable]
public class CropsTableEntity
{
    public int index;
    public int name;
    public string growthDay; //[]
    public string fileName; //[]
    public string growthSeason; //[]
    public int harvestItem;
    public string harvestItemValue; //[]
    public string harvestItemPercent; //[]
}


public class Entities
{

}
