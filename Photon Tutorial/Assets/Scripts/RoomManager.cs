 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        // standard singleton stuff
        if(Instance) // if one other roommanager instance exists within the scene
        {
            Destroy(gameObject); // destroy it
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        SceneManager.sceneLoaded += LoadScene;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadScene;
    }

    public void LoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) // if we're in the game scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); // instantiate the PlayerManager for every player who enters
            // Because photon can only use things out of the resource folder it has to go from there.
        }
    }
   
}
