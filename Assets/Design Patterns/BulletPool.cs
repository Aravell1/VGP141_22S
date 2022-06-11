using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    const int POOL_SIZE = 100;
    Bullet[] bullets_ = new Bullet[POOL_SIZE];
    public Bullet bulletPrefab;

    private void Start()
    {
        for (int i = 0; i < POOL_SIZE; i++)
        {
            bullets_[i] = bulletPrefab;
            Instantiate(bullets_[i]);
            bullets_[i].gameObject.SetActive(false);
        }
    }

    public void Create(float x, float y, float xVel, float yVel, int lifetime)
    {
        for (int i = 0; i < POOL_SIZE; i++)
        {
            if (!bullets_[i].gameObject.activeSelf)
            {
                bullets_[i].gameObject.SetActive(true);
                bullets_[i].Init(x, y, xVel, yVel, lifetime);
                return;
            }
        }
    }

    public void Animate()
    {
        for (int i = 0; i < POOL_SIZE; i++)
        {
            bullets_[i].Animate();
        }
    }

}
