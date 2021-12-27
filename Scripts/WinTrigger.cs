using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ColorCollision>(out ColorCollision collision)){
            if(collision.checkCollision){
                GameManager.gameWonListener?.Invoke();
                collision.Kill();
                GameObject.Destroy(gameObject);
            }
        }
    }
}
