using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMap : MonoBehaviour
{
   [SerializeField] private int _countSpawnParts = 10;

    [SerializeField] GameObject[] _partsForSpawn;
    GameObject _lastSpawnedObj;

    private void Start()
    {
        for(int i=0;i< _countSpawnParts;i++)
        {
            int _rand = Random.Range(0, _partsForSpawn.Length);
            if (i==0)
            {
                _lastSpawnedObj = Instantiate(_partsForSpawn[_rand]);
                _lastSpawnedObj.transform.position = new Vector3(-10,0,0);
            }
            else
            {
                Transform _endPointTransform = _lastSpawnedObj.GetComponentInChildren<EndPointMark>().transform;

                _lastSpawnedObj = Instantiate(_partsForSpawn[_rand]);

                _lastSpawnedObj.transform.rotation = _endPointTransform.rotation;
                _lastSpawnedObj.transform.position = _endPointTransform.position;
            }
            
        }
    }
}
