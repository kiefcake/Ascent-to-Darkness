using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{   //pickup mask determines what layer the player picksup
    //playercamera allows uses raycast in the center of the players screen
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObject;
    //start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {   //checking if player presses E to pickup things
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject = null;
                return;
            }
            //raycast to check object that player sellected, gravity gets disabled, rigidbody gets changed
            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
            {
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
            }
        }
    }
    private void FixedUpdate()
    {
        //checks if current object exists
        if(CurrentObject)
        {   
            //
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;
            //setting velocity 
            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}
