using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public Map map;
    public float bonusTime;

    private void Update()
    {
        transform.position -= new Vector3(map.moveSpeed * Time.deltaTime, 0, 0);
    }
    
}
