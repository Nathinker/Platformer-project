using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePos : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject turretObject;
    [SerializeField] GameObject bulletPre;
    [SerializeField] float fireRate = 5f;
    [SerializeField] float turnRate = 5f;
    private Transform firePos;
    private AudioSource shootSound;
    public int turretDirection = 2;
    private float switchTimer = 0f;
    private float switchMax = 60f;
    private int turnDirection = 0;
    Quaternion turretRotation;
    Vector3 spawnPosition;
    Quaternion spawnRotation;
    #endregion

    #region Start
    void Start()
    {
        firePos = GetComponent<Transform>();
        switchMax = 60 * turnRate;
        turretRotation = turretObject.GetComponent<Transform>().rotation;
        spawnPosition = firePos.position;
        spawnRotation = firePos.rotation;
        Invoke("Shoot", fireRate);
    }
    #endregion

    #region Update
    void Update()
    {
        switchMax = 60 * turnRate;
        switchTimer = (switchTimer + 1f) % switchMax;
        // Debug.Log($"SwitchTimer {turretObject.name}: {switchTimer}");
        if (switchTimer <= 0)
        {
            switchTimer = 0;
            if (turretDirection <= 1)
            {
                turnDirection = 0;
            }
            else if (turretDirection >= 3)
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
    #endregion

    #region Shoot
    // Shoots a bullet from the turrets aim point.
    private void Shoot()
    {
        if (turretDirection > 3 || turretDirection < 1)
        {
            turretDirection = 2;
        }

        switch (turretDirection)
        {
            case 0:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 90f);
                Vector3 offset = turretRotation * new Vector3(firePos.localPosition.x - 0.5f, firePos.localPosition.y, firePos.localPosition.z);
                spawnPosition = firePos.position + offset;
                break;
            case 1:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 45f);
                offset = turretRotation * new Vector3(firePos.localPosition.x - 0.25f, firePos.localPosition.y + 0.4375f, firePos.localPosition.z);
                spawnPosition = firePos.position + offset;
                break;
            case 2:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, 0);
                offset = turretRotation * new Vector3(firePos.localPosition.x, firePos.localPosition.y + 0.5f, firePos.localPosition.z);
                spawnPosition = firePos.position + offset;
                break;
            case 3:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, -45f);
                offset = turretRotation * new Vector3(firePos.localPosition.x + 0.25f, firePos.localPosition.y + 0.4375f, firePos.localPosition.z);
                spawnPosition = firePos.position + offset;
                break;
            case 4:
                spawnRotation = turretRotation * Quaternion.Euler(0, 0, -90f);
                offset = turretRotation * new Vector3(firePos.localPosition.x + 0.5f, firePos.localPosition.y, firePos.localPosition.z);
                spawnPosition = firePos.position + offset;
                break;
            default:
                break;
        }

        Instantiate(bulletPre, spawnPosition, spawnRotation);
        Debug.Log($"{turretObject.name}: Shot bullet");

        Invoke("Shoot", fireRate);
    }
    #endregion
}
