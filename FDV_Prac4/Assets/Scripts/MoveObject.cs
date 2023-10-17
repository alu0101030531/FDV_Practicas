using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 FinalPos;
    private bool move = false;
    public float speed = 5f;
    void Start()
    {
        FinalPos = new Vector3(transform.position.x, 5f, transform.position.z); 
        PlayerMovement.OnMoveObjects += MoveObjectToPos; 
    }

    void MoveObjectToPos()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            transform.position = Vector3.MoveTowards(transform.position, FinalPos, speed * Time.deltaTime);
    }
}
