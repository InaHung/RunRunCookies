using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Magnet : MonoBehaviour
{ public Score score;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject")
        {
            collision.transform.DOMove(transform.position, 0.5f);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                Destroy(collision.gameObject);
            });
            score.UpdateScore(collision.transform.GetComponent<Coin>().point);
        }
    }
    
}
