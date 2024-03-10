using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CheckDamageCar : MonoBehaviour
{
    [SerializeField] private float _deadDamage = 40;

    [SerializeField] private AudioSource _easyDamage;
    [SerializeField] private AudioSource _easyDamage2;
    [SerializeField] private AudioSource _hardDamage;
    [SerializeField] private AudioSource _hardDamage2;

    private WheelController _wheelController;

    [SerializeField] private GameObject _particleSystem;


    private void Start()
    {
        _wheelController = GetComponent<WheelController>();
    }
    private void Update()
    {
        Debug.Log(_wheelController.steerableWheels[0].wheelCol.rotationSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Barier")
        {
            List<GameObject> _obgToDestroy = new List<GameObject>();

            var _speed = GetComponent<SpeedCalculator>().Speed;


            foreach (ContactPoint contactPoint in collision.contacts)
            {
                GameObject _tempObj = Instantiate(_particleSystem);

                _obgToDestroy.Add(_tempObj);

                _tempObj.transform.rotation = Quaternion.LookRotation(_tempObj.transform.position -  _wheelController.gameObject.transform.position);
                _tempObj.transform.position = contactPoint.point;

                if (_speed > 30)
                {
                    _tempObj.transform.localScale = Vector3.one * 3;
                }
                else
                {
                    _tempObj.transform.localScale = Vector3.one * (_speed / 10f);
                }
                
            }


            
            if (_speed > 30)
            {
                _hardDamage.pitch = Random.Range(0.9f, 1.1f);

                for (int i = 0; i < 4; i++)
                {
                    _wheelController.steerableWheels[i].wheelCol.rotationSpeed /= 10;
                }
                if (1 > Random.Range(0, 2))
                {
                    _hardDamage.Play();
                }
                else
                {
                    _hardDamage2.Play();
                }


            }
            else if (_speed > 10)
            {
                _easyDamage.pitch = Random.Range(0.9f, 1.2f);


                for (int i = 0; i < 4; i++)
                {
                    _wheelController.steerableWheels[i].wheelCol.rotationSpeed /= 5;
                }
                if (1 > Random.Range(0, 2))
                {
                    _easyDamage.Play();
                }
                else
                {
                    _easyDamage2.Play();
                }
                


            }
            GetComponent<SpeedCalculator>().DamageCar();

            StartCoroutine(waitDestroyParticleSystem(_obgToDestroy));
        }

    }

    IEnumerator waitDestroyParticleSystem(List<GameObject> _objs)
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject obj in _objs)
        {
            Destroy(obj);
        }
    }
}
