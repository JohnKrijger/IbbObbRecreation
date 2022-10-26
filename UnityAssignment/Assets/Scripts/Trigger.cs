using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component allows custom actions to be invoked on trigger events
[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{

    public Action<Collider> TriggerEnter;
    public Action<Collider> TriggerExit;
    public Action<Collider> TriggerStay;

    void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);
    void OnTriggerExit(Collider other) => TriggerExit?.Invoke(other);
    void OnTriggerStay(Collider other) => TriggerStay?.Invoke(other);
}
