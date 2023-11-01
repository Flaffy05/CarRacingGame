using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    
    internal enum WheelDriveType
    {
        FourWheelDrive,
        RearWheelDrive,
        FrontWheelDrive
    }


    public WheelCollider[] wheelsColliders;
    
    public Transform[] wheelsMeshes;//transform of the wheels meshes

    [SerializeField]private WheelDriveType wheelDrive = WheelDriveType.FourWheelDrive;

    private Rigidbody rb;

    public AnimationCurve horsePowerCurve;
    public float acceleration;
    public float breakingForce;
    public float turningAngle;
    public float idleRpm;
    public float maxRpm;
    public float differentialRatio;

    public float[] gearRatios;
    private int currentGear = 0;

    public Speedometer speedometer;

    private float currentSpeed;

    private float currentRpm;
    private float currentWheelsRpm;
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
        UpdateSteerAngle();
        

        ApplyTorque();
        ApplyBreakingForce();
        ApplySteerAngle();
        //currentAcceleration = acceleration * Input.GetAxisRaw("Vertical");



    }

    private void Update()
    {
        UpdateWheelMeshes();
        UpdateWheelRpm();

        speedometer.SetNeedleAngle(currentRpm/8000f);
        //currentRpm += 0.01f;
    }

    private void UpdateWheelMeshes()
    {
        Vector3 position;
        Quaternion rotation;
        for (int i = 0; i < wheelsColliders.Length; i++)
        {
            wheelsColliders[i].GetWorldPose(out position, out rotation);
            wheelsMeshes[i].position = position;
            wheelsMeshes[i].rotation = rotation;
        }   
        
        //Quaternion offset = new ;
        
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
        currentRpm = Mathf.Lerp(currentRpm, Mathf.Max(idleRpm, currentWheelsRpm), Time.deltaTime * 3f);
        currentTorque =  (horsePowerCurve.Evaluate(currentRpm)/(currentRpm)) * gearRatios[currentGear] * differentialRatio * Input.GetAxisRaw("Vertical") * 52520f;
    }

    private void ApplyTorque()
    {
        switch(wheelDrive)
        {
            case WheelDriveType.FourWheelDrive:
                for (int i = 0; i < 4; i++)
                {
                    wheelsColliders[i].motorTorque = currentTorque;
                }
                    
                break;
            case WheelDriveType.FrontWheelDrive:
                for (int i = 0; i < 2; i++)
                    wheelsColliders[i].motorTorque = currentTorque;
                break;
            case WheelDriveType.RearWheelDrive:
                for (int i = 2; i < 4; i++)
                    wheelsColliders[i].motorTorque = currentTorque;
                break;

            default:
                break;
        }
    }

    private void ApplyBreakingForce()
    {
        for (int i = 0; i < 4; i++)
            wheelsColliders[i].brakeTorque = breakingForce;
    }

    private void UpdateSteerAngle()
    {
        currentTurningAngle = Mathf.Clamp(turningAngle * Input.GetAxisRaw("Horizontal") / Mathf.Clamp(Mathf.Abs(currentSpeed) / 2.5f, 1, 10), -40, 40);

    }

    private void ApplySteerAngle()
    {
        for (int i = 0; i < 2; i++)
            wheelsColliders[i].steerAngle = currentTurningAngle;
    }

    private void UpdateWheelRpm()
    {
        float sum = 0;

        switch (wheelDrive)
        {
            case WheelDriveType.FourWheelDrive:
                for (int i = 0; i < 4; i++)
                    sum += wheelsColliders[i].rpm;
                    currentWheelsRpm = Mathf.Abs((sum / 4f) * gearRatios[currentGear] * differentialRatio);
                break;
            case WheelDriveType.FrontWheelDrive:
                for (int i = 0; i < 2; i++)
                    sum += wheelsColliders[i].motorTorque = currentTorque;
                    currentWheelsRpm = Mathf.Abs((sum / 2) * gearRatios[currentGear] * differentialRatio);
                break;
            case WheelDriveType.RearWheelDrive:
                for (int i = 2; i < 4; i++)
                    sum += wheelsColliders[i].motorTorque = currentTorque;
                    currentWheelsRpm = Mathf.Abs((sum / 2) * gearRatios[currentGear] * differentialRatio);
                break;

            default:
                break;
        }
    }
}
