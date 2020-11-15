using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float gravity;
    GravityOrbit orbit;
    // Start is called before the first frame update
    void Start()
    {
        orbit = GetComponent<GravityOrbit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().gravity = orbit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
