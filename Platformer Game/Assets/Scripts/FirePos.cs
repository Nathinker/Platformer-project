using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePos : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] GameObject bulletPre;
    [SerializeField] float fireRate = 5f;
    public int turretDirection = 2;

    void Start()
    {
        Invoke("Shoot", fireRate);
    }

    void Update()
    {

    }

    private void Shoot()
    {
        if (turretDirection > 4 || turretDirection < 0)
        {
            turretDirection = 2;
        }

        switch (turretDirection)
        {
            case 0:
                Instantiate(bulletPre, firePos.position - new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 0, 90f));
                break;
            case 1:
                Instantiate(bulletPre, firePos.position - new Vector3(0.3125f, -0.3125f, 0), Quaternion.Euler(0, 0, 45f));
                break;
            case 2:
                Instantiate(bulletPre, firePos.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                Instantiate(bulletPre, firePos.position + new Vector3(0.3125f, 0.3125f, 0), Quaternion.Euler(0, 0, -45f));
                break;
            case 4:
                Instantiate(bulletPre, firePos.position + new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 0, -90f));
                break;
            default:
                break;
        }
        
        Invoke("Shoot", fireRate);
    }
}
