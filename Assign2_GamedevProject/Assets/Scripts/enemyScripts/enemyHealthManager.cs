using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthManager : MonoBehaviour
{
   // Start is called before the first frame update
    public Animation enemyrecoilAnim;// used in other scripts getweapon
    public float enemyKnockbackMultiplier;//used in other script getweapon
    private bool canBeHit = true;    //Can we be hit
    RoundManager roundScript;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    public Slider slider;
    public float maxHealth, currentHealth;
    // Update is called once per frame
    void Start()
    {
        roundScript = (GameObject.FindGameObjectWithTag("roundManager")).GetComponent<RoundManager>();
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void takeDamage(float damage)
    {
        if (canBeHit)
        {
        slider.value = slider.value - damage;
        StartCoroutine("Hit2");
        
        if (slider.value <= 0)
        {
            roundScript.currentKillCount++;
            Instantiate(explosionPrefab, new Vector3((gameObject.transform.position).x, (gameObject.transform.position).y, (gameObject.transform.position).z), Quaternion.identity);
            gameObject.SetActive(false);
          // DESTROY GAME OBJECTS LATER: prevents null error when bullet collides with object
        }
        }
    }
    IEnumerator Hit2()
    {
        //We cant be hit
        canBeHit = false;

        //Set material color to red
        playerSpriteRenderer.color = Color.red;

        //Wait 0.1 second
        yield return new WaitForSeconds(0.1f);

        //Set material color to white
        playerSpriteRenderer.color = Color.white;

        //We can be hit
        canBeHit = true;
    }
}
