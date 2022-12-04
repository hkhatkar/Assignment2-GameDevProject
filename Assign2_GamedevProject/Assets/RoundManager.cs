using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundManager : MonoBehaviour
{
    int currentRound;
    int currentKillCount;
    int objectiveKills=20;
    bool inRound=false;

    [SerializeField] GameObject enterRoundPrompt;
    [SerializeField] GameObject roundStartUI;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] GameObject enemySpawner;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //manage rounds, shop, when objective met
        //also clears enemies after round
        //and ui
        //ON DEATH RESET
         
        if (Input.GetKeyUp(KeyCode.Return) && inRound == false ) // enter key 
        {
            enterRoundPrompt.SetActive(false);
            inRound = true;
            currentRound = currentRound+1;
            roundText.text = "Round " + currentRound;
            roundStartUI.SetActive(true);
            enemySpawner.SetActive(true);
            //add spawner (set active) and change spanwer script to spawn in multiple locations

            //if objective met, inRound = false
            
        }
    }
}
