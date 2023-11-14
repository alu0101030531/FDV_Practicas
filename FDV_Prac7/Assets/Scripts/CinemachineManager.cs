using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //to use cinemachine

public class CinemachineManager : MonoBehaviour
{


    public CinemachineVirtualCamera vcam; //to assign cam
    // Start is called before the first frame update
    void Start()
    {
       vcam.m_Lens.OrthographicSize = 5; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
