using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erin_AudioTest : MonoBehaviour
{
    public AudioSource door1;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("5")) { door1.Play(); }
    }
}
