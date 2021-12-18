using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameInventory : MonoBehaviour
{

    //12 Inventory Items:
    public GameObject InventoryMenu;

    public static bool flashlightbool = false; // flashlight 32 x 32
    public static bool cookiesbool = false; // cookies
    public static bool sodabool = false; // soda
    public static bool donutsbool = false; // donuts	

    public static bool key1bool = false; // floorkeys
    public static bool key2bool = false;
    public static bool key3bool = false;
    public static bool key4bool = false;
    public static bool key5bool = false;
    public static bool key6bool = false;
    //public static bool key7bool = false;
    //public static bool key8bool = false;
	
	//public static int cookies = 0;

	public static int cookies = 0;
	public static int soda = 0;
	public static int donuts = 0;	

	public Text cookiesText;
	public Text sodaText;
	public Text donutsText;	


    public GameObject flashlightimage;
    public GameObject cookiesimage;
    public GameObject sodaimage;
    public GameObject donutsimage;
    public GameObject key1image;
	public GameObject key2image;
    public GameObject key3image;
    public GameObject key4image;
    public GameObject key5image;
    public GameObject key6image;
	//public GameObject key7image;
    //public GameObject key8image;
	
	private GameHandler gameHandler;
	private GameObject player;
	public bool gotFlashlight = false;

	public int CookieHealthBoost=5;
	public int DonutSuperHealthBoost=10;
	public float SodaSpeedBoost=2.0f;

    void Start()
    {
        InventoryMenu.SetActive(true);
        flashlightimage.SetActive(false);
        cookiesimage.SetActive(false);
        sodaimage.SetActive(false);
        key1image.SetActive(false);
        key2image.SetActive(false);
	    key3image.SetActive(false);
        key4image.SetActive(false);
        key5image.SetActive(false);
        key6image.SetActive(false);	
		//key7image.SetActive(false);
        //key8image.SetActive(false);

        InventoryDisplay();
		
		if (GameObject.FindWithTag ("GameHandler") != null) {
			gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
		}
		
		if (GameObject.FindWithTag ("Player") != null) {
			player = GameObject.FindWithTag ("Player");
		}
    }

    void InventoryDisplay()
    {
        if (flashlightbool == true) { flashlightimage.SetActive(true); } else { flashlightimage.SetActive(false); }
        if (cookiesbool == true) { cookiesimage.SetActive(true); } else { cookiesimage.SetActive(false); }
        if (sodabool == true) { sodaimage.SetActive(true); } else { sodaimage.SetActive(false); }
        if (donutsbool == true) { donutsimage.SetActive(true); } else { donutsimage.SetActive(false); }
        if (key1bool == true) { key1image.SetActive(true); } else { key1image.SetActive(false); }
        if (key2bool == true) { key2image.SetActive(true); } else { key2image.SetActive(false); }
		if (key3bool == true) { key3image.SetActive(true); } else { key3image.SetActive(false); }
		if (key4bool == true) { key4image.SetActive(true); } else { key4image.SetActive(false); }
		if (key5bool == true) { key5image.SetActive(true); } else { key5image.SetActive(false); }
        if (key6bool == true) { key6image.SetActive(true); } else { key6image.SetActive(false); }
		//if (key7bool == true) { key7image.SetActive(true); } else { key7image.SetActive(false); }
		//if (key8bool == true) { key8image.SetActive(true); } else { key8image.SetActive(false); }


        Text cookiesTextB = cookiesText.GetComponent<Text>();
        cookiesTextB.text = ("" + cookies);
		
		Text sodaTextB = sodaText.GetComponent<Text>();
        sodaTextB.text = ("" + soda);
		
		Text donutsTextB = donutsText.GetComponent<Text>();
        donutsTextB.text = ("" + donuts);
    }

    public void InventoryAdd(string item)
    {
        string foundItemName = item;
        if (foundItemName == "flashlight") { flashlightbool = true; }
        else if (foundItemName == "cookie") { cookiesbool = true; cookies+=1;}
        else if (foundItemName == "soda") { sodabool = true;  soda+=1;}
        else if (foundItemName == "donut") { donutsbool = true; donuts+=1;}
        else if (foundItemName == "key1") { key1bool = true; }
		else if (foundItemName == "key2") { key2bool = true; }
        else if (foundItemName == "key3") { key3bool = true; }
		else if (foundItemName == "key4") { key4bool = true; }
        else if (foundItemName == "key5") { key5bool = true; }
		else if (foundItemName == "key6") { key6bool = true; }
        //else if (foundItemName == "key7") { key7bool = true; }
		//else if (foundItemName == "key8") { key8bool = true; }
        InventoryDisplay();
    }

    public void InventoryRemove(string item)
    {
        string itemRemove = item;
        //if (itemRemove == "flashlight") { flashlightbool = false; }
        if (itemRemove == "cookie") { 
			cookies-=1;
			if (cookies <= 0){
				cookiesbool = false; 
				cookies=0;
			}
		}
        else if (itemRemove == "soda") { 
			soda-=1;
			if (soda <= 0){
				sodabool = false; 
				soda=0;
			}
		}
        else if (itemRemove == "donut") {
			donuts-=1;
			if (donuts <= 0){
				donutsbool = false; 
				donuts=0;
			}
		}
        InventoryDisplay();
    }

    //public void CoinChange(int amount)
    //{
    //    coins += amount;
    //    InventoryDisplay();
    //}
	
	public void eatCookie(){
		gameHandler.playerGetHit(CookieHealthBoost * -1);
		InventoryRemove("cookie");
	}
		
	public void eatSoda(){	
		player.GetComponent<PlayerMoveAround>().SpeedBoost(SodaSpeedBoost);
		InventoryRemove("soda");
	}
	
	public void eatDonut(){
		gameHandler.playerGetHit(DonutSuperHealthBoost * -1);
		InventoryRemove("donut");
	}

	public void equipFlashlight(){	
		if (gotFlashlight==false){
			player.GetComponent<PlayerFlashlight>().HoldFlashlight();
			gotFlashlight=true;
		}
		else if (gotFlashlight==true){
			player.GetComponent<PlayerFlashlight>().DropFlashlight();
			gotFlashlight=false;
		}
	}
	
	public bool checkKeys(string LockedKey){
		if (LockedKey == "key1"){ if (key1bool){return true;} else {return false;}}
		else if (LockedKey == "key2"){ if (key2bool){return true;} else {return false;}}
		else if (LockedKey == "key3"){ if (key3bool){return true;} else {return false;}}
		else if (LockedKey == "key4"){ if (key4bool){return true;} else {return false;}}
		else if (LockedKey == "key5"){ if (key5bool){return true;} else {return false;}}
		else if (LockedKey == "key6"){ if (key6bool){return true;} else {return false;}}
		else {return false;}
	}
	
}
