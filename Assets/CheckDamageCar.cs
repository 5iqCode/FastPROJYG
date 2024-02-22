using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamageCar : MonoBehaviour
{
    [SerializeField] private float _deadDamage = 40;


    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<SpeedCalculator>().Speed > 40)
        {
            Debug.Log("Dead");
        }
        else
        {
            Debug.Log("Damage");
            GetComponent<SpeedCalculator>().DamageCar();
        }
       
    }
}
