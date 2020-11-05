using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadRoom()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            // if we aren't the master client print this message
            Debug.LogError("Trying to load a level but we are not the master client");
        }
        // load the correct room for the amount of players we are with
        Debug.LogFormat("Loading level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room_for_" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // you won't see this msg when you are the player connecting to a room
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadRoom();
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // see this message when a different player disconnects
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName); 


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom() IsMasterClient {0}", PhotonNetwork.IsMasterClient); 
            LoadRoom();
        }

    }



}
