using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView photonView;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if(photonView.IsMine == true) // if the photonview is the local players property, then create a player for them to use
        {
            CreatePlayer();
        }
    }

    void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity); // spawn the playerprefab whenever someone joins the game
    }
}
