using System;

[Serializable]
public class CharacterTableEntity
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

public class Entities
{

}
