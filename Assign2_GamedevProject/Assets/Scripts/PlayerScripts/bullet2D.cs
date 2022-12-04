using UnityEngine;
using System.Collections;

public class bullet2D : MonoBehaviour {

    public float speed = 1;            //Move speed
    public float destroyTime = 5;    //Time it takes to destroy
    public int damage = 20;        //Damage
    
    void Start ()
    {
        //Start DestroyGo
        StartCoroutine("DestroyGo");
    }
    
    void Update ()
    {
        //Move bullet
        transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {


        //If we are in a enemy trigger
        if (other.gameObject.tag == "Enemy")
        {
            
            other.GetComponent<enemyHealthManager>().takeDamage(0.5f);
            //Destroy
            Destroy(gameObject);
            
        }
    }
    
    IEnumerator DestroyGo()
    {
        //Wait
        yield return new WaitForSeconds(destroyTime);
        //Destroy
        Destroy(gameObject);
    }
}
