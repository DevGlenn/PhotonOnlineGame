using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerName : MonoBehaviourPunCallbacks
{
    const string playerName = "PlayerName";

    private void Start()
    {
        string defaultName = string.Empty;

        InputField nameField = this.GetComponent<InputField>();
            
        if (nameField != null)
        {
            // Put whatever you put in the inputfield in the playerprefs so its stored for next time you launch the game
            if (PlayerPrefs.HasKey(playerName))
            {
                defaultName = PlayerPrefs.GetString(playerName);
                nameField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }

    public void SetName(string name)
    {
        // if the player hasn't filled in a name, the game will prompt the player to fill one in
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Fill in a player name");
            return;
        }
        // the name the player filled in will be stored in the playerprefs each time a new entry is made, overwriting the last
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(playerName, name);
    }
}
