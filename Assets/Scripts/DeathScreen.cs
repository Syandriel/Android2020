using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Handler for the death screen
public class DeathScreen : MonoBehaviour { 

    //Loads the first Level again
    public void TryAgain() {
        SceneManager.LoadScene(1);
    }

}
