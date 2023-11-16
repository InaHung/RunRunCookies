using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public GameObject baseScene;
    public GameObject bonusScene;
    public SpriteRenderer toBaseSprite;
    public SpriteRenderer toBonusSprite;

    public void TransitionToBase()
    {
        baseScene.gameObject.SetActive(true);
        toBaseSprite.gameObject.SetActive(true);
        toBaseSprite.color = new Color(toBaseSprite.color.r, toBaseSprite.color.g, toBaseSprite.color.b, 0);
        toBaseSprite.DOFade(1f, 1f).OnComplete(() =>
        {
            toBonusSprite.gameObject.SetActive(false);
            bonusScene.SetActive(false);
            toBaseSprite.gameObject.SetActive(false);
        });

    }

    public void TransitionToBonus()
    {
        bonusScene.SetActive(true);
        toBonusSprite.gameObject.SetActive(true);
        toBonusSprite.color = new Color(1, 1, 1, 0);
        toBonusSprite.DOFade(1f, 1f).OnComplete(() =>
        {
            baseScene.SetActive(false);

        });
    }
}
