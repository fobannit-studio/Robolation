using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float speed = 0.25f;
    public CharacterController controller;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            y = -1;
        }

        Vector3 move = transform.right * x + transform.forward * z + transform.up*y*0.5f;
        controller.Move(move*speed*Time.deltaTime);




        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        this.transform.Rotate(Vector3.up * mouseX);

    }
}
