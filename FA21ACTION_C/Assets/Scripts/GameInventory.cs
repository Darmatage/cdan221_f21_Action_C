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
    public static bool key7bool = false;
    public static bool key8bool = false;
	
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
	public GameObject key7image;
    public GameObject key8image;
	
    //public GameObject cookiesText;


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
		key7image.SetActive(false);
        key8image.SetActive(false);

        InventoryDisplay();
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
		if (key7bool == true) { key7image.SetActive(true); } else { key7image.SetActive(false); }
		if (key8bool == true) { key8image.SetActive(true); } else { key8image.SetActive(false); }


        //Text coinTextB = coinText.GetComponent<Text>();
        //coinTextB.text = ("COINS: " + coins);
    }

    public void InventoryAdd(string item)
    {
        string foundItemName = item;
        if (foundItemName == "flashlight") { flashlightbool = true; }
        else if (foundItemName == "cookie") { cookiesbool = true; }
        else if (foundItemName == "soda") { sodabool = true; }
        else if (foundItemName == "donut") { donutsbool = true; }
        else if (foundItemName == "key1") { key1bool = true; }
		else if (foundItemName == "key2") { key2bool = true; }
        else if (foundItemName == "key3") { key3bool = true; }
		else if (foundItemName == "key4") { key4bool = true; }
        else if (foundItemName == "key5") { key5bool = true; }
		else if (foundItemName == "key6") { key6bool = true; }
        else if (foundItemName == "key7") { key7bool = true; }
		else if (foundItemName == "key8") { key8bool = true; }
        InventoryDisplay();
    }

    public void InventoryRemove(string item)
    {
        string itemRemove = item;
        //if (itemRemove == "flashlight") { flashlightbool = false; }
        if (itemRemove == "cookie") { cookiesbool = false; }
        else if (itemRemove == "soda") { sodabool = false; }
        else if (itemRemove == "donut") { donutsbool = false; }
        InventoryDisplay();
    }

    //public void CoinChange(int amount)
    //{
    //    coins += amount;
    //    InventoryDisplay();
    //}
	
	public void eatFood(){
		
		
		
	}
	
	
}
