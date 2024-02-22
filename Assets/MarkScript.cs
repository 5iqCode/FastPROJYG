using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("fdgdfgdfg");
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<SpawnerMap>().DestroyMark(gameObject,false);
        }
    }
}
