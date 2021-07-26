using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Rigidbody2D ballRigidbody;
    private Camera mainCamera;
    private bool isDragging;
    private SpringJoint2D springJoint2D;
    [SerializeField] private float detachDelay;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot; //her top instantiate ettigimizde pivot noktasinda olmasi icin
    [SerializeField] private float respawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall();
        
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

    void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        ballRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        springJoint2D = ballInstance.GetComponent<SpringJoint2D>();

        springJoint2D.connectedBody = pivot;

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

        Invoke(nameof(SpawnNewBall), respawnDelay);

    }
     
        
    
}
