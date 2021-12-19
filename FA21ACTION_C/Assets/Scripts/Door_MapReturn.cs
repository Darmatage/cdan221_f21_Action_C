using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_MapReturn : MonoBehaviour {

	public string NextLevel = "MainMenu";
	private string thisLevel;
	private Vector2 doorReturn;       // load with location for player when they return
	public float offsetY = -2f;        // distance in front / below of the door for the player to spawn
      
	void Start() {
		thisLevel = SceneManager.GetActiveScene().name;
		doorReturn = new Vector2(transform.position.x, (transform.position.y + offsetY));
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			GameHandler_PlayerReturn.lastDoorPosition = doorReturn;
			GameHandler_PlayerReturn.lastMap = thisLevel;
			SceneManager.LoadScene (NextLevel);
		}
	}
}