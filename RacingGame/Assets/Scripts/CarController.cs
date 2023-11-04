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
    public float horsePower;
    //public float maxSpeed;
    public float breakingForce;
    public float turningAngle;
    public float idleRpm;
    public float maxRpm;
    public float differentialRatio;

    public float[] gearRatios;
    private int currentGear = 0;

    public Speedometer speedometer;

    
    //private float currentSpeed;
    public float CurrentSpeed { get { return rb.velocity.magnitude * 2.23693629f; } }

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
        //UpdateCurrentSpeed();
        UpdateBreakingForce();
        UpdateTorque();
        UpdateSteerAngle();
        

        ApplyTorque();
        ApplyBreakingForce();
        ApplySteerAngle();
        //currentAcceleration = acceleration * Input.GetAxisRaw("Vertical");

        GearChanging();



    }

    private void Update()
    {
        UpdateWheelMeshes();
        UpdateWheelRpm();

        speedometer.SetNeedleAngle(currentRpm/8000f);
        speedometer.SetSpeedText(CurrentSpeed);
        speedometer.SetGearText(currentGear);
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


    private void UpdateBreakingForce()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakingForce = Mathf.Lerp(currentBreakingForce, breakingForce, Time.deltaTime);
        }
        else
        {
            currentBreakingForce = 0f;
        }
    }

    private void UpdateTorque()
    {
        currentRpm = Mathf.Lerp(currentRpm, Mathf.Max(idleRpm, currentWheelsRpm), Time.deltaTime * 3f);
        float rpm01 = Mathf.Clamp01(Mathf.Min(currentRpm, 8000f) / 8000f);
        float torque = ((horsePowerCurve.Evaluate(rpm01) * horsePower) / (currentRpm)) * gearRatios[currentGear] * differentialRatio * Input.GetAxisRaw("Vertical") * 5252f;
        currentTorque = Mathf.Lerp(currentTorque, torque, Time.deltaTime * 2f);
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
            wheelsColliders[i].brakeTorque = currentBreakingForce;
    }

    private void UpdateSteerAngle()
    {
        currentTurningAngle = Mathf.Lerp(currentTurningAngle, Mathf.Clamp(turningAngle * Input.GetAxisRaw("Horizontal") / Mathf.Clamp(Mathf.Abs(CurrentSpeed) / 50f, 1, 5), -45, 45), Time.deltaTime*3f);
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
                    sum += wheelsColliders[i].rpm;
                    currentWheelsRpm = Mathf.Abs((sum / 2) * gearRatios[currentGear] * differentialRatio);
                break;
            case WheelDriveType.RearWheelDrive:
                for (int i = 2; i < 4; i++)
                    sum += wheelsColliders[i].rpm;
                    currentWheelsRpm = Mathf.Abs((sum / 2) * gearRatios[currentGear] * differentialRatio);
                break;

            default:
                break;
        }
    }

    private void GearChanging()
    {
        //float f = Mathf.Abs(CurrentSpeed / maxSpeed);
        //float upgearlimit = (1 / (float)gearRatios.Length) * (currentGear + 1);
        //float downgearlimit = (1 / (float)gearRatios.Length) * currentGear;

        float f = currentRpm;
        float upgearlimit = 6000f;
        float downgearlimit = 2000f;

        if (currentGear > 0 && f < downgearlimit)
        {
            currentGear--;
        }

        if (f > upgearlimit && (currentGear < (gearRatios.Length - 1)))
        {
            currentGear++;
        }
    }

}
