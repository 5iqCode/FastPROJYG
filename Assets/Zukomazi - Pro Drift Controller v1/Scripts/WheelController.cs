using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

    public WheelAlignment[] steerableWheels;

    public float BreakPower;

    public float Horizontal;
    public float Vertical;
    //Steering variables
    public float wheelRotateSpeed;
    public float wheelSteeringAngle;

    //Motor variables
    public float wheelAcceleration;
    public float wheelMaxSpeed;



    public Rigidbody RB;

    private Quaternion _defoultrotation;

    private void Start()
    {
        _defoultrotation = transform.rotation;
    }

    // Update is called once per frame
    void Update ()
    {
        wheelControl();      
	}

    //Applies steering and motor torque
    void wheelControl()
    {
        for (int i = 0; i < steerableWheels.Length; i++)
        {
            //Sets default steering angle
            steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].steeringAngle, 0, Time.deltaTime * wheelRotateSpeed);
            //Sets default motor speed
            steerableWheels[i].wheelCol.motorTorque = -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, 0, Time.deltaTime * wheelAcceleration);

            //Motor controls

            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            if (Vertical > 0.1)
            {

                    steerableWheels[i].wheelCol.motorTorque = -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration);
               
            }

            if (Vertical < -0.1)
            {
                if (Vector3.Dot(RB.velocity, transform.forward) > 0f)
                {
                    steerableWheels[i].wheelCol.motorTorque = Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration);
                }
                else
                {
                    steerableWheels[i].wheelCol.motorTorque = Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration * BreakPower);
                }
                
                RB.drag = 0.3f;
            }
            else
            {
                RB.drag = 0;
            }

            if (Vertical == 0)
            {
                if (Vector3.Dot(RB.velocity, transform.forward) < 0f)
                {
                    steerableWheels[i].wheelCol.motorTorque = Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration * 2);
                }else if (Vector3.Dot(RB.velocity, transform.forward) > 0f)
                {
                    steerableWheels[i].wheelCol.motorTorque = -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, wheelMaxSpeed, Time.deltaTime * wheelAcceleration /2);
                }
            }

            float tempSterableWheel = steerableWheels[i].steeringAngle;

            if  (Horizontal < -0.1f)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(tempSterableWheel, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            }else if (Horizontal > 0.1f)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(tempSterableWheel, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            }
        }
    }


    public void RestartThisMap()
    {
        transform.position = new Vector3(0,2,0);
        transform.rotation = _defoultrotation;

        RB.velocity = Vector3.zero;

        for (int i = 0; i < steerableWheels.Length; i++)
        {
            steerableWheels[i].wheelCol.motorTorque = 0;
        }
    }
}
