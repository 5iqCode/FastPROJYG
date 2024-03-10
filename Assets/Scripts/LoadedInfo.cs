using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInfo
{
    public int current_level= 10;

    public int Points = 0;

    public string _language = "ru";//ru; en;

    public float _volumeMusic = 0;
    public float _volumeFX = -2;

    public int lastPlayingMusic = 0;
    public bool musicOnPause = true;


    public int _lastTakedCar = 0;

    public int[] _statusCars = { 1, 0, 0, 0, 0 ,0,0}; // 0 не куплено

    public int version = 1;
}
public class LoadedInfo : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public ScriptableObj _carInfo;

    public bool isMobile;

    [SerializeField] private AudioMixer _audioMixer;

    public void SetValueAudioMixerGameSounds(float _volume)
    {

        this.PlayerInfo._volumeFX = _volume;
        _audioMixer.SetFloat("GameSounds", _volume);
    }

    public void SetValueAudioMixerMusic(float _volume)
    {
        this.PlayerInfo._volumeMusic = _volume;
        _audioMixer.SetFloat("Music", _volume);
    }
    



    public int LoadLevelScript()
    {
        int _countPrefabsForSpawn;
        if (PlayerInfo.current_level<3)
        {
            _countPrefabsForSpawn = 5;
        }
        else if (PlayerInfo.current_level < 50)
        {
            _countPrefabsForSpawn = 5 + (PlayerInfo.current_level/2);
        }
        else
        {
            _countPrefabsForSpawn = 30;
        }

        return _countPrefabsForSpawn;
    }
    private void Awake()
    {
        PlayerInfo = new PlayerInfo();

        Application.targetFrameRate = 60;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoadedInfo");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

       
    }
    private void Start()
    {
        SetValueAudioMixerMusic(PlayerInfo._volumeMusic);
        SetValueAudioMixerGameSounds(PlayerInfo._volumeFX);
    }

    public void EndMapScript(int _pointsOnMap,int changeValueLvl)
    {
        PlayerInfo.Points += _pointsOnMap;
        PlayerInfo.current_level += changeValueLvl;

        Save();
    }

    public void BuyCarScript(int _idCar,int _pointsForCar)
    {
        PlayerInfo._lastTakedCar = _idCar;
        PlayerInfo.Points -= _pointsForCar;
        PlayerInfo._statusCars[_idCar] = 1;

        Save();
    }


    public void Save()
    {
        //сохранить инфу
    }
}