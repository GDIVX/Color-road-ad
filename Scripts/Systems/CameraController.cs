using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float speed;
    

    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.x = Mathf.Lerp(transform.position.x , player.position.x , interpolation);
        position.x = Mathf.Clamp(position.x , -2.3f , 2.3f);

        transform.position = position;
    }
}
