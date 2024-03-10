using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] private GameObject PauseWindow;
    public void OnClickPauseGame()
    {
        Instantiate(PauseWindow,GetComponentInParent<Canvas>().transform);
        Camera.main.GetComponent<AudioListener>().enabled = false;
        GameObject.FindGameObjectWithTag("MusicBoxController").GetComponentInChildren<MusicBoxController>()._activeMusic.Pause();
        Time.timeScale = 0;
    }
}
