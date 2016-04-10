using UnityEngine;  // Used for Start(), Update(), Rect, Vector2, Input, Gizmos

// MonoBehaviour is the base class for most Unity stuff.  You can ignore it.  I'll explain the specific features as they appear.
public class PlayerMovement : MonoBehaviour {

    // Ship properties.  These are public so I can edit them in the Unity editor.  Yours should be private.
    public float mass = 1F;
    public float drag = 1.5F;
    public float minSpeed = 0.1F;
    public float maxSpeed = 5000F;
    public float boostForce = 2000F;
    public float thrustForce = 80F;
    public float turnSpeed = 0.1F;    
    
    // "Ropes" properties.  This is to enforce the boundaries.  Think wrestling ring.
    public Rect bounds;
    public float ropeBounceForce = 50;

    private Physics phys;
    private Rotation2D currentRotation = new Rotation2D(Mathf.PI / 2);  // pi/2 is just 90deg i.e. straight up.

    // Start is a Unity function called when this script runs for the first time.
    private void Start() {
        /* 
            GetComponent is just getting the instance of the Physics class attached to this object.  Another Unity thing.
            You will most likely want to do this by creating a new instance of the Physics class using a constructor
        */
        phys = GetComponent<Physics>();
        phys.Initialize(mass, drag, minSpeed, maxSpeed);
    }

    // Update is a Unity function that is called every frame.  This is basically just the game loop.
	private void Update () {

        // Thrust controls
        if (Input.GetButton("Forward")) {
            Vector2 forwardVector = currentRotation.GetVector();
            phys.AddForce(thrustForce * forwardVector);            
        } else if (Input.GetButton("Reverse")) {
            Vector2 forwardVector = currentRotation.GetVector();
            phys.AddForce(-1 * thrustForce * forwardVector);
        }

        // Boost controls
        if (Input.GetButtonDown("Boost")) {
            Vector2 forwardVector = currentRotation.GetVector();
            phys.AddForce(boostForce * forwardVector);
        }

        // Turn controls
        if (Input.GetButton("TurnLeft")) {
            currentRotation.AddAngle(turnSpeed);
            transform.eulerAngles = new Vector3(0, 0, currentRotation.GetDegrees());
        } else if (Input.GetButton("TurnRight")) {
            currentRotation.AddAngle(-turnSpeed);
            transform.eulerAngles = new Vector3(0, 0, currentRotation.GetDegrees());
        }

        if (!bounds.Contains(transform.position)) {
            Bounce();
        }

    }

    // OnDrawGizmos is a Unity function.  This just draws the bounds in the Unity editor.  You can ignore it.
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }


    /*
        Bounce() is what makes the player bounce when they pass the boundary of the map.
        This is simulating a spring.  There is a default bounce force and it is multiplied by how far past the boundary the player is,
        so the further past you are, the faster it pushes you back toward the center.
        Mathf.Abs is absolute value.  In this case, it gives the distance from the boundaries.
    */
    private void Bounce() {
        if (transform.position.x < bounds.xMin) {
            phys.AddForce(Vector2.right * ropeBounceForce * Mathf.Abs(transform.position.x - bounds.xMin));
        }
        if (transform.position.x > bounds.xMax) {
            phys.AddForce(Vector2.left * ropeBounceForce * Mathf.Abs(transform.position.x - bounds.xMax));
        }
        if (transform.position.y < bounds.yMin) {
            phys.AddForce(Vector2.up * ropeBounceForce * Mathf.Abs(transform.position.y - bounds.yMin));
        }
        if (transform.position.y > bounds.yMax) {
            phys.AddForce(Vector2.down * ropeBounceForce * Mathf.Abs(transform.position.y - bounds.yMax));
        }
    }


}
