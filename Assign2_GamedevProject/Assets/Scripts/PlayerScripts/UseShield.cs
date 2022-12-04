using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShield : MonoBehaviour
{
    [SerializeField]
    GameObject shieldObject;
    playerPowerBar powerScript;
    [SerializeField]
    GameObject powerOutParticle;
    public bool shieldUp = false; //used in Player2D to check whether player takes damage
    
    //TDMove moveScript;
    public bool disabledShield = false;
   
    // Start is called before the first frame update
    void Start()
    {
       // moveScript = GameObject.Find("Player").GetComponent<TDMove>();
        powerScript = GameObject.Find("powerFillBar").GetComponent<playerPowerBar>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && powerScript.currentValue > 0 && disabledShield == false)
        {
           
            shieldUp = true;
            shieldObject.SetActive(true);
           // moveScript.animator.SetFloat("animSpeedMultiplier", 0.5f);
           // moveScript.moveSpeed = 0.8f;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) || powerScript.currentValue <= 0)
        {

            shieldUp = false;
           // moveScript.animator.SetFloat("animSpeedMultiplier", 1f);
            shieldObject.SetActive(false);
            //moveScript.moveSpeed = 2f;
            if ( powerScript.currentValue <= 0)
            {
            StartCoroutine(cooldownForPower());
            }
        }
  
    }

    IEnumerator cooldownForPower()
    {
        Instantiate(powerOutParticle, gameObject.transform.position, Quaternion.identity);
        disabledShield = true;
        yield return new WaitForSeconds(4f);
        disabledShield = false;

    }
}
