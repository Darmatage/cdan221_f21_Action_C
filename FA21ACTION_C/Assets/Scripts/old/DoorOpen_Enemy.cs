using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen_Enemy : MonoBehaviour
{

    public string NextLevel = "DormRoomScene_Enemy";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextLevel);
        }
    }

}