using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public float jumpHeight;

    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public float colliderOffset;
    State curState;
    SpecialState curSpecialState;
    Vector2 originColliderOffset;
    Vector2 originColliderSize;
    Vector2 originPlayerSize;
    public Score score;
    public HpBar hpBar;
    public Map map;
    public GameObject magnet;
    public GameObject turnToBear;
    //public TurnToObjectCollider turnToObjectCollider;  
    Tween turnToNormal;
    Tween turnObjectColliderTween;
    Tween turnObjectColliderTween3;
    public int listIndex;




    public void Awake()
    {
        ChangeState(State.Idle);
        originColliderOffset = collider.offset;
        originColliderSize = collider.size;
        originPlayerSize = transform.localScale;
        turnToBear.SetActive(false);
    }


    void Update()
    {


        switch (curState)
        {
            case State.Idle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    Debug.Log("¸õ" + map.transform.position);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    Slide();
                }

                break;
            case State.Jump:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TwiceJump();
                }
                break;
            case State.Slide:
                if (Input.GetKeyUp(KeyCode.A))
                {
                    ReturnToIdle();
                }
                break;
        }



    }

    public void ReturnToIdle()
    {

        collider.size = originColliderSize;
        collider.offset = originColliderOffset;

        ChangeState(State.Idle);
    }

    public void Slide()
    {
        collider.size = new Vector2(collider.size.x, collider.size.y / 3);
        collider.offset = new Vector2(collider.offset.x, colliderOffset);
        ChangeState(State.Slide);

    }



    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            rigidbody.velocity = new Vector2(0, jumpHeight);
            ChangeState(State.Jump);

        }


    }
    public void TwiceJump()
    {
        rigidbody.velocity = new Vector2(0, jumpHeight);
        ChangeState(State.TwiceJump);



    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "floor")
        {
            Debug.Log("­°¸¨" + map.transform.position);
            ReturnToIdle();

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject")
        {

            ScoreObject coin = collision.gameObject.GetComponent<ScoreObject>();
            score.UpdateScore(coin.point);
            Destroy(collision.gameObject);

        }
        if (collision.transform.tag == "heart")
        {
            Heart heart = collision.transform.GetComponent<Heart>();
            hpBar.TreatSetHp(heart.plusHp);
            Destroy(collision.gameObject);

        }

        /*if (collision.transform.tag == "turnObject")
        {
            TurnObject turnObject = collision.GetComponent<TurnObject>();
            turnToObjectCollider.turnSettings = turnObject.turnSettings;
            turnToObjectCollider.gameObject.SetActive(true);
            if (turnObjectColliderTween != null)
            {
                turnObjectColliderTween.Kill();
            }

            Destroy(collision.gameObject);
            turnObjectColliderTween = DOVirtual.DelayedCall(turnObject.time, () =>
            {
                turnToObjectCollider.gameObject.SetActive(false);
            });
        }*/


        if (collision.transform.tag == "turnToSilver")
        {
            listIndex = 0;
            turnToBear.SetActive(true);
            Destroy(collision.gameObject);
            if (turnObjectColliderTween != null)
            {
                turnObjectColliderTween.Kill();
            }

            turnObjectColliderTween = DOVirtual.DelayedCall(1f, () =>
             {
                 turnToBear.SetActive(false);
             });

        }
        if (collision.transform.tag == "turnToYellowBear")
        {
            listIndex = 1;
            turnToBear.SetActive(true);

            Destroy(collision.gameObject);
            if (turnObjectColliderTween3 != null)
            {
                turnObjectColliderTween3.Kill();
            }
            DOVirtual.DelayedCall(3f, () =>
            {
                turnToBear.SetActive(false);
            });

        }
        if (collision.transform.tag == "turnToBlueBear")
        {
            listIndex = 2;
            turnToBear.SetActive(true);
            Destroy(collision.gameObject);
            if (turnObjectColliderTween3 != null)
            {
                turnObjectColliderTween3.Kill();
            }
            turnObjectColliderTween3 = DOVirtual.DelayedCall(3f, () =>
             {
                 turnToBear.SetActive(false);
             });

        }
        if (collision.transform.tag == "magnet")
        {
            Destroy(collision.gameObject);
            magnet.SetActive(true);
            DOVirtual.DelayedCall(2f, () =>
            {
                magnet.SetActive(false);
            });

        }
        if (collision.transform.tag == "sprint")
        {
            ChangeSpecialState(SpecialState.Sprint);
            Destroy(collision.gameObject);
            map.TimeFast();

            if (turnToNormal != null)
            {
                turnToNormal.Kill();
            }
            turnToNormal = DOVirtual.DelayedCall(2f, () =>
              {
                  ChangeSpecialState(SpecialState.Normal);

              });

        }
        if (collision.transform.tag == "magnify")
        {
            ChangeSpecialState(SpecialState.Magnify);
            transform.localScale = transform.localScale * 2;
            Destroy(collision.gameObject);

            DOVirtual.DelayedCall(4f, () =>
            {
                ChangeSpecialState(SpecialState.Normal);
                transform.localScale = originPlayerSize;
            });

        }
        if (collision.transform.tag == "barrier")
        {
            Barrier barrier = collision.GetComponent<Barrier>();
            if (curSpecialState == SpecialState.Normal)
            {
                hpBar.DamageSetHp(barrier.damage);
                map.TimeSlow();

            }
            else
            {
                score.UpdateScore(barrier.damage);
                Vector3 barrierPosition = collision.transform.position;
                collision.transform.DOMove(barrierPosition + new Vector3(-7f, 2f, 0f), 0.3f)
                .OnComplete(() =>
                {
                    Destroy(collision.gameObject);
                });

            }


        }




    }
    public void ChangeState(State nextState)
    {
        curState = nextState;
        Debug.Log(curState);
    }
    public void ChangeSpecialState(SpecialState nextSpecialState)
    {
        curSpecialState = nextSpecialState;
        Debug.Log(curSpecialState);
    }
}
public enum State
{
    Idle,
    Jump,
    Slide,
    TwiceJump,

}
public enum SpecialState
{
    Normal,
    Sprint,
    Magnify,
}




