using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static int GetSortingOrder(this Transform transform, float offset = 0)
    {
        return Mathf.RoundToInt((transform.position.y + offset) * -100);
    }
}