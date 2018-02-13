using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForArrive : Steering {

    public bool isPanar = true;
    public float arrivalDistance = 0.3f;
    public float characterRadius = 1.2f;
    public float slowDownDistance;
    public GameObject target;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;

    void Start() {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPanar = m_vehicle.isPlanar;
    }

    public override Vector3 Force() {
        Vector3 toTarget = target.transform.position - transform.position;
        Vector3 desiredVelocity;
        Vector3 returnForce;
        if (isPanar) {
            toTarget.y = 0;
        }
        float distance = toTarget.magnitude;
        if (distance > slowDownDistance) {
            desiredVelocity = toTarget.normalized * maxSpeed;
            returnForce = desiredVelocity - m_vehicle.velocity;
        } else {
            desiredVelocity = toTarget - m_vehicle.velocity;
            returnForce = desiredVelocity - m_vehicle.velocity;
        }
        return returnForce;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(target.transform.position, slowDownDistance);
    }

}
