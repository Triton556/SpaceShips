using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGEndless : MonoBehaviour
{
    public float speed = 2f;
    
    private GameObject BG;
    
    void Start()
    {
        BG = this.gameObject;
    }
    
    void Update()
    {
        BG.transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
        if (BG.transform.position.z < -100f)
        {
            Instantiate(BG, new Vector3(0f, 0f, 100f), Quaternion.Euler(-90f, 0f, 0f));
            Destroy(this.gameObject);
        }
    }
}
