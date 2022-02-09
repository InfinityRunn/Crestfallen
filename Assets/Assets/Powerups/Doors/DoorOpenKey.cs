using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenKey : MonoBehaviour
{
    private bool hasKey = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount > 0)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount--;
                Debug.Log("Current Keys: " + GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount);
                Destroy(this.gameObject);
            }
            else
            Debug.Log("The door is locked and you have no keys");
        }
    }
}
