using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public int maxHp;
    public int curHp;
    public Tween hpDecreasingTween;

    private void Awake()
    {
        curHp = maxHp;
        slider.maxValue = maxHp;
        slider.value = maxHp;
        HpDecreasing();
    }
    public void Update()
    {
       
    }
    public void DamageSetHp(int damage)
    {
        curHp -= damage;
        slider.value = curHp;
        hpDecreasingTween.Kill();
        HpDecreasing();
    }
    public void TreatSetHp(int treat)
    {

        curHp += treat;
        curHp = maxHp;
        slider.value = curHp;
        hpDecreasingTween.Kill();
        HpDecreasing();

    }
    public void HpDecreasing()
    {
        hpDecreasingTween=DOVirtual.Float(curHp, 0, curHp * 3, (hp) =>

             {
                 curHp = (int)hp;
                 slider.value = hp;
             } );
     }
    }
