using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chasing : MonoBehaviour {

	public Transform target;
	public float speed;
    public float jumpHeight = 3f;
    private Rigidbody rbody;

    private float health;
	private float maxHealth;
	public GameObject explostion;

	private Text healText;
	private Image healBar;
	public gameOverButton gameOverButton;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
        speed = 2.5f;
		health = 100.0f;
		maxHealth = 100.0f;
		healText = transform.Find("EnemyCanvas").Find("HealthBarText").GetComponent<Text>();
		healBar = transform.Find("EnemyCanvas").Find("MaxHealthBar").Find("HealthBar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target, Vector3.up);
		transform.position += transform.forward * speed * Time.deltaTime;
		healText.text = health.ToString();
		healBar.fillAmount = health / maxHealth;
		if (scoreKeeper.instance.defeat() == true)
		{
            Destroy(this);
            Instantiate(explostion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

	}

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Bullet") {
			health -= 10;
			if(health < 1) {
				Destroy(this);
				Instantiate(explostion, transform.position, transform.rotation);
				Destroy(gameObject);
                scoreKeeper.instance.enemyDown();
            }
		}
        if (col.gameObject.tag == "Player")
		{
            scoreKeeper.instance.hit();
            Instantiate(explostion, transform.position, transform.rotation);

			gameOverButton.ShowButton();
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
    public void notActive()
    {
        transform.gameObject.SetActive(false);
    }
    public void isActive()
    {
		if (scoreKeeper.instance.firstCol() == true)
		{
			transform.gameObject.SetActive(true);
		}
    }
}





