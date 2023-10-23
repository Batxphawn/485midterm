using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    #region Attributes

    public Transform door;

    public Vector3 closedPosition = new Vector3(-7.27f, 0f, 81.4f);
    public Vector3 openedPosition = new Vector3(-7.27f, 5f, 81.4f);

    public float openSpeed = 5;

    private bool open = false;

    #endregion

    #region MonoBehaviour API

    private void Update()
    {
        if (open)
        {
            door.position = Vector3.Lerp(door.position,
                openedPosition, Time.deltaTime * openSpeed);
            Debug.Log("PLAYER WENT THROUGH THE DOOR");
        }
        else
        {
            door.position = Vector3.Lerp(door.position,
                closedPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CloseDoor();
        }
    }

    #endregion

    public void CloseDoor()
    {
        open = false;
    }

    public void OpenDoor()
    {
        open = true;
    }
}
