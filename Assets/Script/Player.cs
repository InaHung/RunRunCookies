using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpHeight;
    public float slideHeight;
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public float colliderOffset;
    State curState;
    Vector2 originColliderOffset;
    Vector2 originColliderSize;
    public Score score;
    public HpBar hpBar;
    public Map map;
    public void Awake()
    {
        ChangeState(State.Idle);
        originColliderOffset = collider.offset;
        originColliderSize = collider.size;
    }


    void Update()
    {


        switch (curState)
        {
            case State.Idle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
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
        collider.size = new Vector2(collider.size.x, collider.size.y / 2);
        collider.offset = new Vector2(collider.offset.x, colliderOffset);
        ChangeState(State.Slide);

    }



    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            rigidbody.velocity = new Vector2(0, jumpHeight);
            ChangeState(State.Jump);
            Debug.Log(map.transform.position.x);
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
            ReturnToIdle();
            Debug.Log(map.transform.position.x);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "scoreObject")
        {
            Destroy(collision.gameObject);
           Coin coin=collision.gameObject.GetComponent<Coin>();
            score.UpdateScore(coin.point);


        }
        if (collision.transform.tag == "barrier")
        {
            Barrier barrier = collision.gameObject.GetComponent<Barrier>();
            hpBar.DamageSetHp(barrier.damage);
        }   
        if(collision.transform.tag=="heart")
        {
            Heart heart=collision.transform.GetComponent<Heart>();
            hpBar.TreatSetHp(heart.plusHp);
            Destroy(collision.gameObject);
        
        }

    }
    public void ChangeState(State nextState)
    {
        curState = nextState;
        Debug.Log(curState);
    }
}
public enum State
{
    Idle,
    Jump,
    Slide,
    TwiceJump,
}



