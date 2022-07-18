using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloAnim : MonoBehaviour
{
    private float speed = 2f;
    private float maxRotation = 3f;
    private GameObject haloTracking;
    public float xHaloOffset = -.12f;
    [SerializeField] private float yHaloOffset = .9f;
    [SerializeField] private float zHaloOffset = -1;
    [SerializeField] private float floatSpeed = 100f;
    Vector3 targetPos;

    private void Start()
    {
        haloTracking = ReflectionController.currentPlayer;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        HaloFollow();
    }

    void HaloFollow()
    {
        Vector3 targetPos = new Vector3(ReflectionController.currentPlayer.transform.position.x + xHaloOffset,
            ReflectionController.currentPlayer.transform.position.y + yHaloOffset,
            ReflectionController.currentPlayer.transform.position.z + zHaloOffset);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, 
            targetPos, Time.deltaTime * floatSpeed);
    }
}
