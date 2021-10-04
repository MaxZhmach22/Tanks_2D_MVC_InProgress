using Tools;
using UnityEngine;

namespace Tanks
{
    internal class PlayerView : BaseView
    {
        private float timer;
        private bool isMoving;
        private MoveDirection _previousDirection = MoveDirection.None;
        private MoveDirection _currentDirection;

        SubscriptionProperty<MoveDirection> _moveDirectionsState;
        SubscriptionProperty<bool> _buttonState;
        private bool _buttonDown;


        private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
        private Vector3 _tempPosition;
        private Vector3 _direction;
        private float _speed = 2f;

        public void Init(SubscriptionProperty<MoveDirection> moveDirectionsState,
                         SubscriptionProperty<bool> buttonState)
        {
            _moveDirectionsState = moveDirectionsState;
            _buttonState = buttonState;
            _moveDirectionsState.SubscribeOnChange(OnChangeDirection);
            _buttonState.SubscribeOnChange(OnChangeButtonState);
            transform.position += _offset;
            _tempPosition = transform.position;

        }

        private void OnDestroy()
        {
            _moveDirectionsState.UnSubscribeOnChange(OnChangeDirection);
            _buttonState.UnSubscribeOnChange(OnChangeButtonState);
        }

        private void Update()
        {
            if (isMoving)
            {
                Move(_direction);
                return;
            }
            if (_buttonDown)
                isMoving = true;
        }

        private void OnChangeDirection(MoveDirection value)
        {
            _currentDirection = value;
            if (!isMoving)
            {
                switch (value)
                {
                    case MoveDirection.None:
                        _direction = Vector3.right;
                        break;
                    case MoveDirection.Up:
                        _direction = Vector3.up;
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                        break;
                    case MoveDirection.Down:
                        _direction = Vector3.down;
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                        break;
                    case MoveDirection.Left:
                        _direction = Vector3.left;
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    case MoveDirection.Right:
                        _direction = Vector3.right;
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        break;
                }
                _previousDirection = value;
            }

        }

        private void OnChangeButtonState(bool buttonsState)
        {
            _buttonDown = buttonsState;
        }
        private void Move(Vector3 direction)
        {
            isMoving = true;
            timer += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(_tempPosition, _tempPosition + direction, timer);
            if (timer >= 1)
            {
                _tempPosition = transform.position;
                timer = 0;
                if (_buttonDown && _previousDirection == _currentDirection)
                    isMoving = true;
                else
                {
                    isMoving = false;
                    CheckDirection();
                }
            }
        }

        private void CheckDirection()
        {
            if (_previousDirection != _currentDirection)
            {
                OnChangeDirection(_currentDirection);
                isMoving = true;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }

        
        public Transform GetPlayerTransform() =>
            gameObject.transform;

    }
}