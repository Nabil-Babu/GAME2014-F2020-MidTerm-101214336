/**
    BulletManager.cs
    Author: Prof. Tommy Tsiliopoulos
    Contributor: Nabil Babu
    101214336
    Oct 20th 2020
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;
    public float horizontalSpeed;
    public float horizontalBoundary;
    [SerializeField]
    private Vector3 landscapeScale;
    [SerializeField]
    private Vector3 portraitScale;

    private bool _landscapeMode; 
    public bool LandscapeMode 
    { 
        get
        {
            return _landscapeMode;
        } 
        set // The set method also changes the rotation and scale
        {
            _landscapeMode = value;
            if(_landscapeMode)
            {
                transform.rotation = Quaternion.Euler(0,0,-90);
                transform.localScale = landscapeScale;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                transform.localScale = portraitScale;
            }
            
        } 
    }

    // Update is called once per frame
    void Update()
    {
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
    // reseting the position of the background in portrait mode
    private void _Reset()
    {
        transform.position = new Vector3(0.0f, verticalBoundary);
    }
    // Moving background in portrait mode
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
    // Resetting the background for landscape mode
    private void _ResetLandscape()
    {
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }
    // Moving the background in landscape mode
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
