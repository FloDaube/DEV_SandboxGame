using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    private ControlsInputs _ControleInputs;

    public void Awake()
    {
        _ControleInputs = new ControlsInputs();
        _ControleInputs.Menu.Enable();

        _ControleInputs.Menu.Pause.performed += ctx => Pause();
    }

    public void Pause()
    {
        if (PauseMenu == null)
        {
            return;
        }
        if(PauseMenu.activeSelf)
            PauseMenu.SetActive(false);
        else
            PauseMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
