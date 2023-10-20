using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour

{
    public int score;
    
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        
        score = 0;
    }
    public void UpdateScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }


}
