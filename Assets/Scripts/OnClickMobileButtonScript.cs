
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickMobileButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private int ValueVertical;
    [SerializeField] private MobileController _mobileController;
    public void OnPointerDown(PointerEventData eventData)
    {
        _mobileController.SetVerticalValue(ValueVertical);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _mobileController.SetVerticalValue(0);
    }
}
