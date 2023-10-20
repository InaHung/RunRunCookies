using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinSpawner : MonoBehaviour
{

    public GameObject coinBase;

    [Header("Linear")]
    public Vector2 startPos_Linear;
    public Vector2 offset_Linear;
    public int spawnCount_Linear;

    [Header("Square")]
    public Vector2 startPos_Square;
    public Vector2 offset_Square;
    public Vector2 spawnCount_Square;

    [Header("Curve")]
    public Vector2 startPos_Curve;
    public Vector2 top_Curve;
    public Vector2 endPos_Curve;
    public int curveCount;
    public float curveRadius;


    [ContextMenu("LinearSpawn")]
    public void LinearSpawn()
    {
        for (int i = 0; i < spawnCount_Linear; i++)
        {
            GameObject newCoin = Instantiate(coinBase, transform);
            newCoin.transform.localPosition = startPos_Linear + i * offset_Linear;
        }
    }

    [ContextMenu("SquareSpawn")]
    public void SquareSpawn()
    {
        for (int i = 0; i < spawnCount_Square.x; i++)
        {
            for (int j = 0; j < spawnCount_Square.y; j++)
            {
                GameObject newCoin = Instantiate(coinBase, transform);
                newCoin.transform.localPosition = startPos_Square + new Vector2(i * offset_Square.x, j * offset_Square.y);

            }


        }
    }

    [ContextMenu("CurveSpawn")]
    public void CurveSpawn()
    {
        Vector3[] positions = CalculateSemicirclePoints();
        for (int i = 0; i < curveCount; i++)
        {
            GameObject newCoin = Instantiate(coinBase, transform);
            
            newCoin.transform.localPosition = positions[i];
        }

       
    }

    /*Vector3[] CalculateSemicirclePoints()
    {
        Vector3[] points = new Vector3[curveCount];
        for (int i = 0; i < curveCount; i++)
        {
            float angle = i / (float)(curveCount - 1) * 90.0f;  // Angle in degrees (90 degrees for a semicircle)
            float x = top_Curve.x + curveRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = top_Curve.y + curveRadius * Mathf.Sin(Mathf.Deg2Rad * angle);

            points[i] = new Vector3(x, y,0);
        }
        return points;
    }*/
    Vector3[] CalculateSemicirclePoints()
    {
        Vector3[] points = new Vector3[curveCount];
        Vector3 start = startPos_Curve - top_Curve;
        Vector3 end = endPos_Curve - top_Curve;

        for (int i = 0; i < curveCount; i++)
        {
            float t = i / (float)(curveCount - 1);
            float angle = Mathf.Lerp(Vector3.SignedAngle(Vector3.right, start, Vector3.forward), Vector3.SignedAngle(Vector3.right, end, Vector3.forward), t);
            float x = top_Curve.x + curveRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = top_Curve.y + curveRadius * Mathf.Sin(Mathf.Deg2Rad * angle);

            points[i] = new Vector3(x, y, 0);
        }
        return points;
    }







}
