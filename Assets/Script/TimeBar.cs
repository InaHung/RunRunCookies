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
    public float maxTime;
    public float duringTime;
    private void Awake()
    {
        slider.maxValue = maxTime;
        fill.color = gradient.Evaluate(1f);
        TimeDecreasing();
    }
    private void Update()
    {
        
    }
    public void TimeDecreasing()
    {
        DOVirtual.Float(maxTime, 0, duringTime, (currentTime) =>
        {
            slider.value = (int)currentTime;
            fill.color = gradient.Evaluate(currentTime);
        });
    }
    


}
