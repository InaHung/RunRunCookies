using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public Map map;
    private void Awake()
    {
        //map.MapMove();
    }


    public int sorting = 500;
    [ContextMenu("ChangeSorting")]
    public void ChangeSorting()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(var renderer in spriteRenderers)
        {
            renderer.sortingOrder = sorting;
        }
    }
}
