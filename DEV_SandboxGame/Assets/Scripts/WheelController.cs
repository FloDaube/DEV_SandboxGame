using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public enum DriveMode
{
    Front,
    Rear,
    All
};
public class WheelController : NetworkBehaviour
{

    [SerializeField] WheelCollider FrontRight;
    [SerializeField] WheelCollider FrontLeft;
    [SerializeField] WheelCollider RearRight;
    [SerializeField] WheelCollider RearLeft;

    [SerializeField] Transform TFrontRight;
    [SerializeField] Transform TFrontLeft;
    [SerializeField] Transform TRearRight;
    [SerializeField] Transform TRearLeft;

    [SerializeField] CinemachineFreeLook  Camera;

    private ControlsInputs _ControleInputs;

    public float MaxKmh = 100;
    //Debug
    public float CurrentKmh = 0;
    public float R = 0;
    public float RPM = 0;
    
    public bool Enabled = false;
    public float acceleration = 500f;
    public float breakingforce = 300f;
    public float maxTrunAngle = 20f;
    public DriveMode DriveMode = DriveMode.Front;

    private float _currentAcceleration = 0.0f;
    private float _currentBreakForce = 0.0f;
    private float _currentTurnAngle = 0.0f;


    //Start my Camera
    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            Camera.Priority = 10;
        }
        else
        {
            Camera.Priority = 0;
        }
    }

    private void Awake()
    {
        _ControleInputs = new ControlsInputs();
        _ControleInputs.VehicleCar.Enable();
    }
    private void FixedUpdate()
    {

        if (!IsLocalPlayer)
        {
            return;        
        }
        //Read Input
        ReadInputs();
        //Apply Forces
        ApplyForces();
        //Handle Transform        
        UpdateWheel(RearLeft, TRearLeft);
        UpdateWheel(FrontRight, TFrontRight);
        UpdateWheel(FrontLeft, TFrontLeft);
        UpdateWheel(RearRight, TRearRight);

    }

    private void UpdateWheel(WheelCollider Col, Transform Trans)
    {
        //Get Information from Colider
        Vector3 Position;
        Quaternion Rotation;
        Col.GetWorldPose(out Position, out Rotation);
        //Set Tranform
        Trans.position = Position;
        Trans.rotation = Rotation;
    }

    private void ApplyForces()
    {
        if (DriveMode == DriveMode.Front)
        {
            FrontRight.motorTorque = _currentAcceleration;
            FrontLeft.motorTorque = _currentAcceleration;
        }
        else if (DriveMode == DriveMode.Rear)
        {
            RearRight.motorTorque = _currentAcceleration;
            RearLeft.motorTorque = _currentAcceleration;
        }
        else
        {
            FrontRight.motorTorque = _currentAcceleration;
            FrontLeft.motorTorque = _currentAcceleration;
            RearRight.motorTorque = _currentAcceleration;
            RearLeft.motorTorque = _currentAcceleration;
        }


        FrontLeft.steerAngle = _currentTurnAngle;
        FrontRight.steerAngle = _currentTurnAngle;

        FrontRight.brakeTorque = _currentBreakForce;
        FrontLeft.brakeTorque = _currentBreakForce;
        RearRight.brakeTorque = _currentBreakForce;
        RearLeft.brakeTorque = _currentBreakForce;
    }
    private void ReadInputs()
    {
        // Forward Backward
        _currentAcceleration = acceleration * _ControleInputs.VehicleCar.Movement.ReadValue<Vector2>().y;
        // Turning
        _currentTurnAngle = maxTrunAngle * _ControleInputs.VehicleCar.Movement.ReadValue<Vector2>().x;

        //Handbreak
            _currentBreakForce = breakingforce * _ControleInputs.VehicleCar.Break.ReadValue<float>();
        
    }
}
