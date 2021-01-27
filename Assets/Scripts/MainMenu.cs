using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenu: MonoBehaviour {

    [SerializeField] NetworkManager networkManager = null;

    public void StartSinglePlayer() {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HostMultiplayer() {

        networkManager.StartHost();
        networkManager.ServerChangeScene("Level1Multiplayer");
        Debug.Log("Hosting Multiplayer...");
    }

    public void JoinLocalMultiplayer() {
        
        networkManager.StartClient();
        Debug.Log("Joining Multiplayer...");
    }

}
