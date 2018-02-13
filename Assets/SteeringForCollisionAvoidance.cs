using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCollisionAvoidance : Steering {

    public bool isPlanar;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private float maxForce;

    public float avoidanceForce;

    public float MAX_SEE_AHEAD = 2.0f;
    private GameObject[] allColliders;

    void Start() {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        maxForce = m_vehicle.maxForce;
        isPlanar = m_vehicle.isPlanar;

        if (avoidanceForce > maxForce) {
            avoidanceForce = maxForce;
        }
        allColliders = GameObject.FindGameObjectsWithTag("obstacle");
    }

    public override Vector3 Force() {
        RaycastHit hit;
        Vector3 force = new Vector3(0, 0, 0);
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;

        Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed));

        if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * velocity.magnitude / maxSpeed)) {
            Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed);
            force = ahead - hit.collider.transform.position;
            force *= avoidanceForce;
            if (isPlanar) {
                force.y = 0;
            }
            foreach (GameObject c in allColliders) {
                if (hit.collider.gameObject == c) {
                    c.GetComponent<Renderer>().material.color = Color.black;
                } else {
                    c.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        } else {
            foreach (GameObject c in allColliders) {
                c.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        return force;
    }
}
