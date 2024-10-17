using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlacement : MonoBehaviour
{
    ARRaycastManager aRRaycastManager;
    [SerializeField]
    private GameObject gameObjectToCreate;
    public Material mat;

    private GameObject placedObj;

    public bool spamLoadsOfObjects;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        // Check for raycast hits with AR planes
        if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            // Instantiate object at the hit pose or move the existing one. If you want to spawn multiple objects, not just one, you would replace the below if/else statement with just the line "placedobj = Instantiate...."

            if (spamLoadsOfObjects == true)
            {
                placedObj = Instantiate(gameObjectToCreate, hitPose.position, hitPose.rotation);
            }
            else
            {
                if (placedObj == null)
                {
                    placedObj = Instantiate(gameObjectToCreate, hitPose.position, hitPose.rotation);
               //     Material tempMat = placedObj.GetComponent<MeshRenderer>().material = new Material (mat);
                    
                    Debug.Log("debug created an object " + hitPose.position);
                }
                else
                {
                    placedObj.transform.position = hitPose.position;
                    placedObj.transform.rotation = hitPose.rotation;
                }
            }
           
          
        }
    }

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
}

