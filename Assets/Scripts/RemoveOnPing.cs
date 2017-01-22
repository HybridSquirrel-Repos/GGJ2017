using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnPing : MonoBehaviour {

    Collider m_collider;

    void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision coll) {
        Debug.Log("!!!");
        Physics.IgnoreCollision(coll.collider, this.m_collider);
    }
}
