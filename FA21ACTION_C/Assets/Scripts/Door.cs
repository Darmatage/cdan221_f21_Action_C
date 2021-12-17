
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string NextLevel = "DormRoomScene";
	public GameObject pressE;
	public GameObject doorLocked;
	private bool canOpen = false;
	public bool isLocked = false;
	
	public string LockedKey;
	
	

	void Start(){
		pressE.SetActive(false);
	} 

	public void Update(){
		if ((Input.GetKeyDown("e"))&&(canOpen)){
			SceneManager.LoadScene(NextLevel);
		}
	}


    public void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
				
			if (isLocked== false){
				pressE.SetActive(true);
				canOpen = true;
			}
			else if (isLocked== true){
				if (playerHasKey()){
					pressE.SetActive(true);
					canOpen = true;
				}
				else {doorLocked.SetActive(true);}
			}
			
			
        }
    }
	
	public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
			pressE.SetActive(false);
			doorLocked.SetActive(true);
            canOpen = false;
        }
    }
	
	public bool playerHasKey(){
		return true; // temporary true
		
		// if (Inventory.checkkeys has LockedKey)
		// {return true;}
		// else{ return false;}
	}
	

}