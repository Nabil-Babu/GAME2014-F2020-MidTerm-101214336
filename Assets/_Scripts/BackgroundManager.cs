/**
    BackgroundManager.cs
    Author: Nabil Babu
    101214336
    Oct 21th 2020
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public BackgroundController background1;
    public BackgroundController background2;
    [SerializeField]
    private bool _landscapeMode;
    [SerializeField]
    private DeviceOrientation _currentSetOrientation;
    [SerializeField]
    private DeviceOrientation detectedOrientation;
    public bool LandscapeMode 
    { 
        get
        {
            return _landscapeMode;
        } 
        set
        {
            _landscapeMode = value;
            if(_landscapeMode)
            {
                SetLandscapeMode();
            } 
            else 
            {
                SetPortraitMode();
            }
        } 
    } 

    void Start()
    {
        DetectAndSetOrientation();
    }

    void Update()
    {
        DetectAndSetOrientation();
    }
    // Checks for current orientation of the device and sets it on change
    void DetectAndSetOrientation()
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
    // This arranges the backgrounds according to their previous y values to their new x values 
    void SetLandscapeMode()
    {
        // Flip Landscape flags for both backgrounds 
        background1.LandscapeMode = true;
        background2.LandscapeMode = true;
        // Scale position to new boundaries 
        float xPos1 = scale(-background1.verticalBoundary, 
                            background1.verticalBoundary, 
                            -background1.horizontalBoundary, 
                            background1.horizontalBoundary, 
                            background1.transform.position.y);
        background1.transform.position = new Vector3(xPos1,0,0);
        // Set background position back by boundary limit
        background2.transform.position = new Vector3(xPos1+background2.horizontalBoundary,0,0);
    }
    // This functions takes the background previous x positional value and scales it to a matching y value
    void SetPortraitMode()
    {
        background1.LandscapeMode = false;
        background2.LandscapeMode = false;
        float yPos1 = scale(-background1.horizontalBoundary, 
                            background1.horizontalBoundary, 
                            -background1.verticalBoundary, 
                            background1.verticalBoundary, 
                            background1.transform.position.x);
        background1.transform.position = new Vector3(0,yPos1,0);
        background2.transform.position = new Vector3(0,yPos1+background2.verticalBoundary,0);
    }
    // Scale method for mapping a value from one range to another. 
    public float scale(float OldMin, 
                       float OldMax, 
                       float NewMin, 
                       float NewMax, 
                       float OldValue){
 
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
 
        return(NewValue);
    }
}
