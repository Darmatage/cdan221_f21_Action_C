using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FaithAudioTest : MonoBehaviour {
	
    //Start is called before the first frame update
	
	public AudioSource HandSlapFD;
    
	//Update is called once per frame void Update() 
	
    void Update()
    {
	    
		if (Input.GetKeyDown("5")){HandSlapFD.Play();}
	
        
    }
	}