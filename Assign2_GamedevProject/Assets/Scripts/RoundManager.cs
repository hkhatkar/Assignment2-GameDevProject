using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    int currentRound;
    public int currentKillCount; //increment in enemy scripts before they die
    int objectiveKills=0;
    int coinCount = 0;
    bool inRound=false;
    placeSpawners placeSpawnerScript;
    GameObject[] allEnemies;
    GameObject[] allSpawners;
    
    [SerializeField] GameObject shopObject;
    [SerializeField] GameObject enterRoundPrompt;
    [SerializeField] GameObject roundStartUI;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject enemySpawner;
    
    
    // Start is called before the first frame update
    void Start()
    {
       placeSpawnerScript = (GameObject.FindGameObjectWithTag("spawnManager")).GetComponent<placeSpawners>();
    }

    // Update is called once per frame
    void Update()
    {
        //manage rounds, shop, when objective met
        //also clears enemies after round
        //and ui
        //ON DEATH RESET
        if (inRound == true){
            objectiveText.text = "Objective:  Kill " + objectiveKills +  " enemies || Killed: " + currentKillCount;
        }

        if (Input.GetKeyUp(KeyCode.Return) && inRound == false ) // enter key 
        {
            
            placeSpawnerScript.placeSpawnersRandomly();
            shopObject.SetActive(false);
            objectiveKills = objectiveKills + 5;
            enterRoundPrompt.SetActive(false);
            inRound = true;
            currentRound = currentRound+1;
            roundText.text = "Round " + currentRound;
            roundStartUI.SetActive(true);
            enemySpawner.SetActive(true);
            //add spawner (set active) and change spanwer script to spawn in multiple locations

            //if objective met, inRound = false
            
        }
        if (inRound == true && (currentKillCount >= objectiveKills))
        {//runs only once, as currentKillCount is reset back to 0
            objectiveText.text = "Objective:  Complete! ";
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies) {
                Destroy(enemy);
            }

            allSpawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in allSpawners) {
                Destroy(spawner);
            }

            currentKillCount = 0;
            enterRoundPrompt.SetActive(true);
            shopObject.SetActive(true);
            enterRoundPrompt.SetActive(true);
            inRound=false;

        }
    }

    public void incrementCoin() {
        coinCount = coinCount + 1;
        coinText.text = "Coins: " + coinCount;
    }
}