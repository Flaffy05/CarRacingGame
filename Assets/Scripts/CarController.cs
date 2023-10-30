using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider frontRight;
    public WheelCollider backRight;
    public WheelCollider frontLeft;
    public WheelCollider backLeft;

    public Transform frontRightMesh;
    public Transform backRightMesh;
    public Transform frontLeftMesh;
    public Transform backLeftMesh;

    private Rigidbody rb;

    public float acceleration;
    public float breakingForce;
    public float turningAngle;

    private float currentSpeed;

    //private float currentRpm;
    private float currentAcceleration;
    private float currentBreakingForce;
    private float currentTurningAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateCurrentSpeed();

        currentAcceleration = acceleration * Input.GetAxisRaw("Vertical");

        currentTurningAngle = Mathf.Clamp(turningAngle * Input.GetAxisRaw("Horizontal") / Mathf.Clamp(Mathf.Abs(currentSpeed)/2.5f, 1, 10), -40,40);


        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakingForce = breakingForce;
        }else
        {
            currentBreakingForce = 0f;
        }

        backLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;


        backLeft.brakeTorque = currentBreakingForce;
        backRight.brakeTorque = currentBreakingForce;

        frontRight.steerAngle = currentTurningAngle;
        frontLeft.steerAngle = currentTurningAngle;
    }

    private void Update()
    {
        UpdateWheelMeshes(frontLeft, frontLeftMesh);
        UpdateWheelMeshes(frontRight, frontRightMesh);
        UpdateWheelMeshes(backLeft, backLeftMesh);
        UpdateWheelMeshes(backRight, backRightMesh);
    }

    private void UpdateWheelMeshes(WheelCollider collider, Transform meshTransform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        meshTransform.position = position;
        //Quaternion offset = new ;
        meshTransform.rotation = rotation;
    }

    private void UpdateCurrentSpeed()
    {
        currentSpeed = rb.velocity.magnitude;
    }
}
