using UnityEngine;
using System.Collections;

public class Spawn2D : MonoBehaviour {

    //public float spawnSpeed = 1.2f;    //Spawn speed
    public GameObject[] enemys;    //Enemy prefabs

    void Start()
    {
        //Start Spawn
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        //spawn rate
        yield return new WaitForSeconds(Random.Range(8, 15));

        //Spawn enemy
        
        GameObject instEnemy = Instantiate(enemys[Random.Range(0,enemys.Length)],
        new Vector3(transform.position.x + Random.Range(-3,3) * 0.2f,Random.Range(-3,3) * 0.2f+ transform.position.y ,1), Quaternion.identity);
        //spawns random enemy
        //spawn in local area around spawner

        instEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);



        //Start Spawn
        StartCoroutine("Spawn");
    }
}