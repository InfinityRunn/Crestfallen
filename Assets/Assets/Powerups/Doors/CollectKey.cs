using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount += 1;
            Debug.Log("Keys Collected: " + GameObject.Find("Player").GetComponent<PlayerMovement>().keyCount);
            Destroy(this.gameObject);
        }
    }
}
