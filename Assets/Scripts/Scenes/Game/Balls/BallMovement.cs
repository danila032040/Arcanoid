﻿using System;
using Scenes.Game.Paddles;
using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;


        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _maxSpeed;

        private float _currentSpeedProgress = 0f;

        private void Update()
        {
            NormalizeVelocity(_rb.velocity.normalized);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ReflectBallVelocity(collision);
            
            CheckCollisionWithPaddle(collision);
            
        }

        private void ReflectBallVelocity(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 velocity = -collision.relativeVelocity;

            _rb.velocity = Vector2.Reflect(velocity, normal);
        }

        private void CheckCollisionWithPaddle(Collision2D collision)
        {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            if (!paddle) return;
            
            _rb.velocity = Vector2.zero;

            float x = (this.transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;

            Vector2 direction = new Vector2(x, 1).normalized;

            _rb.velocity = direction * GetCurrentVelocity();
        }


        private void NormalizeVelocity(Vector2 direction)
        {
            _rb.velocity = direction.normalized * GetCurrentVelocity();
            
            if (Vector2.Angle(_rb.velocity, Vector2.left) <= 10 || 
                Vector2.Angle(_rb.velocity, Vector2.right) <= 10)
            {
                _rb.velocity = Quaternion.Euler(0, 0, UnityEngine.Random.Range(15f, 30f)) * _rb.velocity;
            }
        }

        private float GetCurrentVelocity() => Mathf.Lerp(_initialSpeed, _maxSpeed, _currentSpeedProgress);


        public void StartMoving()
        {
            NormalizeVelocity(Vector2.up);
        }
    }
}
