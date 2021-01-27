using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Handler for the level transition screen
public class LevelTransition : MonoBehaviour {
    
    //loads the next level
    public void LoadNextLevel(int level) {
        SceneManager.LoadScene(level);
    }

}
