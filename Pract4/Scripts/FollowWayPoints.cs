using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    float threshold = 0.1f;
    int waypoint_index = 0;
    public Transform Waypoints;
    public float speed = 12f;
    public float rotationSpeed = 20f;

    void Start()
    {
        
    }

    Vector3 GetGoalDirection(Transform goal) {
        return goal.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint_index < Waypoints.childCount) {
            Transform goal = Waypoints.GetChild(waypoint_index);
            Vector3 direction = GetGoalDirection(goal);
            if (direction.magnitude >= threshold) {
                Vector3 goal_point = new Vector3(goal.position.x, transform.position.y, goal.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goal_point), rotationSpeed * Time.deltaTime);
                Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            } else {
                waypoint_index++;
            }
        } else {
            waypoint_index = 0;
        }
        
        }
    }


