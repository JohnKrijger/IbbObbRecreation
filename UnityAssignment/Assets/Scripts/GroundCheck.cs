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

    private bool isOnOtherPlayer = false;
    private PlayerController otherPlayer;

    public bool IsOnOtherPlayer(out PlayerController otherPlayer)
    {
        otherPlayer = this.otherPlayer;
        return isOnOtherPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Player"))
        {
            groundCount++;
            if (other.CompareTag("Player"))
            {
                isOnOtherPlayer = true;
                otherPlayer = other.GetComponent<PlayerController>();
                otherPlayer.hasOtherPlayerOnHead = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Player"))
        {
            groundCount--;
            if (other.CompareTag("Player"))
            {
                isOnOtherPlayer = false;
                otherPlayer.hasOtherPlayerOnHead = false;
                otherPlayer = null;
            }
        }
    }
}
