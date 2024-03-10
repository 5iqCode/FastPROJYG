using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicBoxController : MonoBehaviour
{
    [SerializeField] private Slider _fxSlider;
    [SerializeField] private Slider _musicSlider;


    [SerializeField] private string[] _masMusicName;

    [SerializeField] private TMP_Text _textNameMusic;

    [SerializeField]private AudioClip[] _clips;

    [SerializeField] private Sprite[] _statusMusicSprites;

    [SerializeField] private Image _activeStatusMusicSprite;

    public AudioSource _nextMusic;
    public AudioSource _activeMusic;
    public AudioSource _backMusic;

    private int _maxMusic = 6;

    public bool _isPauseMusic = false;

    private int _playingMusicId=0;
    LoadedInfo loadedInfo;
    private void Awake()
    {

        loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _isPauseMusic = loadedInfo.PlayerInfo.musicOnPause;

        if (_isPauseMusic)
        {
            _activeStatusMusicSprite.sprite = _statusMusicSprites[0];
        }
        else
        {
            _activeStatusMusicSprite.sprite = _statusMusicSprites[1];
        }
        

        _playingMusicId = loadedInfo.PlayerInfo.lastPlayingMusic;

        _maxMusic = _masMusicName.Length-1;

        SetNameMusic();

        UpdateMusicBackNextMusic();


    }

    private void Start()
    {
        _fxSlider.value = loadedInfo.PlayerInfo._volumeFX;

        _musicSlider.value = loadedInfo.PlayerInfo._volumeMusic;

        _activeMusic.clip = _clips[_playingMusicId];

        _activeMusic.Play();
        _activeMusic.Pause();
        if (!loadedInfo.PlayerInfo.musicOnPause)
        {
            _activeMusic.UnPause();
        }


    }
    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "DemoScene")
        {
            if (!_isPauseMusic)
            {
                _activeMusic.UnPause();
            }
            GetComponentInParent<Canvas>().gameObject.SetActive(true);
        }
        else
        {
            Destroy(GetComponentInParent<Canvas>().gameObject);
        }
    }
    public void OnSliderValueChangedScriptFXSlider()
    {
        float _value = _fxSlider.value;
        if (_value > -35)
        {
            loadedInfo.SetValueAudioMixerGameSounds(_value);
        }
        else
        {
            loadedInfo.SetValueAudioMixerGameSounds(-80);
        }
    }
    public void OnSliderValueChangedScriptMusicSlider()
    {
        float _value = _musicSlider.value;
        if (_value > -35)
        {
            loadedInfo.SetValueAudioMixerMusic(_value);
        }
        else
        {
            loadedInfo.SetValueAudioMixerMusic(-80);
        }
    }

    private void UpdateMusicBackNextMusic()
    {
        

        if (_playingMusicId == 0)
        {
            _backMusic.clip = _clips[_maxMusic];
        }
        else
        {
            _backMusic.clip = _clips[_playingMusicId - 1];
        }

        if (_playingMusicId == _maxMusic)
        {
            _nextMusic.clip = _clips[0];
        }
        else
        {
            _nextMusic.clip = _clips[_playingMusicId + 1];
        }
    }
    public void OnClickNextMusic()
    {
        

        _playingMusicId++;
        if (_playingMusicId > _maxMusic)
        {
            _playingMusicId = 0;
        }
        _backMusic.clip = _activeMusic.clip;
        _activeMusic.clip = _nextMusic.clip;


        UpdateMusicBackNextMusic();

        _activeMusic.Play();


        _isPauseMusic = false;

        SetNameMusic();
    }


    public void OnClickBackMusic()
    {
        _playingMusicId--;
        if (_playingMusicId < 0)
        {
            _playingMusicId = _maxMusic;
        }

        _nextMusic.clip = _activeMusic.clip;
        _activeMusic.clip = _backMusic.clip;

        UpdateMusicBackNextMusic();

        _activeMusic.Play();

        _isPauseMusic = false;

        SetNameMusic();
    }

    public void OnClickPausePlay()
    {
        if (_activeMusic.isPlaying == true)
        {
            _activeMusic.Pause();
            _isPauseMusic = true;
            _activeStatusMusicSprite.sprite = _statusMusicSprites[0];
        }
        else
        {
            _isPauseMusic = false;
            _activeMusic.UnPause();
            _activeStatusMusicSprite.sprite = _statusMusicSprites[1];
        }

    }
    private void FixedUpdate()
    {
            if ((!_activeMusic.isPlaying) && (!_isPauseMusic))
            {
                OnClickNextMusic();
            }

    }
    private void SetNameMusic()
    {
        loadedInfo.PlayerInfo.lastPlayingMusic = _playingMusicId;
        _textNameMusic.text = _masMusicName[_playingMusicId];
    }
    private AudioClip LoadMusicFromResources(int _idMusic)
    {
        return Resources.Load<AudioClip>("Music/" + _idMusic);
        
    }
}
