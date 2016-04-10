using UnityEngine;                  // Used for Vector2, LateUpdate(), Time
using System.Collections.Generic;   // Used for List

// MonoBehaviour is the base class for most Unity stuff.  You can ignore it.  I'll explain the specific features as they appear.
public class Physics : MonoBehaviour {

    /*
        Vector2 is Unity's 2D vector class.  Use your engine's equivalent.
        List<Vector2> is essentially an array of Vector2's, but with a variable length.
        If you don't have something similar, you can just use an array with a big enough length to account for all of your possible forces.
    */
    private Vector2 velocity = Vector2.zero;
    private List<Vector2> forces = new List<Vector2>();
    private float mass;
    private float drag;
    private float maxSpeed;
    private float minSpeed;

    /*
        LateUpdate() is a Unity function called at the end of every frame.  How you implement this depends on how your game loop works.
        We use Update() in PlayerMovement.cs to set everything that will be implemented in LateUpdate().
        Update() is also called every frame, but before LateUpdate().
    */
    private void LateUpdate() {
        // First we sum up all of the forces to figure out the total force affecting the player
        Vector2 totalForce = Vector2.zero;
        if (forces.Count > 0) {
            for (int i = 0; i < forces.Count; i++) {
                totalForce += forces[i] / mass;
            }
        }

        /*  
            transform.position is the position of the object in Unity.
            The reason I make a temporary vector here is because you can't set transform.position.x and transform.position.y directly, 
            and I prefer readable to concise.

            Time.deltaTime is the time since the last frame.  Your engine should have something similar, otherwise you need to calculate it yourself.
            It's very important to multiply by your deltaTime or your physics will be different depending on framerate.  This is BAD
        */
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0);
        velocity += totalForce * Time.deltaTime;
        velocity -= velocity * drag * drag * Time.deltaTime;
        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }
        newPosition.x += velocity.x * Time.deltaTime;
        newPosition.y += velocity.y * Time.deltaTime;
        transform.position = newPosition;

        // Since we set the forces each frame, we clear all of the forces after the frame as been processed.  If you use an array, you'll probably need a loop here
        forces.Clear();

        /* 
            Here we just enforce the minimum speed.  Without this, the ship will continue to move ridiculously slowly foreverish.
            sqrMagnitude is a Vector2 function that returns the magnitude squared.  That means we get (x^2 + y^2) instead of sqrt(x^2 + y^2).
            Calculating a square root is a little bit computationally expensive and the actual magnitude doesn't matter much here.  You can just change the minimum speed.
        */
        if (velocity.sqrMagnitude < minSpeed) {
            velocity = Vector2.zero;
        }
    }


    // You can use a constructor for this.  I have to do it this way because Unity doesn't let you use constructors for classes that inherit from MonoBehaviour
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
