﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;
    public float horizontalSpeed;
    public float horizontalBoundary;

    private bool _landScapeMode = true; 

    // Update is called once per frame
    void Update()
    {
        if(_landScapeMode)
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

    private void _Reset()
    {
        transform.position = new Vector3(0.0f, verticalBoundary);
    }

    private void _Move()
    {
        transform.position -= new Vector3(0.0f, verticalSpeed) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        // if the background is lower than the bottom of the screen then reset
        if (transform.position.y <= -verticalBoundary)
        {
            _Reset();
        }
    }

    private void _ResetLandscape()
    {
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }

    private void _MoveLandscape()
    {
        transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
    }

    private void _CheckBoundsLandscape()
    {
        // if the background is farther than the left side of the screen then reset
        if (transform.position.x <= -horizontalBoundary)
        {
            _ResetLandscape();
        }
    }
}