using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Ship properties
    public float mass = 1F;
    public float drag = 1.5F;
    public float minSpeed = 0.1F;
    public float maxSpeed = 5000F;
    public float boostForce = 2000F;
    public float thrustForce = 80F;
    public float turnSpeed = 0.1F;    
    
    // "Ropes" properties
    public Rect bounds;
    public float ropeBounceForce = 50;

    private Physics phys;
    private Rotation2D currentRotation = new Rotation2D(Mathf.PI / 2);

    private void Start() {
        phys = GetComponent<Physics>();
        phys.Initialize(mass, drag, minSpeed, maxSpeed);
    }

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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

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
