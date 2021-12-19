using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public string NextLevel;
	public GameObject pressE;
	public GameObject doorLocked;
	public GameObject doorUnlocked;
	
	private bool canOpen = false;
	public bool isLocked = false;
	public bool isNonDoor = false;
	
	public string LockedKey;
	private GameInventory gameINV;
	
	//map return content
	private string thisLevel;
	private Vector2 doorReturn;       // load with location for player when they return
	public float offsetY = 0;        // distance in front / below of the door for the player to spawn
    public float offsetX = -2f; 
	public bool isHallwayDoor = false;
	
	void Start() {	
		pressE.SetActive(false);
		doorLocked.SetActive(false);
		doorUnlocked.SetActive(false);
		
		if (GameObject.FindWithTag("GameHandler") != null){
			gameINV = GameObject.FindWithTag("GameHandler").GetComponent<GameInventory>();
		}
		
		//Map_Return content
		if (isHallwayDoor == true){
			thisLevel = SceneManager.GetActiveScene().name;
			doorReturn = new Vector2((transform.position.x + offsetX), (transform.position.y + offsetY));
		}
	}

	public void Update(){
		if ((Input.GetKeyDown("e"))&&(canOpen)){
			SceneManager.LoadScene(NextLevel);
			
			//Map_Return content
			if (isHallwayDoor == true){
				GameHandler_PlayerReturn.lastDoorPosition = doorReturn;
				GameHandler_PlayerReturn.lastMap = thisLevel;
				Debug.Log("doorReturn: " + doorReturn);
				Debug.Log("thisLevel: " + thisLevel);
			}
		}
	}
	
	public void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
				
			if ((isLocked == false)&&(isNonDoor==false)){
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
		if (gameINV.checkKeys(LockedKey)){return true;}
		else{ return false;}
	}

}

// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class Door : MonoBehaviour{

    // public string NextLevel = "DormRoomScene";
	// public GameObject pressE;
	// public GameObject doorLocked;
	// public GameObject doorUnlocked;
	
	// private bool canOpen = false;
	// public bool isLocked = false;
	
	// public string LockedKey;
	// private GameInventory gameINV;
	

	// void Start(){
		// pressE.SetActive(false);
		// doorLocked.SetActive(false);
		// doorUnlocked.SetActive(false);
		
		// if (GameObject.FindWithTag("GameHandler") != null){
			// gameINV = GameObject.FindWithTag("GameHandler").GetComponent<GameInventory>();
		// }
	// } 

	// public void Update(){
		// if ((Input.GetKeyDown("e"))&&(canOpen)){
			// SceneManager.LoadScene(NextLevel);
		// }
	// }


    // public void OnTriggerStay2D(Collider2D other){
        // if (other.gameObject.tag == "Player"){
				
			// if (isLocked == false){
				// pressE.SetActive(true);
				// canOpen = true;
			// }
			// else if (isLocked== true){
				// if (PlayerHasKey()){
					// doorUnlocked.SetActive(true);
					// canOpen = true;
				// }
				// else {doorLocked.SetActive(true);}
			// }
        // }
    // }
	
	// public void OnTriggerExit2D(Collider2D other){
        // if (other.gameObject.tag == "Player"){
			// pressE.SetActive(false);
			// doorLocked.SetActive(false);
			// doorUnlocked.SetActive(false);
            // canOpen = false;
        // }
    // }
	
	// public bool PlayerHasKey(){
		// if (gameINV.checkKeys(LockedKey)){return true;}
		// else{ return false;}
	// }

// }