using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem warp;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _sensetive = 100f;

    private Rigidbody _rb;

    private float _moveDirection = 0;
    private Vector2 _startPosition;
    private Vector3 _lastPosition;
    private float _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _velocity = (transform.position - _lastPosition).magnitude / Time.deltaTime;
        _lastPosition = transform.position;

        //Control
        if (!StickmanControl.isDead)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.x /= Screen.width;

            if (Input.GetMouseButtonDown(0))
                _startPosition = mouse;

            if (Input.GetMouseButton(0))
            {
                _moveDirection = mouse.x - _startPosition.x;
                _moveDirection *= _sensetive;

                _startPosition = mouse;
            }

            if (Input.GetMouseButtonUp(0))
                _moveDirection = 0;

            transform.Translate(((Vector3.right * _moveDirection * _speed) + (Vector3.forward * _speed)) * Time.deltaTime);
        }

        //Warp effect
        if(_velocity > 0f) warp.Play();
        else warp.Stop();

        //Staying position per y
        if(transform.position.y > 0.515)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (transform.position.x > 2f)
            transform.position = new Vector3(2f, transform.position.y, transform.position.z);
        else if(transform.position.x < -2f)
            transform.position = new Vector3(-2f, transform.position.y, transform.position.z);
    }
}