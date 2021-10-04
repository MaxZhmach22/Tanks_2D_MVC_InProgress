using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Tools;
namespace Tanks
{
    internal class Player : MonoBehaviour
    {
        public SubscriptionProperty<MoveDirection> CurrentDirection;

        private float timer;
        private bool isMoving;
        private MoveDirection _previousDirection = MoveDirection.None;
        private MoveDirection _currentDirection;
        public bool ButtonDown { get; set; }

        private Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
        private Vector3 _tempPosition;
        private Vector3 _direction;
        [SerializeField] private float _speed;

        void Start()
        {
            CurrentDirection = new SubscriptionProperty<MoveDirection>(MoveDirection.None);
            transform.position += _offset;
            _tempPosition = transform.position;
            OnChangeDirection(CurrentDirection.Value);
            CurrentDirection.SubscribeOnChange(OnChangeDirection);
        }

        private void OnDestroy()
        {
            CurrentDirection.UnSubscribeOnChange(OnChangeDirection);
        }

        private void OnChangeDirection(MoveDirection value)
        {
            _currentDirection = value;
            if (!isMoving)
            {
                switch (value)
                {
                    case MoveDirection.None:
                        _direction = Vector3.zero;
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
            }
            
        }

       
        void Update()
        {
            if (isMoving)
            {
                Move();
                return;
            }
            if (ButtonDown)
                Move();
        }

        private void Move()
        {
            isMoving = true;
            timer += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(_tempPosition, _tempPosition + _direction, timer);
            if (timer >= 1)
            {
                _tempPosition = transform.position;
                timer = 0;
                isMoving = false;
                CheckDirection();
            }
        }

        private void CheckDirection()
        {
            if (_previousDirection != _currentDirection)
            {
                _previousDirection = _currentDirection;
                OnChangeDirection(_currentDirection);
                Move();
            }
        }

    }
}
