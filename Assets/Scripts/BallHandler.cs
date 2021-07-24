using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody;
    
    private Camera mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            ballRigidbody.isKinematic = false;
            return;
        }


        Vector2 touchScreen = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(touchScreen);

        ballRigidbody.position = worldPoint;

        ballRigidbody.isKinematic = true;

        Debug.Log(worldPoint);
    }
}
