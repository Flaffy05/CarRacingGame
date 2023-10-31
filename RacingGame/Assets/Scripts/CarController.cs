using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    /*
    public WheelCollider frontRight;
    public WheelCollider backRight;
    public WheelCollider frontLeft;
    public WheelCollider backLeft;
    */
    public WheelCollider[] wheelsColliders;
    /*
    public Transform frontRightMesh;
    public Transform backRightMesh;
    public Transform frontLeftMesh;
    public Transform backLeftMesh;
    */

    public Transform[] wheelsMeshes;

    

    private Rigidbody rb;

    public AnimationCurve horsePowerCurve;
    public float acceleration;
    public float breakingForce;
    public float turningAngle;

    public float[] gearRatios;
    private int currentGear;

    public Speedometer speedometer;

    private float currentSpeed;

    private float currentRpm;
    private float currentTorque;
    private float currentBreakingForce;
    private float currentTurningAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateCurrentSpeed();
        UpdateBreakingForce();
        UpdateTorque();
        //currentAcceleration = acceleration * Input.GetAxisRaw("Vertical");

        currentTurningAngle = Mathf.Clamp(turningAngle * Input.GetAxisRaw("Horizontal") / Mathf.Clamp(Mathf.Abs(currentSpeed)/2.5f, 1, 10), -40,40);


        

        for (int i = 0; i < 2; i++)
            wheelsColliders[i].motorTorque = currentTorque;

        for (int i = 0; i < 4; i++)
            wheelsColliders[i].brakeTorque = breakingForce;

        for (int i = 0; i < 2; i++)
            wheelsColliders[i+2].steerAngle = currentTurningAngle;
    }

    private void Update()
    {
        for(int i = 0; i < wheelsColliders.Length; i++)
            UpdateWheelMeshes(wheelsColliders[i], wheelsMeshes[i]);

        speedometer.SetNeedleAngle(currentRpm/9000f);
        //currentRpm += 0.01f;
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

    private void UpdateBreakingForce()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakingForce = breakingForce;
        }
        else
        {
            currentBreakingForce = 0f;
        }
    }

    private void UpdateTorque()
    {
        //float wheelsRpm;

        //return (horsePowerCurve.Evaluate(Mathf.Clamp01(currentRpm))/currentRpm) * gearRatios[currentGear] * Input.GetAxisRaw("Vertical") * 5252f;
    }

}
