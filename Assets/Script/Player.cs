using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public float jumpHeight;
    public float bonusjump;
    public float bonusDown;
    public float originGravity;
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public float colliderOffset;
    State curState;
    SpecialState curSpecialState;
    Vector2 originColliderOffset;
    Vector2 originColliderSize;
    Vector2 originPlayerSize;
    float originGravityScale;
    public Score score;
    public HpBar hpBar;
    public Map map;
    public GameObject magnet;
    public TurnToObjectCollider turnToObjectCollider;
    public SceneController sceneController;
    Tween turnToNormalTween;
    Tween turnObjectColliderTween;
    public Animator playerAnimator;
    public int listIndex;





    public void Awake()
    {
        ChangeState(State.Idle);
        originColliderOffset = collider.offset;
        originColliderSize = collider.size;
        originPlayerSize = transform.localScale;
        sceneController.OnTransitionToBase += () =>
        {
            ChangeState(State.Idle);
            rigidbody.gravityScale = originGravity;
            transform.position += new Vector3(0, 5f);
        };
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeState(State.Bonus);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            sceneController.TransitionToBase();
        }

        switch (curState)
        {
            case State.Idle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    Debug.Log("¸õ");
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
                    playerAnimator.SetTrigger("Walk");
                    Debug.Log("walk");
                    ReturnToIdle();
                }
                break;
            case State.Bonus:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidbody.velocity = new Vector2(0f, bonusjump);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    rigidbody.velocity = new Vector2(0, bonusDown);
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
        playerAnimator.SetTrigger("Slide");
        ChangeState(State.Slide);

    }



    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            playerAnimator.SetTrigger("Jump");
            rigidbody.velocity = new Vector2(0, jumpHeight);
            ChangeState(State.Jump);

        }
    }
    public void TwiceJump()
    {
        rigidbody.velocity = new Vector2(0, jumpHeight);
        ChangeState(State.TwiceJump);
        playerAnimator.SetTrigger("TwiceJump");
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "floor" && curState != State.Slide && curState != State.Idle)
        {
            playerAnimator.SetTrigger("Walk");
            ReturnToIdle();

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject"|| collision.transform.tag == "jelly")
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

        if (collision.transform.tag == "turnObject")
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
            float sprintTime = collision.GetComponent<Sprint>().sprintTime;
            ChangeSpecialState(SpecialState.Sprint);
            Destroy(collision.gameObject);
            map.TimeFast(sprintTime);

            if (turnToNormalTween != null)
            {
                turnToNormalTween.Kill();
            }

            turnToNormalTween = DOVirtual.DelayedCall(sprintTime, () =>
              {
                  ChangeSpecialState(SpecialState.Normal);

              });

        }
        if (collision.transform.tag == "magnify")
        {
            float magnifyTime = collision.GetComponent<Magnify>().magnifyTime;
            ChangeSpecialState(SpecialState.Magnify);
            transform.localScale = transform.localScale * 2;
            Destroy(collision.gameObject);

            DOVirtual.DelayedCall(magnifyTime, () =>
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
        if (collision.transform.tag == "bonus")
        {
            playerAnimator.SetTrigger("Bonus");
            ChangeState(State.Bonus);
            sceneController.TransitionToBonus();
            originGravity = rigidbody.gravityScale;
            rigidbody.gravityScale = 0;
            Destroy(collision.gameObject);
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
    Bonus,
}
public enum SpecialState
{
    Normal,
    Sprint,
    Magnify,

}




