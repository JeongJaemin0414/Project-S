using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class TimeTable : ExcelBase
{
	public List<TimeTableEntity> time; // Replace 'EntityType' to an actual type that is serializable.
}
