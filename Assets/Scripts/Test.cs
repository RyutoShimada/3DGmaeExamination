using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //foreach (Transform t in this.transform)
        //{
        //    Debug.Log(t.name);
        //}

        this.transform.Cast<Transform>().ToList().ForEach(t => Debug.Log(t.name));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
