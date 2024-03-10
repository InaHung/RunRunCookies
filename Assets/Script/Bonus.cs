using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class Bonus : MonoBehaviour
{
    public Map map;
    public float bonusTime;
    public Action onBonusCountEnd;

    private void Awake()
    {
        BonusCount();
    }
    private void Update()
    {
        transform.position -= new Vector3(map.moveSpeed * Time.deltaTime, 0, 0);
    }
    public void BonusCount()
    {
        DOVirtual.DelayedCall(bonusTime, () =>
        {
            onBonusCountEnd();
        });
    }
    
}
