using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInfo
{
    public int current_level= 0;

    public int Points = 0;

    public string _language = "ru";//ru; en;

    public float _volumeMusic = 1;
    public float _volumeAll = 1; 

    

    public int _lastTakedCar = 1;




    public int version = 1;
}

public class LoadedInfo : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public ScriptableObj _carInfo;

    public bool isMobile;
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


}