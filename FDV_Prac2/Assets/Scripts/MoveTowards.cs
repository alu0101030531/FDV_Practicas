using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 12;
    public float rotationSpeed = 20;
    private float threshold = .1f;
    public Transform goal; 
    void Start()
    {
    }

    Vector3 GetGoalDirection() {
        return goal.position - transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GetGoalDirection();
        if (direction.magnitude >= threshold) {
            transform.LookAt(goal.position);
            Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}