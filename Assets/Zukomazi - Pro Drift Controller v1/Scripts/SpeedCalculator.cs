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

    public float _PointsForDrift;

    private float _MyltiplyDrift =1;

    private float _lastPointsForDrift = 0;

    private WheelSkid WheelHitInfo;

    private Camera _mainCamera;


    private void Start()
    {
        WheelHitInfo = GameObject.Find("fl").GetComponent<WheelSkid>();

        _mainCamera = Camera.main;


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

        SpeedText.text = (Speed*1.5f).ToString("0");

        if (_lastPointsForDrift > 0)
        {
            Color lerpedcolor = Color.white;
            LastPointsText.text = _lastPointsForDrift.ToString("0");
            if (_lastPointsForDrift < 300)
            {
                lerpedcolor = Color.Lerp(Color.green, Color.yellow, _lastPointsForDrift / 300f);
            } else if (_lastPointsForDrift < 900)
            {
                lerpedcolor = Color.Lerp(Color.yellow, Color.red, (_lastPointsForDrift-300) / 600f);
            }
            else if (_lastPointsForDrift < 2000) 
            {
                lerpedcolor = Color.Lerp(Color.red, Color.magenta, (_lastPointsForDrift-900) / 1100f);
            }
            else
            {
                lerpedcolor = Color.Lerp(Color.magenta, Color.cyan, (_lastPointsForDrift-2000) / 2000f);
            }

            if (_lastPointsForDrift < 2000)
            {
                MyltiplyDrift.fontSize = 75 * (1+(_lastPointsForDrift / 3000f));
            }
            else
            {
                MyltiplyDrift.fontSize = 130;
            }

            LastPointsText.color = lerpedcolor;
            MyltiplyDrift.color = lerpedcolor;
        }
        else
        {
            LastPointsText.text = "";
        }
        if (Speed < 10)
        {
            _mainCamera.fieldOfView = 60;
        }
        else if (Speed<40)
        {
            _mainCamera.fieldOfView = 60 + (Speed-10);
        }
        else
        {
            _mainCamera.fieldOfView = 90;
        }
    }
    public void EndGame()
    {
        _PointsForDrift += _lastPointsForDrift;
        PointsText.text = _PointsForDrift.ToString("0");
    }
    public void RestartMapPoints()
    {
        _PointsForDrift = 0;
        _lastPointsForDrift = 0;
        PointsText.text = "0";
    }
    public void DamageCar()
    {
        _lastPointsForDrift = 0;
    }

}
