using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {
    public GameObject projectile;
    public float delay = 8;
    public float next = 8.5f;

    void Start() {
        InvokeRepeating("LaunchProjectile", delay, next);
    }

    void Update() { 
    }

    void LaunchProjectile() {
        Instantiate(projectile, transform.position, projectile.transform.rotation);
    }
}
