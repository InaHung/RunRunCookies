using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Map : MonoBehaviour
{
    public float moveSpeed;
    public List<Scene> scenes = new List<Scene>();
    public List<Scene> aliveScenes = new List<Scene>();
    private float createPosition;
    private int sceneIndex;
    public float slowTime;
    public float sprintSpeed;
    private float originMoveSpeed;
    Tween originSpeedTween;
   

    private void Awake()
    {

        originMoveSpeed = moveSpeed;
        foreach (var aliveScene in aliveScenes)
        {
            aliveScene.OnEnterCheckPoint += createNextScene;
            aliveScene.onDestroyScene += (Scene) =>
            {
                aliveScenes.Remove(Scene);
            };
            
        }
        createPosition += aliveScenes[0].radius;
        for (int i = 1; i < aliveScenes.Count; i++)
        {
            createPosition += aliveScenes[i].radius * 2;
        }
        sceneIndex = aliveScenes.Count;
    }
    void Update()
    {
        MapMove();
    }
    public void MapMove()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }
    public void createNextScene()
    {
        Scene newScene = Instantiate(scenes[sceneIndex % scenes.Count], transform);
        newScene.OnEnterCheckPoint += createNextScene;
        createPosition += newScene.radius;
        newScene.transform.localPosition = new Vector3(createPosition, 0, 0);
        createPosition += newScene.radius;
        aliveScenes.Add(newScene);
        newScene.onDestroyScene += (Scene) =>
        {
            aliveScenes.Remove(Scene);
        };
        sceneIndex++;
    }

    public void TimeSlow()
    {
        Time.timeScale = slowTime;
        DOVirtual.DelayedCall(0.5f, () =>
         {
             Time.timeScale = 1f;
         });
    }
    public void TimeFast(float sprintTime)
    {
        moveSpeed = sprintSpeed;
        if (originSpeedTween != null)
        {
            originSpeedTween.Kill();
        }

        originSpeedTween=DOVirtual.DelayedCall(sprintTime, () =>
        {
            moveSpeed = originMoveSpeed;
        });
       
    }


}
