using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public GameObject baseScene;
    public GameObject bonusScene;
    public SpriteRenderer toBaseRenderer;
    public SpriteRenderer toBonusRenderer;

    public void TransitionToBase()
    {

        toBaseRenderer.gameObject.SetActive(true);
        toBaseRenderer.color = new Color(toBaseRenderer.color.r, toBaseRenderer.color.g, toBaseRenderer.color.b, 0);
        toBaseRenderer.DOFade(1f, 1f).OnComplete(() =>
        {
            baseScene.gameObject.SetActive(true);
            toBonusRenderer.gameObject.SetActive(false);
            bonusScene.gameObject.SetActive(false);
            toBaseRenderer.gameObject.SetActive(false);
        });

    }

    public void TransitionToBonus()
    {
        bonusScene.gameObject.SetActive(true);
        toBonusRenderer.gameObject.SetActive(true);
        toBonusRenderer.color = new Color(1, 1, 1, 0);
        toBonusRenderer.DOFade(1f, 1f).OnComplete(() =>
        {
            baseScene.SetActive(false);

        });

    }
}
