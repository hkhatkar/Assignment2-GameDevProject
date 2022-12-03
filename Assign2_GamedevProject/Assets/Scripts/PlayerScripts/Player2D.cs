using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2D : MonoBehaviour {

	public Transform[] bulletSpawnPos;	//Bullet spawn position
	public GameObject bullet;			//Bullet prefab
	public GameObject explosion;		//Explosion prefab
	private int hp = 3;					//Health
	private int score;					//Score
	private float tmpFireTime;			//Tmp fire time
	private bool dead;					//Are we dead
	private Vector3 pos;				//Position
	private bool canBeHit = true;		//Can we be hit
	public Canvas GOCanvas;
	[SerializeField] private GameObject blockedParticle;
	UseShield shieldScript;


	void Start ()
	{
	
		//Set screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		shieldScript = gameObject.GetComponent<UseShield>();
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
		//If the game is running on a android device
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			//Get screen position
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
		}
		//If the game is not running on an iphone device
		else
		{
			//Get screen position
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
		}
		
		transform.position = new Vector3(pos.x,pos.y + 0.2f,pos.z);
	}
	
	void ShotUpdate()
	{
		//If tmpFireTime is bigger than 0.2
		if (tmpFireTime > 0.2f)
		{
			//If the game is running on an iphone device
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//If finger is on screen
				if (Input.GetTouch(0).tapCount != 0)
				{
					//Go through all the bullet spawn position
					for (int i = 0; i < bulletSpawnPos.Length; i++)
					{
						//Instantiate bullet
						Instantiate(bullet,bulletSpawnPos[i].position,Quaternion.identity);
						//Set tmpFireTime to 0
						tmpFireTime = 0;
					}
				}
				else
				{
					//Set tmpFireTime to 0.2
					tmpFireTime = 0.2f;
				}
			}
			//If the game is not running on an iphone device
			else
			{
				//If get left mouse button down
				if (Input.GetMouseButton(0))
				{
					//Go through all the bullet spawn position
					for (int i = 0; i < bulletSpawnPos.Length; i++)
					{
						//Instantiate bullet
						Instantiate(bullet,bulletSpawnPos[i].position,Quaternion.identity);
						//Set tmpFireTime to 0
						tmpFireTime = 0;
					}
				}
				else
				{
					//Set tmpFireTime to 0.2
					tmpFireTime = 0.2f;
				}
			}
		}
		//If tmpFireTime is less than 0.2
		else
		{
			//Add 1 to tmpFireTime
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
		if (shieldScript.shieldUp == true)
		{
			if (other.tag == "enemyBullet")
			{
				other.gameObject.SetActive(false);
				Instantiate(blockedParticle,transform.position,Quaternion.identity);
			}
		}
		else if (other.tag == "Enemy" || other.tag == "enemyBullet")
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
