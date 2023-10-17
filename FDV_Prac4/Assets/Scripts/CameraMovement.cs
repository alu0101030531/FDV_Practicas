using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100f;
    private float xrotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");
        xrotation -= (rotationY * rotationSpeed * Time.deltaTime); 
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(xrotation, -90f, 90f), 0f, 0f);
        player.Rotate(Vector3.up * rotationX * rotationSpeed * Time.deltaTime);
    }
}