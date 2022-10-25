using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public GroundCheck groundCheck;
    // Input keys
    [SerializeField]
    KeyCode leftKey, rightKey, jumpKey;
    // Movement values
    [SerializeField]
    float moveSpeed = 2.5f, jumpSpeed = 5f;
    [SerializeField]
    float gravityScale = Physics.gravity.magnitude;
    Vector3 gravityDirection = Vector3.down;

    Vector2 movement = Vector2.zero;
    LayerMask geometryLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider>();
        Rigidbody = GetComponent<Rigidbody>();
        gravityScale = Physics.gravity.magnitude;
        geometryLayerMask = LayerMask.GetMask("Geometry");
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal movement
        movement.x = 0f;
        if (Input.GetKey(leftKey)) { movement.x -= moveSpeed; }
        if (Input.GetKey(rightKey)) { movement.x += moveSpeed; }

        // Get vertical movement
        if (groundCheck.IsGrounded && Input.GetKeyDown(jumpKey))
        {
            movement.y = -gravityDirection.y * jumpSpeed;
        }
        else
        {
            movement.y = Rigidbody.velocity.y;
        }
    }

    private void FixedUpdate()
    {
        // Flip gravity if there is no floor "beneath" the player.
        bool flipGravity = true;
        if (Physics.Raycast(
            transform.position,
            gravityDirection,
            out RaycastHit hit,
            Mathf.Infinity,
            geometryLayerMask,
            QueryTriggerInteraction.Collide
        ) && hit.collider.CompareTag("Floor"))
        {
            flipGravity = false;
        }
        if (flipGravity)
        {
            // Flip the player and their gravity
            gravityDirection *= -1f;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            // Ensure that the player has enough vertical momentum after a flip to escape a new flip
            if (-movement.y / gravityDirection.y < jumpSpeed)
            {
                movement.y = -gravityDirection.y * jumpSpeed;
            }
        }
        // Apply gravity manually
        Rigidbody.AddForce(gravityScale * gravityDirection, ForceMode.Acceleration);

        // Apply movement
        Rigidbody.velocity = movement;
    }
}
