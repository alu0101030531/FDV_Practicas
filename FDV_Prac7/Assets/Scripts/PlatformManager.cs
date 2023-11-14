using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    // Start is called before the first frame update
    public float speed = 12f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float axisX = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(axisX * speed * Time.deltaTime, 0f));
        
    }
}
