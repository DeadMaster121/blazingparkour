using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {
    private Rigidbody rigidBody;
    private Animator animator;

    public float speed = 2f;
    public float position;
    public float range = 5;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        position = transform.position.z;
    }

    void Update() {
        Move();
        Check();
    }

    void Move() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void Check() {
        if (Mathf.Round(transform.position.z) >= position + range / 2) {
            transform.Rotate(Vector3.up, 180);
        }
        else if (Mathf.Round(transform.position.z) <= position - range / 2) {
            transform.Rotate(Vector3.up, 180);
        }
    }
}
