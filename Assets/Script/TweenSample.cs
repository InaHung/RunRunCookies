using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenSample : MonoBehaviour
{

    public int currentHP;
    public Tween delayTween;
    public Tween floatTween;

    private void Awake()
    {
        //3秒後執行
        delayTween = DOVirtual.DelayedCall(3f, () =>
        {
            Debug.Log("三秒後執行的東西");
        });

        //刪除這個Tween做的事情
        delayTween.Kill();
        //暫停這個Tween做的事情
        delayTween.Pause();
        //繼續執行這個Tween做的事情
        delayTween.Play();


        //            (開始數字,結束數字,花費秒數,(變數)=>{})
        floatTween = DOVirtual.Float(100f, 0f, 30f, (hp) =>
        {
            // hp 是一個變數，他會每一禎都在裡面跑一次誇號內程式碼，hp就會代表現在數字是多少了
            currentHP = (int)hp;
        });


        //這個Tween結束後要執行的東西
        floatTween.OnComplete(() =>
        {
            Debug.Log("倒數結束了");
        });

        //本地移動(目標位置,秒數)
        transform.DOLocalMove(transform.localPosition + new Vector3(0, 5f, 0), 3f);



    }


}
