using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steering : MonoBehaviour {

    public float weight = 1;

    void Start() { }

    void Update() { }

    public virtual Vector3 Force() {
        return new Vector3(0, 0, 0);
    }

}