using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
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
            onGUI();
        }
    }
    void onGUI ()
    {
        if (GUI.Button(new Rect(600, 400, 100, 50), "Next Level"))
        {
            Application.LoadLevel(3);
        }
    } 
}
