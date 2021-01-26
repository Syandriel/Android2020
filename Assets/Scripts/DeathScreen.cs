using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public Image image;
    public float fadeSpeed = 0.1f;

    private float fade = 0;

    public void Start() {
        
    }

    public void Update() {
        image.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), fade);
        if(fade > 0) {
            fade += fadeSpeed;
        }
        if(fade <= 0) {
            fade = 0;
        }
    }

    public void TryAgain() {
        SceneManager.LoadScene(1);
    }

}
