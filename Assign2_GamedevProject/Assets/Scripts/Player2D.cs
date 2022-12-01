using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2D : MonoBehaviour {

	//public Transform[] bulletSpawnPos;
	public GameObject bullet;			//Bullet prefab
	public Transform bulletTransform;

	public GameObject explosion;		//Explosion prefab
	private int hp = 3;					//Health
	private int score;					//Score
	private float tmpFireTime;			//Tmp fire time
	private bool dead;					//Are we dead
	private bool canBeHit = true;		//Can we be hit
	public Canvas GOCanvas;
	private Rigidbody2D rb;

	private Camera cam;					//Camera
	private Vector3 mousePos;			//Position


	void Start ()
	{
	
		//Set screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Grab RB2D comp
		rb = GetComponent<Rigidbody2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	void Update()
	{

		//If we are not dead
		if (!dead)
		{
			//Update
			MoveUpdate();
			ShotUpdate();
		}
	}
	
	void MoveUpdate()
	{
		//On WASD input, inact a force in given direction
        if (Input.GetKey(KeyCode.A)) {
        	rb.AddForce(Vector3.left);
		}
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right);
		}
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.up);
		}
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector3.down);
		}
	}

	void ShotUpdate() {
		//Get mouse position relative to camera
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		//Create V3 for the rotation
		Vector3 rotation = mousePos - transform.position;
		float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

		//Rotate the ship on the new angle (and set one for the bullet trajectory)
		transform.rotation = Quaternion.Euler(0, 0, rotZ);
		Quaternion bulletRot = Quaternion.Euler(0, 0, rotZ - 90);

		//Firerate stuff
		if (tmpFireTime > 0.2f) {
			if (Input.GetMouseButton(0)) {
					Instantiate(bullet, transform.position, bulletRot);
					//Set tmpFireTime to 0
					tmpFireTime = 0;
			}
			else {
				//Set tmpFireTime to 0.2
				tmpFireTime = 0.2f;
			}
		} else {
			tmpFireTime += 1 * Time.deltaTime;
		}


	}
	
	IEnumerator Hit()
	{
		//We cant be hit
		canBeHit = false;
		
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		
		//We can be hit
		canBeHit = true;
	}
	
	public void AddScore(int _score)
	{
		//Add _score to score
		score += _score;
	}

	//return score
	public int DisplayScore(){
		return score;
	}


	//return hp
	public int DisplayHP(){
		return hp;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//If we can be hit
			if (canBeHit)
			{
				//Remove 1 from hp
				hp--;
				
				//If hp is less than 1
				if (hp < 1)
				{
					//enable game over canvas
					GOCanvas.GetComponent<Canvas>().enabled = true;
					
					//We are dead
					dead = true;
					
					
					//Dont show player
					GetComponent<SpriteRenderer>().enabled = false;
					
					//Set collider to false
					GetComponent<Collider2D>().enabled = false;

					


				}
				//If hp is bigger than 0
				else
				{
					//Start Hit
					StartCoroutine("Hit");
				}
				
				//Instantiate explosion
				Instantiate(explosion,transform.position,Quaternion.identity);
				//Destroy
				Destroy(other.gameObject);
			}
		}
	}
}
