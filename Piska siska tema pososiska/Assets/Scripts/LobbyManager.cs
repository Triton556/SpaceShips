using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text textMessage;
    public GameObject panel;
    private Coroutine cor;
    void Start()
    {
        panel.SetActive(false);
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 1000);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        
        PhotonNetwork.Disconnect();
    }

    public void ClickCooperativeButton()
    {
        //Присоединяемся к серверу
        PhotonNetwork.ConnectUsingSettings();
        panel.SetActive(true);
        Log("Waiting for another player", true);
    }

    public void CloseWaitWindow()
    {
        //Отключаемся от сервера
        PhotonNetwork.Disconnect();
        panel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        StartGame();
    }

    public void StartGame()
    {
        //Присоединение к ранее созданной команыт или создание комнаты, к которой могут подключиться другие игроки
        PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.NickName, new Photon.Realtime.RoomOptions {MaxPlayers = 2}, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
            Log("Waiting for another player", true);
        else
        {
            Log("The player is found");
            //Загрузка сцены
            PhotonNetwork.LoadLevel("CooperativeGame");
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Log("The player is found");
        PhotonNetwork.LoadLevel("CooperativeGame");
    }

    private void Log(string message, bool log=false)
    {
        textMessage.text = message;
        if (cor != null)
        {
            StopCoroutine(cor);
            if (log)
                cor = StartCoroutine(UpdateText());
        }
        else if (log)
            cor = StartCoroutine(UpdateText());
    }

    public IEnumerator UpdateText()
    {
        string tmp = textMessage.text;
        while (true)
        {
            if (textMessage.text.Contains("..."))
                textMessage.text = tmp;
            
            yield return new WaitForSeconds(0.4f);
            textMessage.text += ".";

            yield return new WaitForSeconds(0.4f);
        }
    }
}
