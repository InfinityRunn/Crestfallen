using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakSpot : MonoBehaviour
{
    private Gluttony_Controller GC;
    
    private Vector3 startPos;

    public float speed = 1;
    public float xScale = 1;
    public float yScale = 1;

    void Start()
    {
        startPos = transform.position;
        GC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Gluttony_Controller>();
    }

    void Update()
    {
        transform.position = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GC.phase++;
            GC.attackAmount = 0;
            Object.Destroy(this.gameObject);
        }
    }
}
