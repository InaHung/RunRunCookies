using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Scene : MonoBehaviour
{
    public Action OnEnterCheckPoint;
    public int sceneListIndex;
    public float radius = 15f;
    public SpriteRenderer[] allSpriteRenderers;

    private void Awake()
    {
    }


    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {

            Debug.LogWarning(collision.name + " , " + name);
            if (OnEnterCheckPoint != null)
                OnEnterCheckPoint();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {
            Destroy(gameObject);
        }

    }

    public void DoFade(float alpha, Action _callBack = null)
    {
        foreach (var renderer in allSpriteRenderers)
        {
            if (renderer != null)
            {
                renderer.DOFade(alpha, 1f).OnComplete(() =>
                {
                    if (_callBack != null)
                        _callBack();
                });

            }
        }
    }

    [ContextMenu("Get All SpriteRenderers")]
    public void GetAllRenderers()
    {
        allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
}


