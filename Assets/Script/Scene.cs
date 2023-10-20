using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scene : MonoBehaviour
{
    public Action OnEnterCheckPoint;
    public int sceneListIndex;
    public float radius = 15f;
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
}


