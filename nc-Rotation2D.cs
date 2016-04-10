using UnityEngine;

public class Rotation2D {

    private float currentRotation = 0;  // in radians

    public Rotation2D (float angle) {
        currentRotation = angle;
    }

    public float GetRadians() {
        return currentRotation;
    }

    public float GetDegrees() {
        return currentRotation * 180 / Mathf.PI;
    }

    public Vector2 GetVector() {
        return new Vector2(Mathf.Cos(currentRotation), Mathf.Sin(currentRotation));
    }

    public void AddAngle(float angle) {
        currentRotation += angle;
        currentRotation = currentRotation % (2 * Mathf.PI);
    }

    public void SetRotation(float newRotation) {
        currentRotation = newRotation % (2 * Mathf.PI);
    }

}
