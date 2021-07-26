using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody;
    
    private Camera mainCamera;
    private bool isDragging;
    [SerializeField] private SpringJoint2D springJoint2D;
    [SerializeField] private float detachDelay;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ballRigidbody == null) {return;}
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if(isDragging)
            {
                LaunchBall();
            }
            
            isDragging = false;
            return;
        
        }

        
        isDragging = true;

        Vector2 touchScreen = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(touchScreen);

        ballRigidbody.position = worldPoint;

        ballRigidbody.isKinematic = true;

        Debug.Log(worldPoint);
    }

    void LaunchBall()
    {
        ballRigidbody.isKinematic = false;
        ballRigidbody = null;
        Invoke(nameof(DetachBall), detachDelay);
        
    }
     
     void DetachBall()
     
    {
        springJoint2D.enabled = false;
        springJoint2D = null;
    }
     
        
    
}
