using Tanks;
using UnityEngine;
using UnityEngine.EventSystems;

internal class LongClickedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private MoveDirection _moveDirection;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _player.CurrentDirection.Value = _moveDirection;
        _player.ButtonDown = true;
    }
        

    public void OnPointerUp(PointerEventData eventData) =>
       _player.ButtonDown = false;
   

}
