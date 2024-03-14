using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Scene : MonoBehaviour
{
    public Action OnEnterCheckPoint;
    public Action<Scene> onDestroyScene;
    public int sceneListIndex;
    public float radius = 15f;
    public bool isbonus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {
            if (OnEnterCheckPoint != null)
                OnEnterCheckPoint();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint"&&!isbonus)
        {
            DOVirtual.DelayedCall(1f, () =>
             {
                 onDestroyScene(this);
                 Destroy(gameObject);
             });
            
        }

    }

   
}


