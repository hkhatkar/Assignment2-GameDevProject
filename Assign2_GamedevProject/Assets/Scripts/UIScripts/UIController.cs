using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    public void loadgame(){
        SceneManager.LoadScene("mainGame");
    }

    public void loadcred(){
        SceneManager.LoadScene("creditScreen");
    }

    public void loadmenu(){
        SceneManager.LoadScene("mainMenu");
    }
}
