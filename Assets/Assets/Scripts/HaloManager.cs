using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//place on each room collider

public class HaloManager : MonoBehaviour
{
    public GameObject CurrentPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
        }
    }
}
