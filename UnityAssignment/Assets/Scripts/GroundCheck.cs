using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class count the number of ground colliders it currently overlaps with,
// if this is zero, the character is not grounded.
[RequireComponent(typeof(Collider))]
public class GroundCheck : MonoBehaviour
{
    private int groundCount = 0;

    public bool IsGrounded => groundCount > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Player"))
        {
            groundCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Player"))
        {
            groundCount--;
        }
    }
}
