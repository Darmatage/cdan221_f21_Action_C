using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameHandler : MonoBehaviour {

	private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public GameObject healthText;

	public GameObject fadeBlack;
	private bool timeToFadeOut = false;
	private bool timeToFadeIn = false;
	private float fadeAlpha = 0f;
	private string sceneName; 

    //public static int gotTokens = 0;
    //public GameObject tokensText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public AudioMixer mixer;
        public static float volumeLevel = 1.0f;
        private Slider sliderVolumeCtrl;


	void Awake (){
		SetLevel (volumeLevel);
		GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
		if (sliderTemp != null){
			sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
			sliderVolumeCtrl.value = volumeLevel;
		}
		sceneName = SceneManager.GetActiveScene().name; 
		if ((sceneName == "EndLose") || (sceneName == "End_Win")){
			fadeBlack.GetComponent<Renderer>().material.color = new Color(2.55f, 2.55f, 2.55f, 1f);
			//StartCoroutine(FadedIn());
		}
		else {
			fadeBlack.GetComponent<Renderer>().material.color = new Color(2.55f, 2.55f, 2.55f, 0f);
		}
	}

	void Start(){
		pauseMenuUI.SetActive(false);
		fadeBlack.SetActive(false);
		player = GameObject.FindWithTag("Player");
		if (sceneName=="MainMenu"){
			playerHealth = StartPlayerHealth;
		}
		updateStatsDisplay();       
	}

	void Update (){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (GameisPaused){
				Resume();
			}
			else{
				Pause();
			}
		}
	}

	void FixedUpdate(){
		if (timeToFadeOut){
			fadeAlpha += 0.005f;
			fadeBlack.GetComponent<Renderer>().material.color = new Color(2.55f, 2.55f, 2.55f, fadeAlpha);
			if (fadeAlpha >= 1f){fadeAlpha=1f;}
		}
		else if (timeToFadeIn){
			fadeAlpha -= 0.005f;
			fadeBlack.GetComponent<Renderer>().material.color = new Color(2.55f, 2.55f, 2.55f, fadeAlpha);
			if (fadeAlpha <= 0f){fadeAlpha=0f;}
		}
	}

	void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameisPaused = true;
	}

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void SetLevel (float sliderValue){
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
        } 


      // public void playerGetTokens(int newTokens){
            // gotTokens += newTokens;
            // updateStatsDisplay();
      // }

	public void playerGetHit(int damage){
		if (isDefending == false){
			playerHealth -= damage;
			if (playerHealth >= 0){
				updateStatsDisplay();
			}
			player.GetComponent<PlayerHurt>().playerHit();
		}
		if (playerHealth <= 0){
			playerHealth = 0;
			playerDies();
			fadeBlack.SetActive(true);
			timeToFadeOut = true;
		}
      }

	public void playerGetHeath(int healthBoost){
		playerHealth += healthBoost;
		updateStatsDisplay();
		
		if (playerHealth >= StartPlayerHealth + 20){
            playerHealth = StartPlayerHealth + 20;
        }
      }


      public void updateStatsDisplay(){
            Text healthTextTemp = healthText.GetComponent<Text>();
            healthTextTemp.text = "HEALTH: " + playerHealth;

            //Text tokensTextTemp = tokensText.GetComponent<Text>();
            //tokensTextTemp.text = "COOKIES: " + gotTokens;
      }

      public void playerDies(){
            player.GetComponent<PlayerHurt>().playerDead();
			player.GetComponent<PlayerMoveAround>().enabled = false;
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            //player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(4f);
			playerHealth = StartPlayerHealth;
            SceneManager.LoadScene("EndLose");
      }

      public void StartGame() {
            SceneManager.LoadScene("DormRoomScene_Beginning");
      }

	public void RestartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
		playerHealth = StartPlayerHealth;
	}

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
	  
	  
	IEnumerator FadedIn(){
		fadeBlack.SetActive(true);
		yield return new WaitForSeconds(2f);
		timeToFadeIn = true;
		//Debug.Log("End Scene Fading");
	}
	  
}