using UnityEngine;
using UnityEngine.InputSystem;  // New Input System
using UnityEngine.XR.ARFoundation;

public class TouchToChangeColour : MonoBehaviour
{
    public Camera arCamera;  // AR Camera for raycasting
    public InputAction tapAction;  // Input Action for detecting tap
    public LayerMask interactableLayer;  // Layer for objects that can be interacted with

    private void OnEnable()
    {
        tapAction.Enable();  // Enable input action
    }

    private void OnDisable()
    {
        tapAction.Disable();  // Disable input action
    }

    void Update()
    {
        // Check if the tap action is performed
        if (tapAction.triggered)
        {
            Vector2 touchPosition = tapAction.ReadValue<Vector2>();
            Ray ray = arCamera.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
            {
                // Interact with the object that was hit
                OnObjectTapped(hit.collider.gameObject);
            }
        }
    }

    // Handle interaction when an object is tapped
    void OnObjectTapped(GameObject tappedObject)
    {
        // Example: Change the object's color on tap
        Renderer objectRenderer = tappedObject.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Random.ColorHSV();  // Change to a random color
        }
    }
}


