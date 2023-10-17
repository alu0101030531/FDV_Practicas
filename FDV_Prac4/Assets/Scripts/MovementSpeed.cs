using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void ChangeSpeed(float speed);
public class MovementSpeed : MonoBehaviour
{
    public static ChangeSpeed OnChangeSpeed;
    public float movement_speed = 12f;
    public float turbo_modifier = 10f;
    public float collision_reduction = 2f;
    bool turbo_active = false;
    public TMP_Text speed;
    private string turbo_speed_str = "Turbo";
    private string normal_speed_str = "Normal";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed() {
        if (turbo_active) {
            speed.text = normal_speed_str;
            if (OnChangeSpeed != null)
                OnChangeSpeed(movement_speed + turbo_modifier);
        } else {
            speed.text = turbo_speed_str;
            if (OnChangeSpeed != null)
                OnChangeSpeed(movement_speed);
        }
        turbo_active = !turbo_active;
    }
}
