using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{   
    public static Action<Vector2> OnPointerReleaseListener;
    public static Action<Vector2> OnPointerHoldListener;
    public static Action<Vector2> OnPointerClickListener;

    bool isPlaying = false;

    void Awake()
    {
        GameManager.gameStart += ()=> {isPlaying = true;};
        GameManager.gameOverListener += () => {isPlaying = false;};
    }


    void Update()
    {
        if(!isPlaying) return;
        //touch controls
        if(Input.touchSupported){
            if(Input.touchCount > 0)
                HandleTouchControls();
            return;
        }
            HandleMouseControls();
    }

    private void HandleMouseControls()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray , out RaycastHit hit)){
            Vector2 mousePosition = hit.point; 

            mousePosition.x = Mathf.Clamp(mousePosition.x  , -1.6f , 1.6f);

            if(Input.GetMouseButtonDown(0)){
                OnPointerClickListener?.Invoke(mousePosition);
                //return;
            }

            if(Input.GetMouseButtonUp(0)){
                OnPointerReleaseListener?.Invoke(mousePosition);
                //return;
            }

            if(Input.GetMouseButton(0)){
                OnPointerHoldListener?.Invoke(mousePosition);
            }
        }
        
    }

    private void HandleTouchControls()
    {
        Touch touch = Input.GetTouch(0);

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        switch(touch.phase){
            case TouchPhase.Began:{
                OnPointerClickListener?.Invoke(touchPosition);
                return;
            }
            case TouchPhase.Ended:{
                OnPointerReleaseListener?.Invoke(touchPosition);
                return;
            }
            case TouchPhase.Moved:{
                OnPointerHoldListener?.Invoke(touchPosition);
                return;
            }
        }
    }
}
