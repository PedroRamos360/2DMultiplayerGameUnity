using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicYSort : MonoBehaviour
{
    private int baseSortingOrder;
    [SerializeField] private List<SortableSprite> sortableSprites;
    [SerializeField] private float sortingOffest;

    void Update()
    {
        baseSortingOrder = transform.GetSortingOrder(sortingOffest);
        foreach (SortableSprite sortableSprite in sortableSprites)
        {
            sortableSprite.spriteRenderer.sortingOrder = baseSortingOrder + sortableSprite.relativeOrder;
        }
    }

    [Serializable]
    public struct SortableSprite
    {
        public SpriteRenderer spriteRenderer;
        public int relativeOrder;
    }
}
