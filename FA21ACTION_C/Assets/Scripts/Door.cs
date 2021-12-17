
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string NextLevel = "DormRoomScene";
	public GameObject pressE;
	public bool canOpen = false;

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
			pressE.SetActive(true);
            canOpen = true;
        }
    }
	
	public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
			pressE.SetActive(false);
            canOpen = false;
        }
    }

}