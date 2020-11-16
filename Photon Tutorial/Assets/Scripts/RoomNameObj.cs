using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomNameObj : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public RoomInfo info;
    public void RoomSetUp(RoomInfo roomInfo) // assinging the roominfo to info
    {
        info = roomInfo;
        text.text = roomInfo.Name;
    }

    public void OnClick()
    {
        ServerLauncher.instance.JoinRoom(info);
    }
}
