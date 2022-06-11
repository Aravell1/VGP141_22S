using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 2;
    public float ShootCooldown = 1.0f;
    public float bulletSpeed = 10.0f;

    public BulletPool b;
    public Transform BulletSpawnPoint;

    private Rigidbody _rb;
    private float _shootCooldownTime;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool isSpacePressed = Input.GetKey(KeyCode.Space);

        Vector3 inputVector = new Vector3(x, 0, y);

        _rb.velocity = inputVector * MoveSpeed;

        if (isSpacePressed)
        {
            if (_shootCooldownTime == 0)
            {
                Debug.Log("Shooting");
                _shootCooldownTime = ShootCooldown;
                b.Create(BulletSpawnPoint.transform.position.x, BulletSpawnPoint.transform.position.y, transform.forward.x * bulletSpeed, transform.forward.z * bulletSpeed, 10000);
            }
        }
        b.Animate();

        if (_shootCooldownTime > 0)
        {
            _shootCooldownTime -= Time.deltaTime;
        }

        if (_shootCooldownTime < 0)
        {
            _shootCooldownTime = 0;
        }
    }
}
