
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickMobileButtonScriptHorisontal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private int ValueHorisontal;
    [SerializeField] private MobileController _mobileController;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(13);
        _mobileController.SetHorisontalValue(ValueHorisontal);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _mobileController.SetHorisontalValue(0);
    }

}
