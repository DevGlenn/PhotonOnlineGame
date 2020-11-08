using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ServerLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListObject;
    private string gameVersion = "1";  // this separates users from eachother when they try to join the same room

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        Debug.Log("Player is connected to the Master Server");
        PhotonNetwork.JoinLobby(); // make sure the players enter a lobby 
        
    }

    public override void OnJoinedLobby()
    {
        MenuScreenManager.Instance.OpenMenuScreen("title"); // open the title screen
        Debug.Log("Joined a Lobby");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameField.text)) // if the Room name input field is empty don't do anything
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomNameField.text); // create a room to be shown in the room list
        MenuScreenManager.Instance.OpenMenuScreen("load"); // open the load menuscreen
    }
    public override void OnJoinedRoom()
    {
        MenuScreenManager.Instance.OpenMenuScreen("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Joined a Room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Creating A Room failed: " + message;
        MenuScreenManager.Instance.OpenMenuScreen("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left the Room");
        MenuScreenManager.Instance.OpenMenuScreen("loading");
    }

    public override void OnLeftRoom()
    {
        MenuScreenManager.Instance.OpenMenuScreen("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) // stores are values of my room
    {
        
    }
}
