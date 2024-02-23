using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    private LoadedInfo _LoadedInfo;

    private ScriptableObj _carInfo;

    [SerializeField] private int _maxCar;

    [SerializeField] private Transform _parentCar;
    [SerializeField] private Transform _player;

    int lastTakedCar;

    private GameObject _lastSpawnedCar;

    private void Awake()
    {
        _LoadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        lastTakedCar = _LoadedInfo.PlayerInfo._lastTakedCar;
    }
    private void Start()
    {
        _carInfo = Resources.Load<ScriptableObj>("SpellsScriptableObject/" + lastTakedCar);

        NextCar(0);
    }

    public void NextCar(int _value)
    {
        Debug.Log(lastTakedCar);
        if (_value == -1)
        {
            if (lastTakedCar == 0)
            {
                lastTakedCar = _maxCar;
            }
            else
            {
                lastTakedCar--;
            }
        }
        else if (_value ==1)
        {
            if (lastTakedCar == _maxCar)
            {
                lastTakedCar = 0;
            }
            else
            {
                lastTakedCar++;
            }
        }
        Debug.Log(lastTakedCar);
        _carInfo = Resources.Load<ScriptableObj>("CarInfo/" + lastTakedCar);

        if (_lastSpawnedCar!=null)
        {
            Destroy( _lastSpawnedCar );
        }

        SetInfoInScreen();

        _lastSpawnedCar = Instantiate(_carInfo.Car3DObj, _parentCar);
        _player.position += new Vector3(0, 0.05f, 0);


    }
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _BreakSlider;
    [SerializeField] private TMP_Text _mass;
    [SerializeField] private TMP_Text _euler;
    [SerializeField] private TMP_Text _NameCar;



    private void SetInfoInScreen()
    {
        _NameCar.text = _carInfo.Name;
        _mass.text = _carInfo.Mass.ToString();
        _euler.text = _carInfo.EulerRotate.ToString();
        Debug.Log((_carInfo.CircleSpeed / _speedSlider.maxValue));
        _speedSlider.value = _carInfo.CircleSpeed ;
        _BreakSlider.value = _carInfo.BreakValue;
    }

    public void Play()
    {
        _LoadedInfo._carInfo = _carInfo;

        SceneManager.LoadScene("DemoScene");
    }
}
