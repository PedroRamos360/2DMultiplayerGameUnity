using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static int GetSortingOrder(this Transform transform, float offsetY = 0)
    {
        return -(int)((transform.position.y + offsetY) * 100);
    }
}