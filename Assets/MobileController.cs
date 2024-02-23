using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour
{
    [SerializeField] private GameObject _mobileButtons;

    private WheelController _wheelController;
    void Awake()
    {
        LoadedInfo loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _wheelController = GetComponent<WheelController>();

        if (loadedInfo.isMobile==false)
        {
            Destroy(_mobileButtons);
            Destroy(Camera.main.GetComponent<Physics2DRaycaster>());
            Destroy(this);
        }
    }

    public void SetVerticalValue(int _value)
    {
        _wheelController.Vertical = _value;
    }
    public void SetHorisontalValue(int _value)
    {
        _wheelController.Horizontal = _value;
    }
}
