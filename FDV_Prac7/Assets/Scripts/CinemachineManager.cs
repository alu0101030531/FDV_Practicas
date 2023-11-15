using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //to use cinemachine

public class CinemachineManager : MonoBehaviour
{


    public CinemachineVirtualCamera vcam; //to assign cam
    public CinemachineVirtualCamera vcam2;
    public float zoom_factor = 0.01f;
    private float zoom;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       float axisY = Input.GetAxis("Vertical");
       zoom = axisY * zoom_factor * Time.deltaTime; 
       vcam.m_Lens.OrthographicSize += zoom;

       if (Input.GetKeyDown("g")) {
        vcam2.gameObject.SetActive(false);
        vcam.gameObject.SetActive(true);
       }
       if (Input.GetKeyDown("h")) {
        vcam.gameObject.SetActive(false);
        vcam2.gameObject.SetActive(true);
    }
    }
}
