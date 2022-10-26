using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Movement constraints
    [SerializeField]
    private Collider attachedFloor;
    [SerializeField]
    private float distanceFromEdges = 1f;
    [SerializeField]
    private float movementSpeed = 1.5f;
    [SerializeField]
    private int movementDirection = 1;

    // Trigger of the spikey part of the enemy, this hurts the player
    [SerializeField]
    private Trigger spikeyTrigger;
    // Trigger of the spirit part of the enemy, touching this kills the enemy
    [SerializeField]
    private Trigger spiritTrigger;

    void Start()
    {
        spikeyTrigger.TriggerEnter += OnSpikeyTriggerEnter;
        spiritTrigger.TriggerEnter += OnSpiritTriggerEnter;
    }

    void FixedUpdate()
    {
        // Revert direction if enemy gets too close to the edges of its platform
        if (transform.position.x * movementDirection + distanceFromEdges >=
            attachedFloor.bounds.center.x * movementDirection + attachedFloor.bounds.extents.x)
        {
            movementDirection *= -1;
        }
        transform.localPosition += movementDirection * movementSpeed * Time.deltaTime * Vector3.right;
    }

    void OnSpikeyTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Respawn();
        }
    }

    void OnSpiritTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
