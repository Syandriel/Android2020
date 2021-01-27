using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

//Handler for the main menu
public class MainMenu : MonoBehaviour {

    //Starts the singleplayer
    public void StartSinglePlayer() {
        SceneManager.LoadScene(1);
    }

    //Quits the game
    public void QuitGame() {
        Debug.Log("Quit"); //Application.Quit() doesn't work in the editor, added Debuglog to determin if method is called.
        Application.Quit(); 
    }

    //Hosts the local multiplayer server
    public void HostMultiplayer() {

        SceneManager.LoadScene("DeathScreen");
        Debug.Log("Hosting Multiplayer...");
    }

    //Joins the local multiplayer server
    public void JoinLocalMultiplayer() {

        Debug.Log("Joining Multiplayer...");
    }

}
