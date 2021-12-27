using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class ColorCollision : MonoBehaviour
{
    public enum color {Red, Blue , Yellow}
    public color initialColor;
    public color MyColor{get{return _MyColor;} set{ SetColor(value);}}
    public Material red, blue , yellow;
    public bool checkCollision = false;
    public bool isRamp = false;
    public static Action<ColorCollision , ColorCollision> OnColorCollision;

    public MMFeedbacks jumpFeedback;
    public MMFeedbacks explodeFeedback;

    public static ColorCollision player;

    public static Action onKillListener;


    color _MyColor;
    MeshRenderer meshRenderer;


    void Awake()
    {
        if(checkCollision) player = this;
        
        meshRenderer = GetComponent<MeshRenderer>();
        GameManager.gameStart += () => {
            gameObject.SetActive(true);
            MyColor = initialColor;
            };
    }


    void OnTriggerEnter(Collider other)
    {
        if(!checkCollision) return;

        if(other.gameObject.TryGetComponent<ColorCollision>( out ColorCollision otherColor)){
            OnColorCollision?.Invoke(this , otherColor);
            return;
        }
        
        
    }
    internal void HandleRampInteraction(color color)
    {
        MyColor = color;
        jumpFeedback?.PlayFeedbacks();
    }

    public void Kill()
    {
        explodeFeedback?.PlayFeedbacks();
        onKillListener?.Invoke();
    }

    private void SetColor(color value)
    {
        _MyColor = value;
        switch (value)
        {
            case color.Blue:
                meshRenderer.material = blue;
                break;
            case color.Red:
                meshRenderer.material = red;
                break;
            case color.Yellow:
                meshRenderer.material = yellow;
                break;
        }
        
    }
}
