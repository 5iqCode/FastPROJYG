using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultValues : MonoBehaviour
{
    [SerializeField] Transform parent;

    void Start()
    {
        ScriptableObj _carInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>()._carInfo;

        WheelController wheelController = GetComponent<WheelController>();

        wheelController.wheelSteeringAngle = _carInfo.EulerRotate;
        wheelController.BreakPower = _carInfo.BreakValue;
        wheelController.wheelMaxSpeed = _carInfo.CircleSpeed;

        GetComponent<Rigidbody>().mass = _carInfo.Mass;

        Instantiate(_carInfo.Car3DObj, parent);
    }
}
