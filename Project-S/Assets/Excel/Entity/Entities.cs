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


public class Entities
{

}
