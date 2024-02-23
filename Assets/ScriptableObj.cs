
using UnityEngine;

[CreateAssetMenu(fileName = "CarInfo", menuName = "ScriptableObject/CarInfo")]
public class ScriptableObj : ScriptableObject
{
    public string Name;

    public float CircleSpeed;

    public float BreakValue;

    public int EulerRotate;

    public int Mass;

    public GameObject Car3DObj;

}

