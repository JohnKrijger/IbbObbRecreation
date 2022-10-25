using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    // Input keys
    [SerializeField]
    KeyCode leftKey, rightKey, jumpKey;
    // Movement values
    [SerializeField]
    float moveSpeed = 2.5f, jumpSpeed = 5f;

    Vector2 movement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal movement
        movement.x = 0f;
        if (Input.GetKey(leftKey)) { movement.x -= moveSpeed; }
        if (Input.GetKey(rightKey)) { movement.x += moveSpeed; }

        // Get vertical movement
        movement.y = Input.GetKeyDown(jumpKey) ? jumpSpeed : Rigidbody.velocity.y;
    }

    private void FixedUpdate()
    {
        // Apply movement
        Rigidbody.velocity = movement;
    }
}
