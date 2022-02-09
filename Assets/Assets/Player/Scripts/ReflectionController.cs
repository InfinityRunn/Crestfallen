using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController: MonoBehaviour
{
    static public GameObject currentPlayer;
    [SerializeField] private GameObject reflection;
    private GameObject swap;
    static public GameObject halo;

    //Reflection Stats
    private LayerMask groundLayer;
    private float movementAcceleration;
    private float maxMoveSpeed;
    private float groundLinearDrag;
    private float jumpForce;
    private float airLinearDrag;
    private float fallMultiplier;
    private float lowJumpFallMultiplier;
    private int extraJumps;
    private float hangTime;
    private float jumpBufferLength;
    private float groundRaycastLength;
    private Vector3 groundRaycastOffset;
    private float topRaycastLength;
    private Vector3 edgeRaycastOffset;
    private Vector3 innerRaycastOffset;

    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        halo = GameObject.FindGameObjectWithTag("Halo");
        halo.GetComponent<HaloAnim>();
        StorePlayerStats();
        ApplyReflectionStats();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StorePlayerStats();
            halo.GetComponent<HaloAnim>().xHaloOffset = -(halo.GetComponent<HaloAnim>().xHaloOffset);
            currentPlayer.tag = "Mirror";
            reflection.tag = "Player";
            swap = reflection;
            reflection = currentPlayer;
            currentPlayer = swap;
            currentPlayer.GetComponent<PlayerMovement>().enabled = true;
            currentPlayer.GetComponent<ReflectionMovement>().enabled = false;
            reflection.GetComponent<PlayerMovement>().enabled = false;
            reflection.GetComponent<ReflectionMovement>().enabled = true;
            ApplyPlayerStats();
            ApplyReflectionStats();
        }
    }

    void StorePlayerStats()
    {
        groundLayer = currentPlayer.GetComponent<PlayerMovement>().groundLayer;
        movementAcceleration = currentPlayer.GetComponent<PlayerMovement>().movementAcceleration;
        maxMoveSpeed = currentPlayer.GetComponent<PlayerMovement>().maxMoveSpeed;
        groundLinearDrag = currentPlayer.GetComponent<PlayerMovement>().groundLinearDrag;
        jumpForce = currentPlayer.GetComponent<PlayerMovement>().jumpForce;
        airLinearDrag = currentPlayer.GetComponent<PlayerMovement>().airLinearDrag;
        fallMultiplier = currentPlayer.GetComponent<PlayerMovement>().fallMultiplier;
        lowJumpFallMultiplier = currentPlayer.GetComponent<PlayerMovement>().lowJumpFallMultiplier;
        extraJumps = currentPlayer.GetComponent<PlayerMovement>().extraJumps;
        hangTime = currentPlayer.GetComponent<PlayerMovement>().hangTime;
        jumpBufferLength = currentPlayer.GetComponent<PlayerMovement>().jumpBufferLength;
        groundRaycastLength = currentPlayer.GetComponent<PlayerMovement>().groundRaycastLength;
        groundRaycastOffset = currentPlayer.GetComponent<PlayerMovement>().groundRaycastOffset;
        topRaycastLength = currentPlayer.GetComponent<PlayerMovement>().topRaycastLength;
        edgeRaycastOffset = currentPlayer.GetComponent<PlayerMovement>().edgeRaycastOffset;
        innerRaycastOffset = currentPlayer.GetComponent<PlayerMovement>().innerRaycastOffset;
    }

    void ApplyPlayerStats()
    {
        currentPlayer.GetComponent<PlayerMovement>().groundLayer = groundLayer;
        currentPlayer.GetComponent<PlayerMovement>().movementAcceleration = movementAcceleration;
        currentPlayer.GetComponent<PlayerMovement>().maxMoveSpeed = maxMoveSpeed;
        currentPlayer.GetComponent<PlayerMovement>().groundLinearDrag = groundLinearDrag;
        currentPlayer.GetComponent<PlayerMovement>().jumpForce = jumpForce;
        currentPlayer.GetComponent<PlayerMovement>().airLinearDrag = airLinearDrag;
        currentPlayer.GetComponent<PlayerMovement>().fallMultiplier = fallMultiplier;
        currentPlayer.GetComponent<PlayerMovement>().lowJumpFallMultiplier = lowJumpFallMultiplier;
        currentPlayer.GetComponent<PlayerMovement>().extraJumps = extraJumps;
        currentPlayer.GetComponent<PlayerMovement>().hangTime = hangTime;
        currentPlayer.GetComponent<PlayerMovement>().jumpBufferLength = jumpBufferLength;
        currentPlayer.GetComponent<PlayerMovement>().groundRaycastLength = groundRaycastLength;
        currentPlayer.GetComponent<PlayerMovement>().groundRaycastOffset = groundRaycastOffset;
        currentPlayer.GetComponent<PlayerMovement>().topRaycastLength = topRaycastLength;
        currentPlayer.GetComponent<PlayerMovement>().edgeRaycastOffset = edgeRaycastOffset;
        currentPlayer.GetComponent<PlayerMovement>().innerRaycastOffset = innerRaycastOffset;
    }

    void ApplyReflectionStats()
    {
        reflection.GetComponent<ReflectionMovement>().groundLayer = groundLayer;
        reflection.GetComponent<ReflectionMovement>().movementAcceleration = movementAcceleration;
        reflection.GetComponent<ReflectionMovement>().maxMoveSpeed = maxMoveSpeed;
        reflection.GetComponent<ReflectionMovement>().groundLinearDrag = groundLinearDrag;
        reflection.GetComponent<ReflectionMovement>().jumpForce = jumpForce;
        reflection.GetComponent<ReflectionMovement>().airLinearDrag = airLinearDrag;
        reflection.GetComponent<ReflectionMovement>().fallMultiplier = fallMultiplier;
        reflection.GetComponent<ReflectionMovement>().lowJumpFallMultiplier = lowJumpFallMultiplier;
        reflection.GetComponent<ReflectionMovement>().extraJumps = extraJumps;
        reflection.GetComponent<ReflectionMovement>().hangTime = hangTime;
        reflection.GetComponent<ReflectionMovement>().jumpBufferLength = jumpBufferLength;
        reflection.GetComponent<ReflectionMovement>().groundRaycastLength = groundRaycastLength;
        reflection.GetComponent<ReflectionMovement>().groundRaycastOffset = groundRaycastOffset;
        reflection.GetComponent<ReflectionMovement>().topRaycastLength = topRaycastLength;
        reflection.GetComponent<ReflectionMovement>().edgeRaycastOffset = edgeRaycastOffset;
        reflection.GetComponent<ReflectionMovement>().innerRaycastOffset = innerRaycastOffset;
    }
}
