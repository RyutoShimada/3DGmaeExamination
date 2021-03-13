using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var p = GetComponent<ParticleSystem>();
        //p.main.scalingMode = ParticleSystemScalingMode.Local;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
