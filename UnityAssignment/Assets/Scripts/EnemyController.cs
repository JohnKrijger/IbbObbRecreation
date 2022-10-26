using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // The bounds of this object are the bounds of the enemy's movement
    public Collider attachedFloor;

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
