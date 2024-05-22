using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> childSprites;
    [SerializeField] private bool fatherHasSpriteRenderer = true;
    [SerializeField] private float sortingOffset;
    void Start()
    {
        int sortingOrder = transform.GetSortingOrder(sortingOffset);
        if (fatherHasSpriteRenderer)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = sortingOrder;
        }
        foreach (SpriteRenderer childSprite in childSprites)
        {
            childSprite.sortingOrder = sortingOrder;
        }
    }
}
