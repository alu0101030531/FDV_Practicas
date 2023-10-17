using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 0.01f;
    private float threshold = 1f;
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
            Vector3 goal_point = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goal_point), rotationSpeed * Time.deltaTime);
            Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}