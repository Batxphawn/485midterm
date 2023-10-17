using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public UnityEngine.Events.UnityEvent begin;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    } 
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player"))
        {
            begin.Invoke();
            audio.Play();
        }
    }  
}
