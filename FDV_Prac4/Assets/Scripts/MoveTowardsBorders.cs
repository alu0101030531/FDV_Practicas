using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsBorders : MoveTowards
{
    // Start is called before the first frame update
    bool in_borders = false;
    override public void Start()
    {
        PlayerMovement.OnBorderEnter += OnBorderEnter;
    }

    void OnBorderEnter()
    {
        Debug.Log("hola");
        in_borders = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (in_borders)
        {
            base.Move(); 
        }
    }
}
