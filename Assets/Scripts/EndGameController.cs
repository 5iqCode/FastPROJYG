using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private TMP_Text _statusGameText;
    [SerializeField] private TMP_Text _reasonLoseText;
    [SerializeField] private TMP_Text _valuePoints;
    private LoadedInfo _loadedInfo;
    private WheelController _wheelController;
    private SpeedCalculator _speedCalculator;

    [SerializeField] private Sprite _restartsprite;
    [SerializeField] private Image _imageButton;
    [SerializeField] private Image _bgImage;
    private void Awake()
    {
        _wheelController = GameObject.Find("Player (Vehicle)").GetComponent<WheelController>();

        _speedCalculator = _wheelController.GetComponent<SpeedCalculator>();

        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _textRewardButton = _buttonReward.GetComponentInChildren<TMP_Text>(); ;


        _wheelController._CanMove = false;
    }
    private bool isWinLvl;

    private int _pointsLvl;
    
    public void SetInfo(bool _isWin,float _points, string _reasonLose)
    {
        isWinLvl = _isWin;
        _pointsLvl = (int)_points;

         

        if (_isWin)
        {
            _valuePoints.text = _pointsLvl.ToString();
            _statusGameText.text = "Уровень пройден!";
        }
        else
        {
            _bgImage.color = new Color(1,0.7f, 0.7f);
            _imageButton.sprite = _restartsprite;
            _imageButton.color = new Color(1, 0.2f, 0.2f); 
            _valuePoints.text = "0";

            _textRewardButton.text = "Продолжить?";

            _statusGameText.text = "Уровень провален!";
            _reasonLoseText.text = _reasonLose;
        }
    }

    private void FixedUpdate()
    {
        if(isWinLvl)
        {
            if (_speedCalculator._PointsForDrift != _pointsLvl)
            {
                _pointsLvl = (int)_speedCalculator._PointsForDrift;
                _valuePoints.text = _pointsLvl.ToString();
                _textRewardButton.text = "+ " + _pointsLvl.ToString();
            }
        }

    }
    [SerializeField] private GameObject _buttonReward;
    private TMP_Text _textRewardButton;
    public void OnClickRewardedVideo()
    {
        if (isWinLvl)
        {
            _pointsLvl += _pointsLvl;
            _speedCalculator._PointsForDrift = _pointsLvl;

            _valuePoints.text = _pointsLvl.ToString();
            Destroy(_buttonReward);
        }
        else
        {
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<SpawnerMap>().ResumeForReward();
            _wheelController._CanMove = true;
            Destroy(gameObject);
        }
    }


    public void OnClickNext()
    {
        if(isWinLvl)
        {
            _loadedInfo.EndMapScript(_pointsLvl,1);
            SceneManager.LoadScene("DemoScene");
        }
        else
        {
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<SpawnerMap>().RestartThisMap();
        }
        _wheelController._CanMove = true;
        Destroy(gameObject);
    }

    public void OnClickLeave()
    {
        _loadedInfo.EndMapScript(_pointsLvl, 1);
        SceneManager.LoadScene("StartScreen");
        Destroy(gameObject);
    }
}
