/**
    BulletController.cs
    Author: Prof. Tommy Tsiliopoulos
    Contributor: Nabil Babu
    101214336
    Oct 20th 2020
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IApplyDamage
{
    public float verticalSpeed;
    public float verticalBoundary;
    public float horizontalBoundary;
    public BulletManager bulletManager;
    public int damage;

    private bool _landscapeMode;
    public bool LandscapeMode 
    { 
        get
        {
            return _landscapeMode;
        } 
        set // The set property also changes the rotation
        {
            _landscapeMode = value;
            if(_landscapeMode == true)
            {
                transform.rotation = Quaternion.Euler(0,0,-90);
            } 
            else 
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        } 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
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

    private void _Move()
    {
        transform.position += new Vector3(0.0f, verticalSpeed, 0.0f) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (transform.position.y > verticalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }
    // Same as _Move but for Landscape mode
    private void _MoveLandscape()
    {
        transform.position += new Vector3(verticalSpeed, 0.0f, 0.0f) * Time.deltaTime;
    }
    // Checking boundaries in landscape mode
    private void _CheckBoundsLandscape()
    {
        if (transform.position.x > horizontalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }

}
