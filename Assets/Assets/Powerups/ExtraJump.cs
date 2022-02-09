using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraJump : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().extraJumps += 1;
            Destroy(this.gameObject);
        }
    }
}
