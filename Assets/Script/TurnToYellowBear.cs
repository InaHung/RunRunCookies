
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnToYellowBear : MonoBehaviour
{
    public Collider2D myCollider;
    public Collider2D objectCollider;
    public Coin yellowBear;
    public SpriteRenderer renderer;
    public Tween delayDestroyTween;
    // public List<string>

    private void Awake()
    {
        objectCollider.enabled = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            renderer.enabled = false;
            myCollider.enabled = false;
            objectCollider.enabled = true;
            delayDestroyTween = DOVirtual.DelayedCall(0.5f, () =>
                {
                    Destroy(transform.gameObject);
                });
        }
        if (collision.gameObject.tag == "jelly")
        { 
            Instantiate(yellowBear, collision.transform.position, transform.rotation, collision.transform.parent);
            Destroy(collision.gameObject);
        }
    }
}
