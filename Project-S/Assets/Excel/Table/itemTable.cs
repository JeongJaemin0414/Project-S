using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class itemTable : ExcelBase
{
	public List<ItemTableEntity> item; // Replace 'EntityType' to an actual type that is serializable.
}
