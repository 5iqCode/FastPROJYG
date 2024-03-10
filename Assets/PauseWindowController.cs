using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindowController : MonoBehaviour
{
    MusicBoxController _musicBoxController;
    private void Start()
    {
        _musicBoxController = GameObject.FindGameObjectWithTag("MusicBoxController").GetComponentInChildren<MusicBoxController>();
    }
    public void OnClickResumeGame()
    {
        ResumeGame();
        Destroy(gameObject);
    }
    private void ResumeGame()
    {
        if (!_musicBoxController._isPauseMusic)
        {
            _musicBoxController._activeMusic.UnPause();
        }
        Camera.main.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1;
    }
    public void OnClickRestart()
    {
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<SpawnerMap>().RestartThisMap();
        ResumeGame();
        Destroy(gameObject);

    }

    public void OnClickGoToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("StartScreen");
    }
}
