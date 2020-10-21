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

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public BulletFactory bulletFactory;
    public int MaxBullets;

    private Queue<GameObject> m_bulletPool;
    [SerializeField]
    private bool _landscapeMode;
    public bool LandscapeMode 
    { 
        get
        {
            return _landscapeMode;
        } 
        set
        {
            _landscapeMode = value;
        } 
    }


    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    void Update()
    {
        DetectOrientation();
    }

    private void _BuildBulletPool()
    {
        // create empty Queue structure
        m_bulletPool = new Queue<GameObject>();

        for (int count = 0; count < MaxBullets; count++)
        {
            var tempBullet = bulletFactory.createBullet();
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        if(_landscapeMode)
        {
            newBullet.GetComponent<BulletController>().LandscapeMode = true; 
        } 
        else 
        {
            newBullet.GetComponent<BulletController>().LandscapeMode = false;
        }
        return newBullet;
    }

    public bool HasBullets()
    {
        return m_bulletPool.Count > 0;
    }

    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        m_bulletPool.Enqueue(returnedBullet);
    }

    void DetectOrientation()
    {
        switch(Input.deviceOrientation)
        {
            case DeviceOrientation.LandscapeLeft:
                this.LandscapeMode = true;
                break;
            case DeviceOrientation.LandscapeRight:
                this.LandscapeMode = true;
                break;
            case DeviceOrientation.Portrait:
                this.LandscapeMode = false;
                break;
            case DeviceOrientation.Unknown:
                break;
        }
    }
}
