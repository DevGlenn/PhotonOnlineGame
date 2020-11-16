using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player;
    public void PlayerNameSetUp(Player playerr)
    {
        player = playerr;
        text.text = playerr.NickName; // set the text to the desired nickname of the playerrrrrrrrrrrrrrrrrrrrrrrrrrrr
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
