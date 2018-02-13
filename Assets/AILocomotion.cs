using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle {

    private CharacterController controller;
    private Rigidbody theRigidbody;
    private Vector3 moveDistance;

    new void Start() {
        controller = GetComponent<CharacterController>();
        theRigidbody = GetComponent<Rigidbody>();
        moveDistance = new Vector3(0, 0, 0);
        base.Start();
    }

    void FixedUpdate() {
        velocity += acceleration * Time.fixedDeltaTime;
        if (velocity.sqrMagnitude > sqrMaxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }
        moveDistance = velocity * Time.fixedDeltaTime;
        if (isPlanar) {
            velocity.y = 0;
            moveDistance.y = 0;
        }
        if (controller != null) {
            controller.SimpleMove(velocity);
        } else if (theRigidbody == null || theRigidbody.isKinematic) {
            transform.position += moveDistance;
        } else {
            theRigidbody.MovePosition(theRigidbody.position + moveDistance);
        }
        if (velocity.sqrMagnitude > 0.00001) {
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
            if (isPlanar) {
                newForward.y = 0;
                transform.forward = newForward;
            }
        }
        //gameObject.GetComponent<Animation>().Play("walk");
    }
}