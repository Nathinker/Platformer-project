using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePos : MonoBehaviour
{
    [SerializeField] GameObject turretObject;
    [SerializeField] GameObject bulletPre;
    [SerializeField] float fireRate = 5f;
    [SerializeField] float turnRate = 5f;
    private Transform firePos;
    public int turretDirection = 2;
    private float switchTimer = 0f;
    private float switchMax = 60f;
    private int turnDirection = 0;

    void Start()
    {
        firePos = GetComponent<Transform>();
        switchMax = 60 * turnRate;
        Invoke("Shoot", fireRate);
    }

    void Update()
    {
        switchMax = 60 * turnRate;
        switchTimer = (switchTimer + 1f) % switchMax;
        // Debug.Log($"SwitchTimer {turretObject.name}: {switchTimer}");
        if (switchTimer <= 0)
        {
            switchTimer = 0;
            if (turretDirection <= 0)
            {
                turnDirection = 0;
            }
            else if (turretDirection >= 4)
            {
                turnDirection = 1;
            }

            switch (turnDirection)
            {
                case 0:
                    turretDirection = (turretDirection + 1) % 5;
                    break;
                case 1:
                    turretDirection = (turretDirection - 1) % 5;
                    break;
                default:
                    break;
            }
        }
    }

    private void Shoot()
    {
        if (turretDirection > 4 || turretDirection < 0)
        {
            turretDirection = 2;
        }

        var turretRotation = turretObject.GetComponent<Transform>().rotation;
        var spawnPosition = firePos.position;
        var spawnRotation = firePos.rotation;

        switch (turretDirection)
        {
            case 0:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 90f);
                break;
            case 1:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 45f);
                break;
            case 2:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, -45f);
                break;
            case 4:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, -90f);
                break;
            default:
                break;
        }

        Instantiate(bulletPre, spawnPosition, spawnRotation);

        Invoke("Shoot", fireRate);
    }
}
