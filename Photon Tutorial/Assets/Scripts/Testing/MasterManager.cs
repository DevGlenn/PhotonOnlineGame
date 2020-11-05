using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance { get; private set; }

    public int value;

    private void Awake()
    {
        // if nothing is currently in the instance since loading the game, set instance to something.
        if (Instance == null)
        {
            Instance = this;
            // if a new scene is loaded, don't destroy the gameObject which this script is located on
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // if a second instance of this gameObject is located within a scene. Destroy all humans!
            Destroy(gameObject);
        }
    }
}
