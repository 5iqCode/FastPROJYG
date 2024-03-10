
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    private LoadedInfo _LoadedInfo;

    private ScriptableObj _carInfo;

    private PlayerInfo _playerInfo;

    [SerializeField] private int _maxCar;

    [SerializeField] private Transform _parentCar;
    [SerializeField] private Transform _player;

    int lastTakedCar;

    private GameObject _lastSpawnedCar;

    [SerializeField] private TMP_Text _PointsText;

    private void Awake()
    {
        _LoadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _playerInfo = _LoadedInfo.PlayerInfo;

        _PointsText.text = _playerInfo.Points.ToString();

        lastTakedCar = _playerInfo._lastTakedCar;


    }
    [SerializeField] private GameObject _playButton;

    [SerializeField] private GameObject _closeBlock;

    [SerializeField] private TMP_Text _closeBlockButtonText;

    [SerializeField] private TMP_Text _coatsCarText;
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
        _carInfo = Resources.Load<ScriptableObj>("CarInfo/" + lastTakedCar);

        if (_lastSpawnedCar!=null)
        {
            Destroy( _lastSpawnedCar );
        }

        SetInfoInScreen();

        _lastSpawnedCar = Instantiate(_carInfo.Car3DObj, _parentCar);
        _player.position += new Vector3(0, 0.05f, 0);

        if (_playerInfo._statusCars[lastTakedCar] == 0)
        {
            _playButton.SetActive(false);
            _closeBlock.SetActive(true);

            _coatsCarText.text = _carInfo._coastPoint.ToString();

            if (_carInfo._coastPoint>_playerInfo.Points)
            {
                _closeBlockButtonText.text = "Недостаточно очков";
                _closeBlock.GetComponentInChildren<Button>().interactable = false;
                
            }
            else
            {
                _closeBlockButtonText.text = "Разблокировать";
                _closeBlock.GetComponentInChildren<Button>().interactable = true;
                
            }
        }
        else
        {
            _playButton.SetActive(true);
            _closeBlock.SetActive(false);
        }
    }



    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _BreakSlider;
    [SerializeField] private Slider _RotateSlider;
    [SerializeField] private TMP_Text _mass;
    [SerializeField] private TMP_Text _NameCar;

    public void OnClickOpenCar()
    {
        _playerInfo._statusCars[lastTakedCar] = 1;
        _playerInfo.Points -= _carInfo._coastPoint;
        _playerInfo._lastTakedCar = lastTakedCar;

        _LoadedInfo.Save();

        _PointsText.text = _playerInfo.Points.ToString();

        NextCar(0);
    }

    private void SetInfoInScreen()
    {
        _NameCar.text = _carInfo.Name;
        _mass.text = _carInfo.Mass.ToString();
        _RotateSlider.value = _carInfo.EulerRotate;
        _speedSlider.value = _carInfo.CircleSpeed ;
        _BreakSlider.value = _carInfo.BreakValue;
    }

    public void Play()
    {
        _LoadedInfo._carInfo = _carInfo;
        _playerInfo._lastTakedCar = lastTakedCar;


        SceneManager.LoadScene("DemoScene");
    }
}
