using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public float recordTime = 3f;

    public bool isRewinding = false;

    List<Vector3> positions;

    Rigidbody2D rb;

    private void Start()
    {
        positions = new List<Vector3>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) StartRewind();

        //if (Input.GetKeyUp(KeyCode.Return)) StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else Record();
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

    void Record()
    {
        if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, transform.position);
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else StopRewind();
    }
}
