using UnityEngine;  // Used for Vector2 class

public class Rotation2D {

    /*                --------ALL ABOUT RADIANS--------
        Radians are a way to measure angles based on the circumference of a circle with radius 1
        A full circle is 2pi radians.  That's 2*pi*R where R is the radius.  Since R=1, the circumference is 2*pi.
        Half of a circle is pi radians.
        Going around the circle twice, you get 4*pi.  The angle that this gives is the same as 2*pi.
        Similarly, the angle given by 3*pi is the same as 1*pi.
        0 radians is the same as 2*pi radians, since going all the way around the circle takes you back where you started.
        360 degrees is 2pi radians.  180 degress is pi radians.
        If you look at the conversion formula for radians to degrees, you can see this.
        deg = rad * 180 / pi
        So a half circle, pi radians, is pi * 180 / pi = 180deg
        A full circle, 2pi radians, is 2*pi*180/pi = 360deg
        There are two big reasons that we use radians.
        1. Your trig functions, sin and cos, almost always take radians as an argument
        2. They're actually easier to think about.
        What is 1507 degrees?  Have fun dividing by 360 and figuring out the fractions.
        What is 1507*pi radians?  Well 1507 is odd so this is just pi radians.
        Yes it's that simple.  even*pi=2*pi radians.  odd*pi=pi radians
        5/2*pi radians = (4 + 1)/2*pi radians = (4/2 + 1/2)*pi radians = 2pi + pi/2 radians = 0 + pi/2 radians
        1/2 of a half circle is a quarter circle so pi/2 radians = 90 degrees
        If you still feel uncomfortable, go learn the unit circle.  Trig is important.
    */

    private float currentRotation = 0;  // in radians

    // Constructor
    public Rotation2D (float angle) {
        currentRotation = angle;
    }

    public float GetRadians() {
        return currentRotation;
    }

    public float GetDegrees() {
        return currentRotation * 180 / Mathf.PI;
    }

    /*
        Note: I'm going to wrap some of the longer math statements in square brackets like [math]
        This class uses polar coordinates by default.
        That means that instead of our coordinates being (x,y), we're using (radius,angle) instead.
        Since we really only care about the angle, we set the radius to 1 because it simplifies our calculations.
        To convert from polar to rectangular coordinates, we have the following two formulas:
        x = R*cos(angle)
        y = R*sin(angle)
        Since R=1, we can just do x=cos(angle) and y=sin(angle) instead.
        Since our position and velocity vectors use rectangular coordinates, it's useful to have an easy way to get rectangular coordinates from the rotation, hence GetVector().

        The magnitude of a 2D vector (x,y) is given by sqrt(x^2 + y^2).  A vector with magnitude=1 is called a unit vector.
        Unit vectors are nice because you can multiply them by a number to get a new vector in the same direction with a magnitude the same as your number.
        So if (x,y) is a unit vector, 2*(x,y) = (2x,2y) is a vector in the same direction as (x,y) with magnitude 2.
        
        Here's an example combining all of this information.
        Say you want to make your speed 10mph at an angle of pi/2 radians.
        GetVector() is going to return a vector (x,y) = (cos(pi/2), sin(pi/2)).
        It just so happens that [cos(angle)^2 + sin(angle)^2 = 1] if both angles are the same, 
        so the magnitude of the vector returned by GetVector() is [[sqrt(cos(pi/2)^2 + sin(pi/2)^2) = sqrt(1) = 1]], thus (x,y) is a unit vector.
        Then all we have to do is set the velocity to 10*(x,y) and we have a velocity vector with a speed 10 in the direction of pi/2 radians.
    */
    public Vector2 GetVector() {
        return new Vector2(Mathf.Cos(currentRotation), Mathf.Sin(currentRotation));
    }

    public void AddAngle(float angle) {
        currentRotation += angle;
        currentRotation = currentRotation % (2 * Mathf.PI);  // % (2 * Mathf.PI) ensures that the current rotation is between 0 and 2pi
    }

    public void SetRotation(float newRotation) {
        currentRotation = newRotation % (2 * Mathf.PI);
    }

}
