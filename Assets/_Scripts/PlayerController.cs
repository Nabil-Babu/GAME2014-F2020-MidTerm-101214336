/**
    PlayerController.cs
    Author: Prof. Tommy Tsiliopoulos
    Contributor: Nabil Babu
    101214336
    Oct 21th 2020
*/
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    [Header("Boundary Check")]
    public float horizontalBoundary;
    public float verticalBoundary;

    [Header("Player Speed")]
    public float horizontalSpeed;
    public float maxSpeed;
    public float horizontalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;
    [SerializeField]
    private Vector3 landscapePosition;
    [SerializeField]
    private Vector3 portraitPosition;
    [SerializeField]
    private DeviceOrientation _currentSetOrientation;
    [SerializeField]
    private DeviceOrientation detectedOrientation;
    [SerializeField]
    private bool _landscapeMode = true;
    public bool LandscapeMode 
    { 
        get
        {
            return _landscapeMode;
        } 
        set // The set property also changes the rotation
        {
            _landscapeMode = value;
            if(_landscapeMode)
            {
                SetLandscapePositionandRotation();
            } 
            else 
            {
                SetPortraitPositionandRotation();
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        DetectOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        DetectOrientation();
        if(_landscapeMode)
        {
            _MoveLandscape();
            _CheckBoundsLandscape();
        } 
        else 
        {
            _Move();
            _CheckBounds();
        }
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.x != 0.0f)
        {
           transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    private void _CheckBounds()
    {
        // check right bounds
        if (transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }

        // check left bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }

    }

    private void _CheckBoundsLandscape()
    {
        // check right bounds
        if (transform.position.y >= verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundary, 0.0f);
        }

        // check left bounds
        if (transform.position.y <= -verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
        }

    }

     private void _MoveLandscape()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.y > transform.position.y)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (worldTouch.y < transform.position.y)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.y != 0.0f)
        {
           transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, horizontalTValue));
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(0.0f, direction * horizontalSpeed);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    // Checks for current orientation of the device and sets it on change
    void DetectOrientation()
    {
        detectedOrientation = Input.deviceOrientation;
        if(detectedOrientation != _currentSetOrientation)
        {
            switch(detectedOrientation)
            {
                case DeviceOrientation.LandscapeLeft:
                    _currentSetOrientation = DeviceOrientation.LandscapeLeft;
                    this.LandscapeMode = true;
                    break;
                case DeviceOrientation.LandscapeRight:
                    _currentSetOrientation = DeviceOrientation.LandscapeRight;
                    this.LandscapeMode = true;
                    break;
                case DeviceOrientation.Portrait:
                    _currentSetOrientation = DeviceOrientation.Portrait;
                    this.LandscapeMode = false;
                    break;
                case DeviceOrientation.Unknown:
                    break;
            }
        }
    }
    // Settings for when this object is in a Landscape scene
    void SetLandscapePositionandRotation()
    {
        transform.position = landscapePosition;
        transform.rotation = Quaternion.Euler(0,0,-90);
    }
    // Settings for when this object is in a portrait scene
    void SetPortraitPositionandRotation()
    {
        transform.position = portraitPosition;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
