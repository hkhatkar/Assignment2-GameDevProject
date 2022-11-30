using UnityEngine;
using System.Collections;

public class Spawn2D : MonoBehaviour {

    public float spawnSpeed = 1.2f;    //Spawn speed
    public GameObject[] enemys;    //Enemy prefabs

    void Start()
    {
        //Start Spawn
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        //Wait
        yield return new WaitForSeconds(spawnSpeed);

        //Spawn enemy
        GameObject instEnemy = Instantiate(enemys[Random.Range(0,enemys.Length)],new Vector3(Random.Range(-10,11) * 0.2f,transform.position.y,1), Quaternion.identity);
        instEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);



        //Start Spawn
        StartCoroutine("Spawn");
    }
}