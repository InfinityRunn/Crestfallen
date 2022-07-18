using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnFacePlayer : MonoBehaviour
{
    private GameObject playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //transform.rotation.LookAt(playerPos.transform.position);
    }
}
