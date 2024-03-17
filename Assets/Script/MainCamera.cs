using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="cameraPoint")
        {
            CameraCheckPoint cameraCheckPoint = collision.GetComponent<CameraCheckPoint>();
             transform.DOLocalMove(cameraCheckPoint.checkPoint, 1f);
        } 
        
    }
}
