using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FaithAudioTest : MonoBehaviour {
		
		public AudioSource HandSlapFD;
		
		void Update()
		{
			if (Input.GetKeyDown("3")) {HandSlapFD.Play();}
		}
	}