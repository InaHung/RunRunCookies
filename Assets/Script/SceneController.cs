using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SceneController : MonoBehaviour
{
    public Map map;
    public HpBar hp;
    public Bonus bonusScene;
    public SpriteRenderer toBaseRenderer;
    public SpriteRenderer toBonusRenderer;
    public Action OnTransitionToBase;


    private void Awake()
    {
        bonusScene.onBonusCountEnd += TransitionToBase;
    }
    public void TransitionToBase()
    {
       
        toBaseRenderer.gameObject.SetActive(true);
        toBaseRenderer.color = new Color(toBaseRenderer.color.r, toBaseRenderer.color.g, toBaseRenderer.color.b, 0);
        toBaseRenderer.DOFade(1f, 1f).OnComplete(() =>
        {
            foreach(var scene in map.aliveScenes)
            {
                scene.isbonus = false;
            }
            map.gameObject.SetActive(true);
            hp.gameObject.SetActive(true);
            toBonusRenderer.gameObject.SetActive(false);
            bonusScene.gameObject.SetActive(false);
            toBaseRenderer.gameObject.SetActive(false);
            OnTransitionToBase();
        });

    }

    public void TransitionToBonus()
    {
        bonusScene.gameObject.SetActive(true);
        toBonusRenderer.gameObject.SetActive(true);
        foreach (var scene in map.aliveScenes)
        {
            scene.isbonus = true;
        }
        map.gameObject.SetActive(false);
        hp.gameObject.SetActive(false);
        toBonusRenderer.color = new Color(1, 1, 1, 0);  //ÅÜ³z©ú
        toBonusRenderer.DOFade(1f, 0.5f);

    }
}
