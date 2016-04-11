using UnityEngine;
using System.Collections.Generic;

public class Physics : MonoBehaviour {

    private Vector2 velocity = Vector2.zero;
    private List<Vector2> forces = new List<Vector2>();
    private float mass;
    private float drag;
    private float maxSpeed;
    private float minSpeed;

    private void LateUpdate() {
        Vector2 totalForce = Vector2.zero;
        if (forces.Count > 0) {
            for (int i = 0; i < forces.Count; i++) {
                totalForce += forces[i] / mass;
            }
        }

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0);
        velocity += totalForce * Time.deltaTime;
        velocity -= velocity * drag * Time.deltaTime;
        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }
        newPosition.x += velocity.x * Time.deltaTime;
        newPosition.y += velocity.y * Time.deltaTime;
        transform.position = newPosition;

        forces.Clear();

        if (velocity.sqrMagnitude < minSpeed) {
            velocity = Vector2.zero;
        }
    }


    public void Initialize(float mass, float drag, float minSpeed, float maxSpeed) {
        this.mass = mass;
        this.drag = drag;
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;
    }

    public void AddForce(Vector2 newForce) {
        forces.Add(newForce);
    }

}
