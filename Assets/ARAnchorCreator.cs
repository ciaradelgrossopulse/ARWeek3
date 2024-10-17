using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))] // Add the ARAnchorManager requirement
public class ARAnchorCreator : MonoBehaviour
{
    public bool placingMode = true;
    ARRaycastManager aRRaycastManager;
    ARAnchorManager aRAnchorManager;

    [SerializeField]
    private GameObject gameObjectToCreate;

    private GameObject placedObj;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRAnchorManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placingMode)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
            {
                return;
            }

            // Perform a raycast from the touch position
            if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Debug.Log("Object placed and anchored");
                var hitPose = hits[0].pose;
                Vector3 position = hitPose.position;
                Quaternion rotation = hitPose.rotation;

                // Instantiate the object at the anchor's position and parent it to the anchor

                placedObj = Instantiate(gameObjectToCreate, position, rotation);
                placedObj.AddComponent<ARAnchor>();
                Debug.Log("Object placed and anchored");


            }
        }
        else
        {
            ///do something like selecting invisible objects.
        }
    }

    // Method to get the touch position using the New Input System
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // Ensure we have a touchscreen and a primary touch
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // Get the position of the primary touch
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        touchPosition = default;
        return false;
    }

    public void SwitchMode()
    {
        placingMode = !placingMode;
    }
}

