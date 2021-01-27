﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenu: MonoBehaviour
{
    public void StartSinglePlayer() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HostMultiplayer() {
        
        SceneManager.LoadScene("DeathScreen");
        Debug.Log("Hosting Multiplayer...");
    }

    public void JoinLocalMultiplayer() {
        
        Debug.Log("Joining Multiplayer...");
    }

}
