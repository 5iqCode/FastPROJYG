using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpeedCalculator : MonoBehaviour
{

    public float Speed;
    public Rigidbody rb;

    public TMP_Text SpeedText;

    public TMP_Text PointsText;

    public TMP_Text LastPointsText;

    public TMP_Text MyltiplyDrift;

    private float _mnozhitelPoints;

    private float _PointsForDrift;

    private float _MyltiplyDrift =1;

    private float _lastPointsForDrift = 0;

    private WheelSkid WheelHitInfo;

    private void Start()
    {
        WheelHitInfo = GameObject.Find("fl").GetComponent<WheelSkid>();
    }
    void FixedUpdate()
    {
        _mnozhitelPoints = WheelHitInfo._SkidTotal;
        
        Vector3 vel = rb.velocity;
        Speed = rb.velocity.magnitude * 1.5f;

        if (_lastPointsForDrift > 1500)
        {
            _MyltiplyDrift = 1.8f;
            MyltiplyDrift.text = "x" + _MyltiplyDrift;
        }
        else if (_lastPointsForDrift > 900)
        {
            _MyltiplyDrift = 1.5f;
            MyltiplyDrift.text = "x"+ _MyltiplyDrift;
        }
        else if (_lastPointsForDrift > 300)
        {
            _MyltiplyDrift = 1.3f;
            MyltiplyDrift.text = "x" + _MyltiplyDrift;
        }
        else
        {
            _MyltiplyDrift = 1;
            MyltiplyDrift.text = "";
        }

        if ((Speed > 5)&& (_mnozhitelPoints > 3))
        {
            _lastPointsForDrift += Speed * _mnozhitelPoints/100 * _MyltiplyDrift; 
        }
        else
        {
            _PointsForDrift += _lastPointsForDrift;
            _lastPointsForDrift = 0;

            PointsText.text = _PointsForDrift.ToString("0");
        }

        SpeedText.text = Speed.ToString("0");

        if (_lastPointsForDrift > 0)
        {
            LastPointsText.text = _lastPointsForDrift.ToString("0");
        }
        else
        {
            LastPointsText.text = "";
        }
    }


    public void DamageCar()
    {
        _lastPointsForDrift = 0;
    }

}
