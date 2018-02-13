using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringFollowPath : Steering {

    public GameObject[] waypoints = new GameObject[4];
    private Transform target;
    private int currentNode;
    private float arriveDistance;
    private float sqrArriveDistance;

    private int numberOfNodes;
    private Vector3 force;

    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private bool isPlanar;

    public float slowDownDistance;


    void Start() {
        numberOfNodes = waypoints.Length;
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlanar = m_vehicle.isPlanar;

        currentNode = 0;

        target = waypoints[currentNode].transform;
        arriveDistance = 1.0f;
        sqrArriveDistance = arriveDistance * arriveDistance;
    }

    public override Vector3 Force() {
        force = new Vector3(0, 0, 0);
        Vector3 dist = target.position - transform.position;
        if (isPlanar) {
            dist.y = 0;
        }
        if (currentNode == numberOfNodes - 1) {
            if (dist.magnitude > slowDownDistance) {
                desiredVelocity = dist.normalized * maxSpeed;
                force = desiredVelocity - m_vehicle.velocity;
            } else {
                desiredVelocity = dist - m_vehicle.velocity;
                force = desiredVelocity - m_vehicle.velocity;
            }
        } else {
            if (dist.sqrMagnitude < sqrArriveDistance) {
                currentNode++;
                target = waypoints[currentNode].transform;
            }
            desiredVelocity = dist.normalized * maxSpeed;
            force = desiredVelocity - m_vehicle.velocity;
        }
        return force;
    }
}