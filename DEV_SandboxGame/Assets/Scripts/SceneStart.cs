using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    public void Awake()
    {
        if(GlobalInformation.multiplayerMode == MultiplayerMode.Host) { NetworkManager.Singleton.StartHost(); }
        else { NetworkManager.Singleton.StartClient(); }
    }
}
