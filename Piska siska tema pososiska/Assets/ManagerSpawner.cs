using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameController gmc;

    private float timeActivation;
    private int numberSpawner;
    void Start()
    {
        gmc = gmc.GetComponent<GameController>();
        
        spawners[gmc.GetCurrentLevel() - 1].SetActive(true);
        
    }
    
    void Update()
    {
        timeActivation += Time.deltaTime;

        if (gmc.GetCurrentLevel() == 2)
        {
            spawners[gmc.GetCurrentLevel() - 1].SetActive(true);
        }
        else if (gmc.GetCurrentLevel() == 3)
        {
            spawners[gmc.GetCurrentLevel() - 1].SetActive(true);
        }
            
    }
}
