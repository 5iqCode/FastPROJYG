using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnerMap : MonoBehaviour
{
    [SerializeField] private GameObject _endGameWindow;

   [SerializeField] private int _countSpawnParts = 10;

    [SerializeField] GameObject[] _partsForSpawn;
    GameObject _lastSpawnedObj;

  [SerializeField]  private List<GameObject> _marks = new List<GameObject>();

   [SerializeField] private Material _markMaterial;
    [SerializeField] private Material _markMaterial_future;


    [SerializeField] private GameObject _startBlock;
    [SerializeField] private GameObject _endBlock;


    [SerializeField] private GameObject _player;

    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TMP_Text _progressText;


    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private TMP_Text _lvlText;

    private LoadedInfo _loadedInfo;
    private void Awake()
    {
        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _lvlText.text = _loadedInfo.PlayerInfo.current_level.ToString();

        _countSpawnParts = _loadedInfo.LoadLevelScript();
        int _lengthMas = _partsForSpawn.Length;
        if (_loadedInfo.PlayerInfo.current_level < 8)
        {
            _lengthMas--;
        }
        for (int i=0;i<= _countSpawnParts;i++)
        {
            int _rand = Random.Range(0, _lengthMas);

            if (i==0)
            {
                _lastSpawnedObj = Instantiate(_startBlock);
            }
            else
            {
                Transform _endPointTransform = _lastSpawnedObj.GetComponentInChildren<EndPointMark>().transform;

                if (i != _countSpawnParts)
                {
                    _lastSpawnedObj = Instantiate(_partsForSpawn[_rand]);
                }
                else
                {
                    _lastSpawnedObj = Instantiate(_endBlock);
                }
                
                Debug.Log(i);
                _lastSpawnedObj.transform.rotation = _endPointTransform.rotation;
                _lastSpawnedObj.transform.position = _endPointTransform.position;

                _lastSpawnedObj.transform.position += Vector3.up * 0.00005f;
            }

            _marks.AddRange(_lastSpawnedObj.GetComponent<MarkToProgressMass>().listMarks);
        }
        for (int i=3;i< _marks.Count;i++)
        {
            _marks[i].SetActive(false);
        }
    }

    private Coroutine _jobTimer;
    private int _timer = 11;

    private int _currentLvl;
    private void Start()
    {
        _currentLvl = _loadedInfo.PlayerInfo.current_level;
        StartCoroutine(waitToDestroy());
        if (_currentLvl > 5) // таймер
        {
            _jobTimer = StartCoroutine(waitSeconds());
        }
        else
        {
            Destroy(_timerText);
        }
    }
    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject[] _temoObjs = GameObject.FindGameObjectsWithTag("CheckBarrier");

        foreach (GameObject _obj in _temoObjs)
        {
            Destroy(_obj);
        }
    }
    
    IEnumerator waitSeconds()
    {
        while (_timer > 0)
        {
            _timer--;
            if (_timerText != null)
            {
                _timerText.text = _timer.ToString();
                Color lerpedcolor = Color.Lerp(Color.red, Color.yellow, _timer / 20f);
                _timerText.color = lerpedcolor;
            }

            if (_timer <= 0)
            {
                foreach (GameObject _mark in _marks)
                {
                    _mark.SetActive(false);
                }
                SpeedCalculator _speedCalculator = _player.GetComponent<SpeedCalculator>();
               
               
               GameObject _window = Instantiate(_endGameWindow);
                _window.GetComponent<EndGameController>().SetInfo(false, 0, "Вы не успели доехать до контрольной точки");

            }
            yield return new WaitForSeconds(1f);
        }

    }
    private float _lastPointForDrift;
    [SerializeField] private GameObject _strelka;
    private void FixedUpdate()
    {
        Vector3 heading;
        if (_progress > 0)
        {
           heading = _marks[_progress - 1].transform.position - _strelka.transform.position;
        }
        else
        {
            heading = _marks[0].transform.position - _strelka.transform.position;
        }
        

        heading.y = 0;

        _strelka.transform.rotation = Quaternion.LookRotation(heading);
    }

    private int _progress=0;

    private void SetTimer()
    {
        if (_currentLvl > 20)
        {
            _timer = 10;

        }
        else if (_currentLvl > 5)
        {
            _timer = 20;
        }
        if (_timerText != null)
        {
            _timerText.text = _timer.ToString();
        }
    }
    public void DestroyMark(GameObject _takedMark, bool _restart)
    {
        SetTimer();
        _takedMark.SetActive(false);
        if (_marks.Count - _progress > 2)
        {
            _marks[_progress].SetActive(true);

            ChangeMaterialMarks(2);
        }
        else
        {
            _marks[_marks.Count - 1].SetActive(true);
            ChangeMaterialMarks(_marks.Count - _progress);
        }

        if (_progress > _marks.Count)
        {
            _marks[_marks.Count-1].SetActive(false);
            _player.GetComponent<SpeedCalculator>().EndGame();
            if (_jobTimer != null)
            {
                StopCoroutine(_jobTimer);
                _jobTimer = null;
            }
            Debug.Log("End LVL");
            GameObject _window = Instantiate(_endGameWindow);
            _window.GetComponent<EndGameController>().SetInfo(true, _player.GetComponent<SpeedCalculator>()._PointsForDrift, "");
        }
        var _progressValue = (float)_progress/ (float)_marks.Count;

        _progressSlider.value = _progressValue;
        if (_progressValue < 1)
        {
            _progressText.text = (_progressValue * 100).ToString("0") + "%";
        }
        else
        {
            _progressText.text = "100%";
        }
        

        if (!_restart)
        {
            _progress++;
        }

    }
    public void RestartThisMap()
    {
        if (_currentLvl > 5) // таймер
        {
            if (_jobTimer != null)
            {
                StopCoroutine(_jobTimer);
            }
                
            SetTimer();
            _jobTimer = StartCoroutine(waitSeconds());
        }

        foreach (GameObject _mark in _marks)
        {
            _mark.SetActive(false);
        }
        _progress = 0;
        DestroyMark(_marks[0], true);

        _player.GetComponent<WheelController>().RestartThisMap();
        _player.GetComponent<SpeedCalculator>().RestartMapPoints();
    }

    public void ResumeForReward()
    {
        if (_marks.Count - _progress <= 2)
        {
            _progress = _marks.Count-2;
        }


        if (_currentLvl > 5) // таймер
        {
            if (_jobTimer != null)
            {
                StopCoroutine(_jobTimer);
            }

            SetTimer();
            DestroyMark(_marks[_progress--], false);
            _player.GetComponent<SpeedCalculator>()._PointsForDrift += _lastPointForDrift;
            _jobTimer = StartCoroutine(waitSeconds());
        }
    }
    private void ChangeMaterialMarks(int _countVisibleMark)
    {
        for(int i=0;i< _countVisibleMark;i++)
        {
            if (i == 0)
            {
                _marks[_progress].GetComponent<Renderer>().material = _markMaterial;
                _marks[_progress].GetComponent<CapsuleCollider>().enabled = true;
            }
            else
            {
                _marks[_progress + i].SetActive(true) ;
                _marks[_progress + i].GetComponent<Renderer>().material = _markMaterial_future;
                _marks[_progress + i].GetComponent<CapsuleCollider>().enabled = false;
            }
        }

    }
}
