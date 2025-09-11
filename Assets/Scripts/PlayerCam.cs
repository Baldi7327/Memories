using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] private float Sensetivity;
    private Player player;
    private float mouseX = 0;
    private float mouseY = 0;
    private float xRotation = 0, yRotation = 0;
    Quaternion cameraDirection;

    void Start()
    {

        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Sensetivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * Sensetivity * Time.deltaTime;
        yRotation += mouseX;
        xRotation -= mouseY;
        if (xRotation < -90f)
        {
            xRotation = -90f;
        }
        else if (xRotation > 90f)
        {
            xRotation = 90f;
        }
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);




    }
}

