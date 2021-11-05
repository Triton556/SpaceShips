using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerPhoton : MonoBehaviour
{
    public GameObject PlayerPrefab;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-20f, 20f), 0f, -25f);
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
    }
}
