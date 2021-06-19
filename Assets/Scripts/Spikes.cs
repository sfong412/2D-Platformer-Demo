using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Enemy
{
    public bool instantDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instantDeath == false)
        {
            damageValue = -1;
        }
        else if (instantDeath == true)
        {
            damageValue = -99;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
