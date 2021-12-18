using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour{
	
	public GameObject flashlight;
	public GameObject flashlightBright;	
	
	public static bool holdFlashlight=false;
	
	void Start(){
		if (holdFlashlight==false){flashlight.SetActive(false);}
		else {flashlight.SetActive(true);}
		flashlightBright.SetActive(false);
	}
	
	
	public void HoldFlashlight(){
        flashlight.SetActive(true);
		holdFlashlight=true;
    }

	public void DropFlashlight(){
        flashlight.SetActive(false);
		holdFlashlight=false;
    }


	public void OnFlashlight(){
        flashlightBright.SetActive(false);
    }
	
	public void OffFlashlight(){
        flashlightBright.SetActive(false);
    }
}
