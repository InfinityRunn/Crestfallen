using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Layer Masks")]
    public LayerMask groundLayer;
    [SerializeField] private LayerMask spikeLayer;

    [Header("Movement Variables")]
    public float movementAcceleration;
    public float maxMoveSpeed;
    public float groundLinearDrag;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump Variables")]
    public float jumpForce = 12f;
    public float airLinearDrag = 2.5f;
    public float fallMultiplier = 8f;
    public float lowJumpFallMultiplier = 5f;
    public int extraJumps = 1;
    public float hangTime = .1f; //Allows for slightly late jumping "Coyote time"
    public float jumpBufferLength = .1f; //Allows for quicker more responsive jumping
    private int extraJumpsValue;
    private float hangTimeCounter;
    private float jumpBufferCounter;
    private bool canJump => jumpBufferCounter > 0f && (hangTimeCounter > 0f || extraJumpsValue > 0);

    [Header("Inventory")]
    [SerializeField] public int keyCount = 0;

    [Header("Ground Collision Variables")]
    public float groundRaycastLength;
    public Vector3 groundRaycastOffset;
    private bool onGround;

    [Header("Corner Correction Variables")]
    public float topRaycastLength;
    public Vector3 edgeRaycastOffset;
    public Vector3 innerRaycastOffset;
    private bool canCornerCorrect;

    [Header("Health Variables")] //Covers the variables pertaining to health and invincibility frames.
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private int invincibilitySeconds;
    public Healthbar healthbar; //References the Healthbar script

    private bool invincible = false;

    [Header("Spike Variables")] //influences damage of all spikes.
    [SerializeField] public int spikeDamagePerSecond = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        horizontalDirection = GetInput().x;

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferLength;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        MoveCharacter();
        
        if (onGround)
        {
            ApplyGroundLinearDrag();
            extraJumpsValue = extraJumps;
            hangTimeCounter = hangTime;
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            hangTimeCounter -= Time.fixedDeltaTime;
        }

        if (canJump)
            Jump();

        if (canCornerCorrect)
            CornerCorrect(rb.velocity.y);
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
            rb.drag = airLinearDrag;
    }

    private void Jump()
    {
        if (!onGround)
            --extraJumpsValue;

        ApplyAirLinearDrag();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        hangTimeCounter = 0f;
        jumpBufferCounter = 0f;
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void CornerCorrect(float Yvelocity)
    {
        //Push player to the right
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.left, topRaycastLength, groundLayer);
        if (hit.collider != null && !onGround)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength,
                transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
            return;
        }

        //Push player to the left
        hit = Physics2D.Raycast(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.right, topRaycastLength, groundLayer);
        if (hit.collider != null && !onGround)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength,
                transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
            return;
        }

    }

    private void CheckCollisions()

    {
        //Ground Collisions
        onGround = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) || 
            Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);

        //Corner Collisions
        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, groundLayer) &&
                           !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer) ||
                           Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, groundLayer) &&
                           !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, groundLayer);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Ground Check
        Gizmos.DrawLine(transform.position + groundRaycastOffset, transform.position + groundRaycastOffset + Vector3.down * groundRaycastLength);
        Gizmos.DrawLine(transform.position - groundRaycastOffset, transform.position - groundRaycastOffset + Vector3.down * groundRaycastLength);

        //Corner Check
        Gizmos.DrawLine(transform.position + edgeRaycastOffset, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - edgeRaycastOffset, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastOffset, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength);

        //Corner Distance check
        Gizmos.DrawLine(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength,
                        transform.position - innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength,
                        transform.position + innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.right * topRaycastLength);
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Spikes") && !invincible)
        { 
            TakeSpikeDamage();
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilitySeconds);
        invincible = false;
    }

    public void TakeSpikeDamage()
    {
        currentHealth -= spikeDamagePerSecond;
        healthbar.SetHealth(currentHealth);
    } //YOU ARE NOT COUNTED AS GROUNDED WHILE ON SPIKES. SO LOW FRICTION AND NO EXTRA JUMPS.
}
