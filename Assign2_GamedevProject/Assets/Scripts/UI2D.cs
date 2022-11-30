using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI2D : MonoBehaviour
{
   public Text scoreText;
   public Slider healthSlider; // Reference to the UI's health bar.
   void Update () {
    scoreText.text = "Score: " + GetComponent<Player2D> ().DisplayScore ();
    healthSlider.value = GetComponent<Player2D> ().DisplayHP () / 3.0f;

    }

    public void restartLevel(){
        SceneManager.LoadScene("mainGame");
    }
}
