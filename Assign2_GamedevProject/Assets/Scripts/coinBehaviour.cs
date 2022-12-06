using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehaviour : MonoBehaviour
{
    private GameObject rm;
    private float expireTime;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("roundManager");
        expireTime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        expireTime -= Time.deltaTime;

        if(expireTime <= 0.0f) {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject);
        if (col.gameObject.tag == "Player")
        {
            rm.GetComponent<RoundManager>().incrementCoin();
            Destroy(gameObject);
        } 
    }
}
