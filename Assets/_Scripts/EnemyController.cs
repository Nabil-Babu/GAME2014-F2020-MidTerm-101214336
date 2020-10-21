/**
    EnemyController.cs
    Author: Prof. Tommy Tsiliopoulos
    Contributor: Nabil Babu
    101214336
    Oct 21th 2020
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float verticalBoundary;
    public float direction;
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

    void Start()
    {
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
    }

    private void _Move()
    {
        transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
    }

    private void _CheckBounds()
    {
        // check right boundary
        if (transform.position.x >= horizontalBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        if (transform.position.x <= -horizontalBoundary)
        {
            direction = 1.0f;
        }
    }

    private void _MoveLandscape()
    {
        transform.position += new Vector3(0.0f, horizontalSpeed * direction * Time.deltaTime, 0.0f);
    }

    private void _CheckBoundsLandscape()
    {
        // check right boundary
        if (transform.position.y >= verticalBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        if (transform.position.y <= -verticalBoundary)
        {
            direction = 1.0f;
        }
    }
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
     void SetLandscapePositionandRotation()
    {
        transform.position = landscapePosition;
        transform.rotation = Quaternion.Euler(0,0,-90);
    }

    void SetPortraitPositionandRotation()
    {
        transform.position = portraitPosition;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
