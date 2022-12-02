using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]int rotationDir;
    [SerializeField]private List<steeringBehaviour> steeringBehaviours;

    [SerializeField]private List<Detector> detectors;

    [SerializeField]private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 0f; //aiUpdateDelay runs ai less often, for performace purpouses

    [SerializeField]
    private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    public Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    public bool following = false;
    //public as used in aim/shoot script to switch between rotation types

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        //Enemy AI movement based on Target availability
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        //Moving the Agent
        OnMovementInput?.Invoke(movementInput);
        if (following == true){
        transform.position += new Vector3(movementInput.x * Time.deltaTime, movementInput.y * Time.deltaTime, 0* Time.deltaTime);
        
        }
        else if (following == false) {enemyDetermineAction();}
    }

    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            
            //Stopping Logic
            
            Debug.Log("Stopping");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < 2f)
            {
                //circle logic
                movementInput = Vector2.zero;
                following = false;
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }

    }

    void enemyDetermineAction()
    {
        if (aiData.currentTarget != null)
        {
            if (Vector2.Distance(gameObject.transform.position, aiData.currentTarget.position) < 5f )
            {
          
          
                transform.RotateAround(aiData.currentTarget.position, new Vector3(0f,0f,1f), rotationDir * Time.deltaTime);
                transform.RotateAround(transform.position, new Vector3(0f,0f,-1f), rotationDir * Time.deltaTime);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject);
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Obstacle" )
        {//enemy is stunned if it collides with player
          rotationDir = -rotationDir;
        } 
    }
}