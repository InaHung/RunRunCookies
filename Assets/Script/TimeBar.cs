using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Bonus bonus;
    private void Awake()
    {
        slider.maxValue = bonus.bonusTime;
        fill.color = gradient.Evaluate(1f);
        TimeDecreasing();
    }
    private void Update()
    {

    }
    public void TimeDecreasing()
    {
        DOVirtual.Float(bonus.bonusTime, 0, bonus.bonusTime, (currentTime) =>
        {
            
            slider.value = (int)currentTime;
            fill.color = gradient.Evaluate(currentTime / bonus.bonusTime);
        }).SetEase(Ease.Linear);
    }



}
