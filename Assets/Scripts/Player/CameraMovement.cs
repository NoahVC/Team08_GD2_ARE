using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]

    [SerializeField] float sensX = 1f;
    [SerializeField] float sensY = 1f;
    [SerializeField] Transform orientation = null;
    [SerializeField] Transform cam = null;

    float mouseX;
    float mouseY;



    float xRotation;
    float yRotation;

    private void Start()
    {
        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse inputs
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * Time.deltaTime;
        xRotation -= mouseY * sensY * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Camera rotation
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
