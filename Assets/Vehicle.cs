using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    private Steering[] steerings;
    public float maxSpeed = 10;
    public float maxForce = 100;
    protected float sqrMaxSpeed;
    public float mass = 1;
    public Vector3 velocity;
    public float damping = 0.9f;
    public float computeInterval = 0.2f;
    public bool isPlanar = true;
    private Vector3 steeringForce;
    protected Vector3 acceleration;
    private float timer;

    protected void Start() {
        steeringForce = new Vector3(0, 0, 0);
        sqrMaxSpeed = maxSpeed * maxSpeed;
        timer = 0;
        steerings = GetComponents<Steering>();
    }

    protected void Update() {
        timer += Time.deltaTime;
        steeringForce = new Vector3(0, 0, 0);
        if (timer > computeInterval) {
            foreach (Steering s in steerings) {
                if (s.enabled) {
                    steeringForce += s.Force() * s.weight;
                }
            }
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            acceleration = steeringForce / mass;
            timer = 0;
        }
    }
}
