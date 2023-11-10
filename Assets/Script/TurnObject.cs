using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObject : MonoBehaviour
{
    public TurnSetting[] turnSettings;
    public float time = 3f;
}

[System.Serializable]
public class TurnSetting
{
    public string tag;
    public ScoreObject insteadObject;
}
