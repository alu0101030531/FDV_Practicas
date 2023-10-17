using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsUI : MoveTowards
{
    // Start is called before the first frame update

    public void StartMoving()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       base.Move();
    }
}
