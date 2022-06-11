using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*public float MoveSpeed = 10.0f;

    private Rigidbody _rb;

    public void Init(Vector3 dir)
    {
        _rb.velocity = dir * MoveSpeed;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }*/

    private int framesLeft_;
    private float xVel_, yVel_;

    public void Init(float x, float y, float xVel, float yVel, int lifetime)
    {
        transform.position = new Vector3(x, 0, y);
        xVel_ = xVel;
        yVel_ = yVel;
        framesLeft_ = lifetime;
    }

    public void Animate()
    {
        if (!InUse())
        {
            gameObject.SetActive(false);
            return;
        }

        framesLeft_--;
        transform.position = new Vector3(transform.position.x + xVel_, 0, transform.position.y + yVel_);
    }

    public bool InUse()
    {
        return framesLeft_ > 0;
    }

}
