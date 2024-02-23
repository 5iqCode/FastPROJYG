using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnerMap : MonoBehaviour
{
   [SerializeField] private int _countSpawnParts = 10;

    [SerializeField] GameObject[] _partsForSpawn;
    GameObject _lastSpawnedObj;

  [SerializeField]  private List<GameObject> _marks = new List<GameObject>();

   [SerializeField] private Material _markMaterial;
    [SerializeField] private Material _markMaterial_future;



    [SerializeField] private GameObject _player;
    private void Awake()
    {
        for (int i=0;i< _countSpawnParts;i++)
        {
            int _rand = Random.Range(0, _partsForSpawn.Length);
            if (i==0)
            {
                _lastSpawnedObj = Instantiate(_partsForSpawn[0]);
                _lastSpawnedObj.transform.position = new Vector3(-10,0,0);
            }
            else
            {
                Transform _endPointTransform = _lastSpawnedObj.GetComponentInChildren<EndPointMark>().transform;

                _lastSpawnedObj = Instantiate(_partsForSpawn[_rand]);
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

    private void Start()
    {

        GameObject[] _temoObjs = GameObject.FindGameObjectsWithTag("CheckBarrier");

        foreach (GameObject _obj in _temoObjs)
        {
            Destroy(_obj);
        }
    }


    [SerializeField] private GameObject _strelka;
    private void FixedUpdate()
    {
        Vector3 heading = _marks[_progress-1].transform.position - _strelka.transform.position;

        heading.y = 0;

        _strelka.transform.rotation = Quaternion.LookRotation(heading);
    }

    private int _progress=0;
    public void DestroyMark(GameObject _takedMark, bool _restart)
    {

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
            Debug.Log("End LVL");
            RestartThisMap();
        }
        if (!_restart)
        {
            _progress++;
        }
        
    }
    private void RestartThisMap()
    {
        _marks[_marks.Count - 1].SetActive(false);
        
        _progress = 0;
        DestroyMark(_marks[0], true);

        _player.GetComponent<WheelController>().RestartThisMap();
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
