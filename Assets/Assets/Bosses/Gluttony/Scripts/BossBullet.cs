using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private GameObject player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }

    void Update()
    {
        transform.localPosition += -transform.right * Time.deltaTime * speed;
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
