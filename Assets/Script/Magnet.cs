using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Magnet : MonoBehaviour
{
    public Score score;
    public HpBar hpBar;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject"|| collision.transform.tag == "heart")
        {
            string objName = collision.name;
            Tween tween = collision.transform.DOMove(transform.position, 0.5f);
            tween.OnUpdate(() =>
            {
                if (collision == null)
                    Debug.LogError("Null OBJ:" + objName);
            });
            tween.OnComplete(() =>
            {
                Destroy(collision.gameObject);
            });

            hpBar.TreatSetHp(collision.transform.GetComponent<Heart>().plusHp);
            
            score.UpdateScore(collision.transform.GetComponent<ScoreObject>().point);
        }
    }

}
