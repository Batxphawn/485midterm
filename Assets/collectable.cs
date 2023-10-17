using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    public float rotationSpeed = 3f;   // Collectable rotation speed
    Vector3 collectablePos;

    private GameObject Player;

    void Start()
    {
        collectablePos = transform.position;
        Player = GameObject.Find("Player");
        //  CollectableManager = GameObject.Find("CollectableManager");
    }

    void Update()
    {
        // Rotate the object in place
        transform.Rotate(0f, rotationSpeed, 0.5f);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.gameObject.SetActive(false);
            scoreKeeper.instance.colScore();
        }
    }
    public void notActive()
    {
        transform.gameObject.SetActive(false);
    }
    public void isActive()
    {
        transform.gameObject.SetActive(true);
    }
}
