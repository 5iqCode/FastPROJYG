using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRoadScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Barier")
        {
            Destroy(other.gameObject);
        }
    }
}
