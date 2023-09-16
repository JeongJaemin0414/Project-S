using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DialogTable : ExcelBase
{
	public List<DialogTableEntity> dialog; // Replace 'EntityType' to an actual type that is serializable.
}
