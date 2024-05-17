using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePos : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bulletPre;
    [SerializeField] float fireRate = 5f;

    void Start()
    {
        Invoke("Shoot", fireRate);
    }


    void Update()
    {

    }

    

    private void Shoot()
    {
        Instantiate(bulletPre, firePos.position, transform.rotation);

        
        Invoke("Shoot", fireRate);
    }
}
