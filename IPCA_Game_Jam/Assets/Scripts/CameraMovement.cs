using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private float maxRotation, minRotation;
    private float rot_x, rot_y;

    private void Start()
    {
        rot_x = 0f; rot_y = 0f;
        /*minRotation = -15f;
        maxRotation = 45f;*/
        minRotation = 0f;
        maxRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rot_y -= Input.GetAxis("Mouse Y");
        rot_x += Input.GetAxis("Mouse X");

        if (rot_y < minRotation) rot_y = minRotation;
        else if (rot_y > maxRotation) rot_y = maxRotation;

        player.transform.eulerAngles = new Vector3(0, rot_x, 0);
        transform.eulerAngles = new Vector3(rot_y, rot_x, 0f);
    }
}
