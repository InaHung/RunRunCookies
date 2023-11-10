
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnToBear : MonoBehaviour
{


    public Player player;

    public List<ScoreObject> turnBearCoin = new List<ScoreObject>();

    private void Awake()
    {
       
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "barrier")
        {
            Instantiate(turnBearCoin[player.listIndex], collision.transform.position, transform.rotation, collision.transform.parent);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "jelly")
        {

            Instantiate(turnBearCoin[player.listIndex], collision.transform.position, transform.rotation, collision.transform.parent);

            Destroy(collision.gameObject);

        }




    }
}
