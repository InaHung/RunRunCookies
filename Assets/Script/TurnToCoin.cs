using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TurnToCoin : MonoBehaviour
{
    public Collider2D myCollider;
    public Collider2D barrierCollider;
    public SpriteRenderer renderer;
    public Coin coin;

    public void Awake()
    {
        barrierCollider.enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "player")
        {
            barrierCollider.enabled = true;
            myCollider.enabled = false;
            renderer.enabled = false;
            DOVirtual.DelayedCall(5f, () =>
            {
                Destroy(gameObject);
            });
        }
        if(collision.transform.tag=="barrier")
        {
            Instantiate(coin,collision.transform.position, transform.rotation,collision.transform.parent);
            Destroy(collision.gameObject);
        }
    }

}
