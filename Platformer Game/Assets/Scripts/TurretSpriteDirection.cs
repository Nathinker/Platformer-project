using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpriteDirection : MonoBehaviour
{
    #region Fields
    public Sprite[] turretSprites;
    private SpriteRenderer sr;
    private int turretDir;
    #endregion

    #region Start

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        turretDir = GetComponentInChildren<FirePos>().turretDirection;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = turretSprites[turretDir];
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        turretDir = GetComponentInChildren<FirePos>().turretDirection;
        if (turretDir <= 4 && turretDir >= 0)
        {
            sr.sprite = turretSprites[turretDir];
        }
    }
    #endregion
}
