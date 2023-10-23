using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleFSM : FSM 
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Stare,
        Manic,
        Dead,
    }

    //Current state that the NPC is reaching
    public FSMState curState;

    //Speed of the tank
    private float curSpeed;

    //Tank Rotation Speed
    private float curRotSpeed;

    //Whether the NPC is destroyed or not
    private bool bDead;
    private float health;
    private float maxHealth;
    private Text healText;
	private Image healBar;
    private Rigidbody rbody;
    public GameObject explostion;
    public float jumpHeight = 3f;
    public Material angry;
    public gameOverButton gb;


    //Initialize the Finite state machine for the NPC tank
	protected override void Initialize () 
    {
        curState = FSMState.Patrol;
        curSpeed = 7.0f;
        curRotSpeed = 2.0f;
        bDead = false;
        elapsedTime = 0.0f;
        shootRate = 3.0f;
        health = 50f;
        maxHealth = health;

        healText = transform.Find("EnemyCanvas").Find("HealthBarText").GetComponent<Text>();
		healBar = transform.Find("EnemyCanvas").Find("MaxHealthBar").Find("HealthBar").GetComponent<Image>();
        rbody = GetComponent<Rigidbody>();

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        //Set Random destination point first
        FindNextPoint();

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
	}

    //Update each frame
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Stare: UpdateStareState(); break;
            case FSMState.Manic: UpdateManicState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
        }

        //Update the time
        elapsedTime += Time.deltaTime;

        //Go to dead state is no health left
        if (health <= 0)
            curState = FSMState.Dead;
    }

    /// <summary>
    /// Patrol state
    /// </summary>
    protected void UpdatePatrolState()
    {
        UpdateHealth();
        
        float dist = Vector3.Distance(destPos, playerTransform.position);
        //Find another random patrol point if the current point is reached
        if (dist <= 10.0f)
        {
            print("Reached to the destination point\ncalculating the next point");
            FindNextPoint();
        }
        //Check the distance with player tank
        //When the distance is near, transition to Stare state
        else if (dist <= 30.0f)
        {
            print("Switch to Stare Position");
            curState = FSMState.Stare;
        }

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);  

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    /// <summary>
    /// Stare state
    /// </summary>
    protected void UpdateStareState()
    {
        UpdateHealth();

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);


        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= 20.0f)
        {
            print("Switch to Chase Position");
            curState = FSMState.Chase;
        } 
        else if (Vector3.Distance(transform.position, playerTransform.position) > 30.0f)
        {
            print("Switch to Patrol Position");
            curState = FSMState.Patrol;
        }
    }

    /// <summary>
    /// Chase state
    /// </summary>
    protected void UpdateChaseState()
    {
        UpdateHealth();

        //Set the target position as the player position
        destPos = playerTransform.position;

        float dist = Vector3.Distance(transform.position, playerTransform.position);

        //Go back to patrol is it become too far
        if (dist >= 35.0f)
        {
            curState = FSMState.Patrol;
        }

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);  

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }
    /// <summary>
    /// Manic state
    /// </summary>
    protected void UpdateManicState()
    {
        UpdateHealth();

        //Set the target position as the player position
        destPos = playerTransform.position;

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);  

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    /// <summary>
    /// Dead state
    /// </summary>
    protected void UpdateDeadState()
    {
        //Show the dead animation with some physics effects
        if (!bDead)
        {
            bDead = true;
            Explode();
        }
    }

    /// <summary>
    /// Check the collision with the bullet
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision col)
    {
        //Reduce health
		if(col.gameObject.tag == "Bullet")
        {
			health -= 10;
		}
        if (col.gameObject.tag == "Player")
		{
            scoreKeeper.instance.hit();
            Instantiate(explostion, transform.position, transform.rotation);

			gb.ShowButton();
			Destroy(this);
        }
    } 

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "object")
        {
		    rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }  

    /// <summary>
    /// Find the next semi-random patrol point
    /// </summary>
    protected void FindNextPoint()
    {
        print("Finding next point");
        int rndIndex = Random.Range(0, pointList.Length);
        float rndRadius = 10.0f;
        
        Vector3 rndPosition = Vector3.zero;
        destPos = pointList[rndIndex].transform.position + rndPosition;

        //Check Range
        //Prevent to decide the random point as the same as before
        if (IsInCurrentRange(destPos))
        {
            rndPosition = new Vector3(Random.Range(-rndRadius, rndRadius), 0.0f, Random.Range(-rndRadius, rndRadius));
            destPos = pointList[rndIndex].transform.position + rndPosition;
        }
    }

    /// <summary>
    /// Check whether the next random position is the same as current tank position
    /// </summary>
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }

    protected void Explode()
    {
        scoreKeeper.instance.enemyDown();

        scoreKeeper.instance.EnemyList.Remove(transform.gameObject);

        float rndX = Random.Range(10.0f, 30.0f);
        float rndZ = Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }

        Destroy(gameObject, 1.5f);
    }
    private void UpdateHealth()
    {
        healText.text = health.ToString();
		healBar.fillAmount = health / maxHealth;
    }
    public void SwapToManic()
    {
        print("Entering Manic!!!");
        curState = FSMState.Manic;

        curSpeed = 10f;
        GetComponent<Renderer>().material = angry;
    }

}