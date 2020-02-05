using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointData : Observable<int>
{
    public static readonly PointData Instance = new PointData();

    public static readonly string ID_PREFIX = "team";
}
