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
        [SerializeField] private Projectile _bullet;
        [SerializeField] private Transform _bulletStartPosition;

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

        void Update()
        {
            if (isMoving)
            {
                Move(_direction);
                return;
            }
            if (ButtonDown)
                isMoving = true;
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
                if(ButtonDown && _previousDirection == _currentDirection)
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

        public void Fire()
        {
            var bullet = GameObject.Instantiate(_bullet, _bulletStartPosition.position, transform.rotation);
            var rigidBody = bullet.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(_direction * bullet.Speed, ForceMode2D.Force);
            Destroy(bullet.gameObject, 2f);
        }

    }
}
