using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToObjectCollider : MonoBehaviour
{
    public TurnSetting[] turnSettings;
    public Player player;
    public Vector3 myPosition;

    private void Update()
    {
        transform.position = player.transform.position + myPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        foreach (var setting in turnSettings)
        {
            if (collision.transform.tag == setting.tag && collision.gameObject.name != setting.insteadObject.name)
            {

                ScoreObject scoreObject = Instantiate(setting.insteadObject, collision.transform.position, transform.rotation, collision.transform.parent);
                scoreObject.name = setting.insteadObject.name;
                Destroy(collision.gameObject);
            }
        }

    }
}
