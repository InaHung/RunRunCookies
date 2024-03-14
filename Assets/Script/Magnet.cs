using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Magnet : MonoBehaviour
{
    public Score score;
    public HpBar hpBar;
    public Player player;
    public Vector3 myPosition;

    private void Update()
    {
        transform.position = player.transform.position + myPosition;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject")
        {
            Tween tween = collision.transform.DOMove(transform.position, 0.5f);
            tween.OnComplete(() =>
            {
                Destroy(collision.gameObject);
            }); 
            score.UpdateScore(collision.transform.GetComponent<ScoreObject>().point);
        }
        if (collision.transform.tag == "heart")
        {
            Tween tween = collision.transform.DOMove(transform.position, 0.5f);
            tween.OnComplete(() =>
            {
                Destroy(collision.gameObject);
            });
            hpBar.TreatSetHp(collision.transform.GetComponent<Heart>().plusHp);
        }
    }

}
