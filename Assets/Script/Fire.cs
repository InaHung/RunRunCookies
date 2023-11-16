using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fire : MonoBehaviour
{
    public Collider2D myCollider;
    public Collider2D destroyCollider;
    public SpriteRenderer renderer;
    Tween delayDestroy;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "player")
        {
            myCollider.enabled = false;
            destroyCollider.enabled = true;
            renderer.enabled = false;
            if(delayDestroy!=null)
            {
                delayDestroy.Kill();
            }

            delayDestroy=DOVirtual.DelayedCall(1f,()=>
            {
                Destroy(transform.gameObject);
            });
        }
        if(collision.transform.tag=="barrier")
        {
            Destroy(collision.gameObject);
        }

    }
}
