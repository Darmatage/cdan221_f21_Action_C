
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen_MainLevel : MonoBehaviour
{

    public string NextLevel = "LevelScene_1";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextLevel);
        }
    }

}