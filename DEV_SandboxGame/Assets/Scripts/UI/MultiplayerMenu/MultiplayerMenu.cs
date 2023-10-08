using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerMenu : MonoBehaviour
{

    public void HostGame()
    {
        SceneManager.LoadScene("Vehicle Car");
        GlobalInformation.multiplayerMode = MultiplayerMode.Host;
        //NetworkManager.Singleton.StartHost();
    }
    public void JoinGame()
    {
        SceneManager.LoadScene("Vehicle Car");
        //NetworkManager.Singleton.StartClient();
        GlobalInformation.multiplayerMode = MultiplayerMode.Client;
    }
}
