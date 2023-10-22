using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    None,
    Goods,
    Tool,
    Food,
    Seed,
    Deco,
}

[Serializable]
public enum InventoryItemType
{
    Item,
    Tool,
}

public enum Tool
{
    Axe = 1,
    Pickaxe,
    Shovel,
}

public enum IllustLocation
{
    Left,
    Right
}

public enum IllustAppear
{
    FadeIn,
    FadeOut,
    Stay,
}

public enum LauguageType
{
    kor,
    eng,
}

public enum SeasonType
{
    spring,
    summer,
    fall,
    winter
}

public class EnumUtil
{
    
}
