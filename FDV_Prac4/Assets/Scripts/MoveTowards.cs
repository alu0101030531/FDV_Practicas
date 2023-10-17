using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 12;
    public float rotationSpeed = 20;
    private float threshold = .1f;
    public Transform goal; 
    bool move_towards = false;
    void Start()
    {
        PlayerMovement.OnBorderEnter += OnBorderEnter;
    }

    void OnBorderEnter()
    {
        move_towards = true;
    }

    Vector3 GetGoalDirection() {
        return goal.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_towards)
        {
            Vector3 direction = GetGoalDirection();
            if (direction.magnitude >= threshold) {
                transform.LookAt(goal.position);
                Debug.DrawRay(this.transform.position, direction.normalized, Color.red);
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            }
        }
    }
}