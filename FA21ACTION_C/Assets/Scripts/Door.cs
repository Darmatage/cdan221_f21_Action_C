
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour{

    public string NextLevel = "DormRoomScene";
	public GameObject pressE;
	public GameObject doorLocked;
	public GameObject doorUnlocked;
	
	private bool canOpen = false;
	public bool isLocked = false;
	
	public string LockedKey;
	private GameInventory gameINV;
	

	void Start(){
		pressE.SetActive(false);
		doorLocked.SetActive(false);
		doorUnlocked.SetActive(false);
		
		if (GameObject.FindWithTag("GameHandler") != null){
			gameINV = GameObject.FindWithTag("GameHandler").GetComponent<GameInventory>();
		}
	} 

	public void Update(){
		if ((Input.GetKeyDown("e"))&&(canOpen)){
			SceneManager.LoadScene(NextLevel);
		}
	}


    public void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
				
			if (isLocked == false){
				pressE.SetActive(true);
				canOpen = true;
			}
			else if (isLocked== true){
				if (PlayerHasKey()){
					doorUnlocked.SetActive(true);
					canOpen = true;
				}
				else {doorLocked.SetActive(true);}
			}
        }
    }
	
	public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
			pressE.SetActive(false);
			doorLocked.SetActive(false);
			doorUnlocked.SetActive(false);
            canOpen = false;
        }
    }
	
	public bool PlayerHasKey(){
		//return true; // temporary true
		if (gameINV.checkKeys(LockedKey)){return true;}
		else{ return false;}
	}

}