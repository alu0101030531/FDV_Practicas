using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 12;
    void Start()
    {
        InvokeRepeating("Move", 1f, 2f);
    }

    void Move() {
        Vector3 position = new Vector3(Random.Range(0, 25), Random.Range(0, 25), Random.Range(0, 25));
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}