using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D bulletRB;
    
    // Start is called before the first frame update
    void Start()
    {
         bulletRB = GetComponent<Rigidbody2D>();
         bulletRB.velocity = (transform.up) * 222f * Time.deltaTime;//Vector2.up;
         Destroy(gameObject, 3f);
    }

}
