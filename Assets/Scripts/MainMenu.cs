using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

//Handler for the main menu
public class MainMenu : MonoBehaviour {

    [SerializeField] NetworkManager networkManager = null;

    //Starts the singleplayer
    public void StartSinglePlayer() {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    //Quits the game
    public void QuitGame() {
        Debug.Log("Quit"); //Application.Quit() doesn't work in the editor, added Debuglog to determin if method is called.
        Application.Quit(); 
    }

    //Hosts the local multiplayer server
    public void HostMultiplayer() {
        networkManager.StartHost();
        networkManager.ServerChangeScene("Level1Multiplayer");
        Debug.Log("Hosting Multiplayer...");
    }

    //Joins the local multiplayer server
    public void JoinLocalMultiplayer() {
        networkManager.StartClient();
        Debug.Log("Joining Multiplayer...");
    }

}
