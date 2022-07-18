using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyBullet : MonoBehaviour
{
    public float speed = 5f;
    private float frequency = 15f;
    private float magnitude = 0.5f;
    private Vector3 axis;
    private Vector3 pos;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        axis = transform.up;
        pos = transform.position;
    }

    void Update()
    {
        pos += -transform.right * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        //transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().TakeSpikeDamage();
            Destroy(this.gameObject);
        }

        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}