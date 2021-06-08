using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", 1f);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
