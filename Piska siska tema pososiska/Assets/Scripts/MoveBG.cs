using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float speed = 4f;

    private GameObject obj;

    private void Start()
    {
        obj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
        if (transform.position.y > 1000f || transform.position.y < -1000f)
            speed = -speed;
    }
}
