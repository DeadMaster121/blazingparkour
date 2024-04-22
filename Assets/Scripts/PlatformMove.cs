using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 2.5f;
    public Vector2 initialPosition;
    public float moveRange = 5;
    public bool moveVertical = true;
    private Vector2 direction;

    void Start() {
        initialPosition = transform.position;
        direction = moveVertical ? Vector2.up : Vector2.right;
    }

    void Update() {
        Move();
        CheckBounds();
    }

    void Move() {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void CheckBounds() {
        float distance = moveVertical
            ? transform.position.y - initialPosition.y
            : transform.position.x - initialPosition.x;

        if (Mathf.Abs(distance) >= moveRange / 2) {
            direction *= -1;
        }
    }
}
