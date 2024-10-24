using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    // Public fields to reference the InputActionAsset (the input map)
    public InputActionReference accelAction;
    public InputActionReference gyroAction;

    // Speed multiplier for controlling the movement speed
    public float speedMultiplier = 1.0f;

    // Reference to the cube GameObject

    public Transform cubeTransform;

    private void Start()
    {
        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        if (AttitudeSensor.current != null)
        {
            InputSystem.EnableDevice(AttitudeSensor.current);
        }
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
        //cubeTransform = transform;

        // Enable the input actions
        accelAction.action.Enable();
        gyroAction.action.Enable();
    }

    private void Update()
    {
        // Get accelerometer input (as Vector3)
        Vector3 accelInput = accelAction.action.ReadValue<Vector3>();

        // Get gyroscope input (as Quaternion)
        Quaternion gyroInput = gyroAction.action.ReadValue<Quaternion>();

        // Convert the accelerometer input to movement speed
        Vector3 moveDirection = new Vector3(accelInput.x, 0, accelInput.y) * speedMultiplier;

        // Apply the gyroscope rotation to the cube
        cubeTransform.rotation = gyroInput;

        // Move the cube in the direction of its forward vector (based on rotation)
        cubeTransform.Translate(moveDirection * Time.deltaTime, Space.World);
       
       
        
    }

    private void OnDisable()
    {
        // Disable the input actions when not needed
        accelAction.action.Disable();
        gyroAction.action.Disable();
    }
}


