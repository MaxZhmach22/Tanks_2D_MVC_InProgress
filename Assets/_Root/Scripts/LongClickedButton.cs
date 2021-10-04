using Tanks;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

internal class LongClickedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private SubscriptionProperty<MoveDirection> _moveDirection;
    private SubscriptionProperty<bool> _buttonState;

    [SerializeField] private MoveDirection _buttonDirection;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _moveDirection.Value = _buttonDirection;
        _buttonState.Value = true;
    }
        
    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonState.Value = false;
    }

    public void Init(SubscriptionProperty<MoveDirection> moveDirection, SubscriptionProperty<bool> buttonState)
    {
        _moveDirection = moveDirection;
        _buttonState = buttonState;
    }
      
   

}
