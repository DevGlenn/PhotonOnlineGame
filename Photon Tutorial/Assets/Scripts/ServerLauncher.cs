using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class ServerLauncher : MonoBehaviourPunCallbacks
{
    public static ServerLauncher instance; // create an instance of this script so if we switch scenes this only happens once
    [SerializeField] TMP_InputField roomNameField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListObject;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerNameObj;
    [SerializeField] GameObject startGameButton;
   

    private void Awake()
    {
        instance = this; // set the instance to this script
       
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        Debug.Log("Player is connected to the Master Server");
        PhotonNetwork.JoinLobby(); // make sure the players enter the lobby 
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuScreenManager.Instance.OpenMenuScreen("title"); // open the title screen
        Debug.Log("Joined a Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
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
        roomNameText.text = PhotonNetwork.CurrentRoom.Name; // change the text to the name the host gave the room
        Debug.Log("Joined a Room");
        foreach(Transform playerNames in playerListContent) // destroys each player in the roommenu after the game is emptied
        {
            Destroy(playerNames.gameObject);
        }
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++) // look through all the players currently in the game
        {
            Instantiate(playerNameObj, playerListContent).GetComponent<PlayerName>().PlayerNameSetUp(players[i]); // instantiate each name as a player joins the room
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); // make the start button appear only for the host
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); // if the a host migration took place, set the button active for the new host
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Creating A Room failed: " + message;
        MenuScreenManager.Instance.OpenMenuScreen("error");
    }

    
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuScreenManager.Instance.OpenMenuScreen("loading");
       
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left the Room");
        MenuScreenManager.Instance.OpenMenuScreen("loading");
    }

    public void LoadGame()
    {
        PhotonNetwork.LoadLevel(1); // load every player with PhotonNetwork not just the host
    }

    public override void OnLeftRoom()
    {
        MenuScreenManager.Instance.OpenMenuScreen("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) // stores the values of my room
    {
        foreach(Transform transform in roomListContent)
        {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList) // when a room is emptied actualy remove it from the rooms list
            {
                continue; // dont do the next line
            }
            Instantiate(roomListObject, roomListContent).GetComponent<RoomNameObj>().RoomSetUp(roomList[i]); // instantiate our namebutton in our content layer.
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerNameObj, playerListContent).GetComponent<PlayerName>().PlayerNameSetUp(newPlayer); // instantiate our name in our content layer.
    }
}
