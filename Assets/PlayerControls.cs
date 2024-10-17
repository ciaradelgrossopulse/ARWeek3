using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    void OnJump()
    {
        Debug.Log("I jumped");
    }

    void OnInteract()
    {
        Debug.Log("I jinteracteded");
    }

    public void Jump()
    {
        Debug.Log("I pressed a button to  jump");
        OnJump();
    }
}
