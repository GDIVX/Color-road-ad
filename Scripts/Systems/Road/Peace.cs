using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peace : MonoBehaviour
{
    public Transform connectedTo;
    public float distanceToMaintain;

    public int index;

    void Awake()
    {
        index = transform.GetSiblingIndex();
    }

    void Update()
    {
        if(connectedTo == null) return;

        transform.LookAt(connectedTo);
        Vector3 direction = (transform.position - connectedTo.position).normalized;
        transform.position = direction * distanceToMaintain + connectedTo.position;
    }
}
