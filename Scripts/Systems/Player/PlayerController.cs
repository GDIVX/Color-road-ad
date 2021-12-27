using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Systems.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float minX , maxX;
        public float speed;
        Rigidbody rigidbody;



        void Awake()
        {
            PlayerInput.OnPointerHoldListener += OnPointerHold;
            GameManager.gameStart += () => {transform.position = new Vector3(0, .8f , 3);};

            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnPointerHold(Vector2 pointerPosition)
        {

            Vector3 currentPosition = transform.position;
            float step = speed * Time.deltaTime;
            Vector3 target = new Vector3(pointerPosition.x , currentPosition.y , currentPosition.z);
            transform.position = Vector3.MoveTowards(currentPosition , target , step);
        }

    }
}