using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]

    //Look sensitivity
    public float mouseSensitivity = 5;

    //Hidden
    Vector2 rotation = Vector2.zero;

    //Focus Manager handler
    [HideInInspector] public bool focused;

    //Float that multiplies sensitivity, loaded on start
    float sensMultiplier = 1.0f;

    void Update()
    {
        //Retrieve Sensitivity of player
        sensMultiplier = PlayerPrefs.GetFloat("Sensitivity");

        if (!focused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotation.y += (Input.GetAxis("Mouse X") * Time.deltaTime * 100 * mouseSensitivity * sensMultiplier);
        rotation.x += (-Input.GetAxis("Mouse Y") * Time.deltaTime * 100 * mouseSensitivity * sensMultiplier);

        rotation.x = Mathf.Clamp(rotation.x, -85, 85);

        Quaternion localRotation = Quaternion.Euler(rotation.x, rotation.y, 0.0f);
        transform.rotation = localRotation;
    }
}
