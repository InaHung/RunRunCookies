using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
   
    public CameraCheckPoint cameraCheckPoint;
    public Vector3 startPosition;

    private void Awake()
    {
        //transform.position = startPosition;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="cameraPoint")
        {
            transform.DOLocalMove(cameraCheckPoint.checkPoint, 1f);
        } 
        
    }
}
