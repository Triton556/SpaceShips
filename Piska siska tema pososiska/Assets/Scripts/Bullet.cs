﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public float direction = 0;
    float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        damage = Player.bulletDamage;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionVector = new Vector3(0, 0, direction);
        transform.Translate(directionVector * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage(damage);
        }
    }
}