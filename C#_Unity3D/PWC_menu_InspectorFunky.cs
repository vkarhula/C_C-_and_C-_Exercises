/*
*   PWC_menu_InspectorFunky.cs
*
*   Author Virpi Karhula
*   17.12.2013
*   Oulu Game Lab
*   Inspector Funky Project
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PWC_menu : MonoBehaviour {

	public GUISkin pwcSkin;
	
	public Texture2D locName1;
	public Texture2D locName2;
	public Texture2D locName3;
	public Texture2D locName4;
	public Texture2D map_reddot;
	public Texture2D map_greendot;
	public Texture2D map_greydot;
	
	// Button textures, limes and dark greens	
	
	public Texture2D buTexture1;
	public Texture2D buTexture2;
	public Texture2D buTexture3;
	public Texture2D buTexture4;
	public Texture2D buTexture1b;
	public Texture2D buTexture2b;
	public Texture2D buTexture3b;
	public Texture2D buTexture4b;
	// Background images, right side
	public Texture2D bgInventory;
	public Texture2D bgMap;
	public Texture2D bgMemo;
	public Texture2D bgOptions;

	public Texture2D backgroundBorder;	//bright border //private?
	public Texture2D backgroundImage;	
	public Texture2D detailBorder;
	public Texture2D detailImage;
	
	public Texture2D invIconActive;
	public Texture2D invIconNotActive;
	
	public Texture2D memoBu1;
	public Texture2D memoBu2;
	public Texture2D memoBu3;
	public Texture2D memoBu4;
	public Texture2D memoBuAct1;
	public Texture2D memoBuAct2;
	public Texture2D memoBuAct3;
	public Texture2D memoBuAct4;
	
	public Texture2D itemMilkDark;	//dark green buttons
	public Texture2D itemIceCubeDark;
	public Texture2D itemArmDark;
	public Texture2D itemAxeDark;
	public Texture2D itemCardDark;
	public Texture2D itemJellyDark;
	public Texture2D itemMittenDark;
	public Texture2D itemMilkBright;	// bright green buttons
	public Texture2D itemIceCubeBright;
	public Texture2D itemArmBright;
	public Texture2D itemAxeBright;
	public Texture2D itemCardBright;
	public Texture2D itemJellyBright;
	public Texture2D itemMittenBright;
	public Texture2D itemMilkBig;		// detailed images
	public Texture2D itemIceCubeBig;
	public Texture2D itemArmBig;
	public Texture2D itemAxeBig;
	public Texture2D itemCardBig;
	public Texture2D itemJellyBig;
	public Texture2D itemMittenBig;
	public Texture2D item1;		//textures for buttons #1-6 on screen upper location
	public Texture2D item2;
	public Texture2D item3;
	public Texture2D item4;
	public Texture2D item5;
	public Texture2D item6;
	public Texture2D itemBig;	//big item image on screen
	public Texture2D close;
	
	private bool showBox1Text;
	private bool showBox2Text;
	private bool showBox3Text;
	private bool showBox4Text;
	private string stringToEdit;
	private string stringToEdit2;
	private string stringToEdit3;
	private string stringToEdit3b;
	bool addText;
	bool addText3;
	
	int noteNbr;
	public string [] notes;
	
	public Texture[] buttonArr;  //animation
	public int animationButtonDelay;
	int curButtonTexture;
	int buttonAnimCounter;
	int buttonCounter;
	public enum Map {loc1 = 1, loc2 = 2, loc3 = 3, loc4 = 4}
	int currentLoc; //TODO use this for current location, continuing ...
	
	public bool loc1Visible;	// funky's office
	public bool loc2Visible;	// kitchen
	public bool loc3Visible;	// drinking minigame
	public bool loc4Visible;	// street
	public bool loc1Visited;
	public bool loc2Visited;
	public bool loc3Visited;
	public bool loc4Visited;
	public bool loc1Disabled;
	public bool loc2Disabled;
	public bool loc3Disabled;
	public bool loc4Disabled;
	public bool cs2Visited;
	public bool cs3Visited;
	
	public enum PWC {inventory = 1, map = 2, memo = 3, options = 4}
	int currentPWCpage;	
	
	public Texture [] gameObjTextures; //Textures
	public Texture [] invBtnBrightTextures;	//bright buttons
	public Texture [] invBtnDarkTextures;	//dark buttons
	public Texture [] invButtonTextures;  //current buttons are stored here
	public Texture [] invBigTextures;	// big images in inventory for detailed description of item
	public Texture [] memoButtonTextures;
	public int activeIndex;
	
	//categorized notes
	public string [] pSwoString;
	public string [] pScrString;
	public string [] pOraString;
	public string [] pLocString3;
	public string [] pInvString1;
	public string [] pPeoString2;
	
	public int pInvCounter;
	public int pScrCounter;
	public int pOraCounter;
	public int pSwoCounter;
	public int pLocCounter3;
	public int pPeoCounter2;
	
	DrinkingGame dgScript;
	KitchenScript kitScript;
	InterrogationScript inScript;
	
	conclusion conScript;
	public bool musicOnOff;
	
	bool guiEnabled;
	GameObject invObj;
	//this list is for storing gameobjects in inventory
	public List<GameObject> gameObjs = new List<GameObject>();
	//list for memo notes
	public List<string> memoNotes = new List<string>();
	public int selectedBox;
	int selectedBoxMemo;
	int count;
	int selGridX = 15 + 21;  //0;
	int selGridY = 42 + 21;	//0;
	public string [] names;
	public int itemCounter;
	bool invSelected;
	
	string activeObjectName;
	
	//
	public Vector2 scrollPosition;
	public string longString = "This is a long-ish string";
	
	public string [] ownNotes;
	int own;
	public Texture2D returnBtn;
	
	public Texture2D newGame;
	public Texture2D quitGame;
	public Texture2D soundOff;
	public Texture2D soundOn;
	public Texture2D soundButton;
	
	float he;
	
	//Screen.width and Screen.height variables
	int scWi;
	int scHe;
	// big buttons
	float bigBtnX;
	float bigBtnY;
	float bigBtnWi;
	float bigBtnHe;
	//content area
	float contX;
	float contY; 
	float contWi;
	float contHe;
	//close button
	float closeX;
	float closeY;
	float closeWi;
	float closeHe;
	//PWC map
	float empMapBgX;
	float empMapBgY;
	float empMapBgWi;
	float empMapBgHe;
	// area
	float areaX;
	float areaY;
	float areaWi;
	float areaHe;
	float gridMinWi;
	float gridMinHe;
	float bigImgX;
	float bigImgY;
	float bigImgWi;
	float bigImgHe;
	// map bg
	float mapBgX;
	float mapBgY;
	float mapBgWi;
	float mapBgHe;
	float loc1NameX;
	float loc1NameY;
	float loc1NameWi;
	float loc1NameHe;
	float loc1X;
	float loc1Y;
	float loc1Wi;
	float loc1He;
	float loc2NameX;
	float loc2NameY;
	float loc2NameWi;
	float loc2NameHe;
	float loc2X;
	float loc2Y;
	float loc2Wi;
	float loc2He;
	float loc3NameX;
	float loc3NameY;
	float loc3NameWi;
	float loc3NameHe;
	float loc3X;
	float loc3Y;
	float loc3Wi;
	float loc3He;
	float loc4NameX;
	float loc4NameY;
	float loc4NameWi;
	float loc4NameHe;
	float loc4X;
	float loc4Y;
	float loc4Wi;
	float loc4He;
	float memoBgX;
	float memoBgY;
	float memoBgWi;
	float memoBgHe;
	float meGridX;
	float meGridY;
	float meGridWi;
	float meGridHe;
	float opX;
	float opY;
	float opWi;
	float opHe;	
	
	// PWCSkin raahattu tähän muuttujaan

	// Use this for initialization
	void Start () {
		useGUILayout = false;
		guiEnabled = false;
		invSelected = false;
		musicOnOff = true;
		cs2Visited = false;
		cs3Visited = false;
		count = 0;
		itemCounter = 0;
		pInvCounter = 0;
		pPeoCounter2 = 0;
		pLocCounter3 = 0;
		pSwoCounter = 0;
		pScrCounter = 0;
		pOraCounter = 0;
		
		pInvString1 = new string[3];
		pPeoString2 = new string[7];
		pLocString3 = new string[3];
		pSwoString = new string[3];
		pScrString = new string[3];
		pOraString = new string[3];
		
		DontDestroyOnLoad(this.gameObject);
		//AddNote("I feel like Orange did not tell me everything...");
		AddPeoNote2("I feel like Orange did not tell me everything...");
		AddOraNote("I feel like Orange did not tell me everything...");
		float windowaspect = (float)Screen.width / (float)Screen.height;
		if(windowaspect == ((float)(4.0f/3.0f)))	{
			GameObject kcam = GameObject.FindGameObjectWithTag("KMainCamera");
			GameObject dcam = GameObject.FindGameObjectWithTag("DMainCamera");
			float targetaspect = 16.0f / 9.0f;
		    float scaleheight = windowaspect / targetaspect;
			if(PlayerPrefs.GetInt ("currentLevel") == 1) {
				Camera camera = kcam.GetComponent<Camera>();
			}
			else if(PlayerPrefs.GetInt ("currentLevel") == 2) {
				Camera camera = dcam.GetComponent<Camera>();
			}
		    if (scaleheight < 1.0f)
		    {  
		        Rect rect = camera.rect;
				rect.width = 1.0f;
		        rect.height = scaleheight;
		        rect.x = 0;
		        rect.y = (1.0f - scaleheight) / 2.0f;
		        camera.rect = rect;
		    }
		    else
		    {
		        float scalewidth = 1.0f / scaleheight;
		        Rect rect = camera.rect;
		        rect.width = scalewidth;
		        rect.height = 1.0f;
		        rect.x = (1.0f - scalewidth) / 2.0f;
		        rect.y = 0;
		        camera.rect = rect;
		    }	
		}
		
		loc1Visible = true;
		//Kitchen/Trashcan.js -> update(): loc2Visible = true;
		loc2Visible = false;  //(for testing: true)
		// drinking_game.js -> doFailDown(): loc3Visible = true; 2x eri paikassa 
		loc3Visible = false;  //(for testing: true)
		// IrterrogationScript.js -> update(): loc4Visible = true;
		loc4Visible = false;  //(for testing: true)
		loc1Visited = true;
		loc2Visited = false;
		// CS3Scripts.js -> sceneMoveEnabled(): loc3Visited = true; //TODO:this is not working properly
		loc3Visited = false;
		loc4Visited = false;
		loc1Disabled = false; //true after the clue been found
		loc2Disabled = true;
		loc3Disabled = true;
		loc4Disabled = true;
		currentPWCpage = (int)PWC.inventory; 	// Starting page
		backgroundImage = bgInventory;	
		//TODO:vk calculate values of Screen.width*0.5f to variables
		
		memoButtonTextures = new Texture[4];
		memoButtonTextures[0] = memoBuAct1;
		memoButtonTextures[1] = memoBu2;
		memoButtonTextures[2] = memoBu3;
		memoButtonTextures[3] = memoBu4;
		activeIndex = 0;
		
		names = new string[7]; //count]; 7
		gameObjTextures = new Texture[6];  //count]; 6
		invBtnBrightTextures = new Texture[6];  //[count];
		invBtnDarkTextures = new Texture[6]; 
		invButtonTextures =  new Texture[6]; //used for GUI, either bright or dark
		invBigTextures = new Texture[6]; 
	
		selectedBoxMemo = 0;
		showBox1Text = true;
		showBox2Text = false;
		showBox3Text = false;
		showBox4Text = false;
		stringToEdit = "";
		stringToEdit2 = "";
		stringToEdit3 = "";
		stringToEdit3b = "";
		addText = false;
		addText3 = false;
		
		ownNotes = new string[5];
		setOwnNotesNull();
		own = 0;
		
		noteNbr = 0;
		
		curButtonTexture = 0;
		buttonCounter = 0;
		buttonAnimCounter = 0;
		animationButtonDelay = 2;
		currentLoc = (int)Map.loc1;
		
		selectedBox = 0;
		itemBig = bgInventory;  //in the beginning when nothing has been chosen
		setBasicTextures();
		activeObjectName = "";
		
		scrollPosition = Vector2.zero;
		soundButton = soundOff;
		
		setScreenValues();
	}

	void setOwnNotesNull(){
		for(int i = 0; i < ownNotes.Length; i++){
			//ownNotes[i] = null;
			ownNotes[i] = "";
		}
	}
	
	void setScreenValues(){
		he = Screen.height*(768-8)/768 /4;  //3 pixels up, 3 pixels down, 2 * 1 space
		scWi = Screen.width;
		scHe = Screen.height;
		// big buttons
		bigBtnX = Screen.width*5/1024;
		bigBtnY = Screen.height*5/768;
		bigBtnWi = Screen.width*193/1024;
		bigBtnHe = Screen.height*760/768;
		//content area
		contX = Screen.width * 202/1024;
		contY = 0; 
		contWi = Screen.width * (1024-202)/1024; 
		contHe = scHe;
		//close button
		closeX = Screen.width*(991-201)/1024;
		closeY = Screen.height*13/768;
		closeWi = Screen.width*19/1024;
		closeHe = Screen.height*19/768;
		//PWC map
		empMapBgX = Screen.width*15/1024;
		empMapBgY = Screen.height*174/768;
		empMapBgWi = Screen.width*785/1024;
		empMapBgHe = Screen.height*546/768;
		// area
		areaX =Screen.width*15/1024;
		areaY =Screen.height*42/768;
		areaWi =Screen.width*785/1024;
		areaHe = Screen.height*131/768;
		gridMinWi = Screen.width*131/1024;
		gridMinHe = Screen.height*131/768;
		bigImgX = Screen.width*15/1024;
		bigImgY = Screen.height*174/768;
		bigImgWi = Screen.width*785/1024;
		bigImgHe = Screen.height*546/768;
		// map bg
		mapBgX = Screen.width*10/1024;
		mapBgY = Screen.height*42/768;
		mapBgWi = Screen.width*799/1024;
		mapBgHe = Screen.height*696/768;
		loc1NameX = Screen.width*(0.4f-0.02f);
		loc1NameY = Screen.height*(0.35f-0.08f);
		loc1NameWi = 128;
		loc1NameHe = 48;
		loc1X = Screen.width * 0.4f;
		loc1Y = Screen.height * 0.35f;
		loc1Wi = Screen.width * 0.06f;
		loc1He = Screen.height * 0.08f;
		loc2NameX = Screen.width*(0.5f-0.02f);
		loc2NameY = Screen.height*(0.6f-0.08f);
		loc2NameWi = 128;
		loc2NameHe = 48;
		loc2X = Screen.width * 0.5f;
		loc2Y = Screen.height * 0.6f;
		loc2Wi = Screen.width * 0.06f;
		loc2He = Screen.height * 0.08f;
		loc3NameX = Screen.width*(0.2f-0.02f);
		loc3NameY = Screen.height*(0.2f-0.08f);
		loc3NameWi = 128;
		loc3NameHe = 48;
		loc3X = Screen.width * 0.2f;
		loc3Y = Screen.height * 0.2f;
		loc3Wi = Screen.width * 0.06f;
		loc3He = Screen.height * 0.08f;
		loc4NameX = Screen.width*(0.15f-0.02f);
		loc4NameY = Screen.height*(0.7f-0.08f);
		loc4NameWi = 128;
		loc4NameHe = 48;	
		loc4X = Screen.width * 0.15f;
		loc4Y = Screen.height * 0.7f;
		loc4Wi = Screen.width * 0.06f;
		loc4He = Screen.height * 0.08f;

		memoBgX = Screen.width*15/1024;
		memoBgY = Screen.height*103/768;
		memoBgWi = Screen.width*786/1024;
		memoBgHe = Screen.height*619/768;
		meGridX = Screen.width*15/1024;
		meGridY = Screen.height*39/768;
		meGridWi = Screen.width*786/1024;
		meGridHe = Screen.height*45/768;
		opX = Screen.width*15/1024;
		opY = Screen.height*103/768;
		opWi = Screen.width*786/1024;
		opHe = Screen.height*619/768;
	}	
	
	public void Revoke() {
		useGUILayout = true;
		guiEnabled = true;	
	}
	
	public void Disable() {
		/*float windowaspect = (float)Screen.width / (float)Screen.height;
		if(windowaspect == ((float)(4.0f/3.0f)))	{
			if(PlayerPrefs.GetInt("currentLevel") == 1) {
				GameObject kcam = GameObject.FindGameObjectWithTag("KCamera");
				kcam.camera.enabled = true;
			}
			else if(PlayerPrefs.GetInt("currentLevel") == 2) {
				GameObject dcam = GameObject.FindGameObjectWithTag("DCamera");
				dcam.camera.enabled = true;
			}
		}*/
		useGUILayout = false;
		guiEnabled = false;	
	}
	
	// Just to avoid null texture announcement
	public void setBasicTextures(){
		//int i = 0;
		// It makes the inventory selectiongrid code more easier when in tables there are 6 items
		for(int i = 0; i <= 5; i++){
			invBtnBrightTextures[i] = invIconActive;  
			invBtnDarkTextures[i] = invIconNotActive; 
			invButtonTextures[i] = invIconNotActive; //used for GUI, either bright or dark
			invBigTextures[i] = bgInventory; 	
		}	
	}
	
	public void RemoveObject(GameObject obj){
		gameObjs.Remove(obj);	
	}
	
	//at this function we add objects into the list
	public void AddObject(GameObject obj) {
		GameObject ice = new GameObject();
		//Remove Icecube from list if Axe is added
		if(obj.name.ToString() == "Axe"){
			int j = 0;
			foreach(GameObject gobj in gameObjs){ //causes error
				if(gobj.name.ToString().Equals("IceCube")){
					// If IceCube icon was selected earlier active and big image of ice cube is shown
					// change the big image to show the axe 
					if (invButtonTextures[j] == invBtnBrightTextures[j]){
						itemBig = itemAxeBig;	
						activeObjectName = "Axe";
					}
					ice = gobj;					
				}
				j++;
			}
			if(ice != null)
				RemoveObject(ice);
		}
		
		foreach(GameObject gobj in gameObjs) {
			Debug.Log(gobj.name);
			if(gobj.name == obj.name)
			{
				Debug.LogWarning("Duplicate found");
				Debug.Break();
			}
		}

		gameObjs.Add(obj);
		count = gameObjs.Count;
		setBasicTextures();
		// needs to be set to null, otherwise generates errors
		for(int j = 0; j < names.Length; j++){
			//names[j] = null;	
			names[j] = "";	
		}
		int i = 0;
		

		foreach(GameObject gobj in gameObjs) {
			Debug.Log("i = " + i);  //
			names[i] = gobj.name.ToString();
			gameObjTextures[i] = gobj.renderer.material.mainTexture;  //Textures
			if(gobj.name.ToString() == "IceCube"){
				invBtnBrightTextures[i] = itemIceCubeBright;
				invBtnDarkTextures[i] = itemIceCubeDark;
				invButtonTextures[i] = itemIceCubeDark;  //default is dark
				invBigTextures[i] = itemIceCubeBig;
			}
			if(gobj.name.ToString() == "Axe"){
				invBtnBrightTextures[i] = itemAxeBright;
				invBtnDarkTextures[i] = itemAxeDark;
				invButtonTextures[i] = itemAxeDark;
				invBigTextures[i] = itemAxeBig;
			}		
			if(gobj.name.ToString() == "Milk"){
				invBtnBrightTextures[i] = itemMilkBright;
				invBtnDarkTextures[i] = itemMilkDark;
				invButtonTextures[i] = itemMilkDark;
				invBigTextures[i] = itemMilkBig;
			}
			if(gobj.name.ToString() == "Jelly"){
				invBtnBrightTextures[i] = itemJellyBright;
				invBtnDarkTextures[i] = itemJellyDark;
				invButtonTextures[i] = itemJellyDark;
				invBigTextures[i] = itemJellyBig;
			}
			if(gobj.name.ToString() == "MannequinArm"){
				invBtnBrightTextures[i] = itemArmBright;
				invBtnDarkTextures[i] = itemArmDark;
				invButtonTextures[i] = itemArmDark;
				invBigTextures[i] = itemArmBig;
			}
			if(gobj.name.ToString() == "Mitten"){
				invBtnBrightTextures[i] = itemMittenBright;
				invBtnDarkTextures[i] = itemMittenDark;
				invButtonTextures[i] = itemMittenDark;
				invBigTextures[i] = itemMittenBig;
			}
			if(gobj.name.ToString() == "Ticket"){
				invBtnBrightTextures[i] = itemCardBright;
				invBtnDarkTextures[i] = itemCardDark;
				invButtonTextures[i] = itemCardDark;
				invBigTextures[i] = itemCardBig;
			}
			i++;
		}
		//If known inventory button was selected, show the button with bright border again
		if(activeObjectName != ""){
			int k = 0;
			foreach(GameObject gobj in gameObjs) {
				if(gobj.name.Equals(activeObjectName)){
					invButtonTextures[k] = invBtnBrightTextures[k];
					activeIndex = k;
				}
				k++;
			}
		}
		itemCounter++;
		//Debug.Log("ITEMS COUNT: "+itemCounter);
	}
	
	public void AddNote(string note) {
		memoNotes.Add(note);
		//Debug.Log("NOTES: "+memoNotes);	
	}
	
	public void AddInvNote1(string note) {
		pInvString1[pInvCounter] = note;
		pInvCounter++;
	}
	
	public void AddPeoNote2(string note) {
		pPeoString2[pPeoCounter2] = note;
		pPeoCounter2++;
	}
	
	public void AddLocNote3(string note) {
		pLocString3[pLocCounter3] = note;
		pLocCounter3++;
	}
	
	public void AddSwoNote(string note) {
		pSwoString[pSwoCounter] = note;
		pSwoCounter++;
	}
	
	public void AddScrNote(string note) {
		pScrString[pScrCounter] = note;
		pScrCounter++;
	}
	
	public void AddOraNote(string note) {
		pOraString[pOraCounter] = note;
		pOraCounter++;
	}
	
	public void PrintNotes(){
		noteNbr = memoNotes.Count;
		notes = new string[noteNbr];
		int y = 120 + 80;
		foreach(string str in memoNotes){
			GUI.Label(new Rect(30, y, 725, 120),str);
			//TODO check how long str is and how many rows it needs
			if(str.Length > 210){
				y += 130;
			} else if(str.Length > 140){
				y += 95;
			} else if(str.Length > 70){
				y += 70;
			} else {
				y += 40;
			}
		}
	}
	
	public void printInvNotes1(){
		for(int i = 0; i < pInvString1.Length; i++){
			GUI.Label(new Rect(40, 110+i*35, 725, 120), pInvString1[i]);  //height 120 too big
		}	
	}
	public void printPeoNotes2(){
		for(int i = 0; i < pPeoString2.Length; i++){
			GUI.Label(new Rect(40, 110+i*35, 725, 120), pPeoString2[i]);  //height 120 too big
		}	
	}
	public void printLocNotes3(){
		for(int i = 0; i < pLocString3.Length; i++){
			GUI.Label(new Rect(40, 110+i*35, 725, 120), pLocString3[i]);  //height 120 too big
		}	
	}
	
	void startDrinkingGame(){
		bool ticketFound = false;
		foreach(string str in names) {
			if(str == "Ticket")
				ticketFound = true;
		}
		if(ticketFound){
			if(PlayerPrefs.GetInt ("currentLevel") == 1) {
				loc1Disabled = true;
				Disable ();
				//GameObject kitchObj = GameObject.FindGameObjectWithTag("KMainCamera");
				//kitScript = (KitchenScript)kitchObj.GetComponent(typeof(KitchenScript));
				//kitScript.DestroyScene ();
				//Application.runInBackground = true;
				//Application.backgroundLoadingPriority = ThreadPriority.High;
				//Application.LoadLevelAsync("drinking_contest");
				Application.LoadLevelAsync("cut_scene2");
			}	
		}
	}
	
	void startStreets() {
		if(PlayerPrefs.GetInt ("currentLevel") == 2) {
			useGUILayout = false;
			guiEnabled = false;
			/*DrinkingGame drinkScript;
			GameObject anInvObj = GameObject.FindGameObjectWithTag("DMainCamera");
			drinkScript = (DrinkingGame)anInvObj.GetComponent(typeof(DrinkingGame));
			drinkScript.DestroyScene();*/
			Application.LoadLevelAsync("streets_interrogation");
		}
	}	
	
	void startConclusion() {
		if(PlayerPrefs.GetInt ("currentLevel") == 3 ) {
			useGUILayout = false;
			guiEnabled = false;
			Application.LoadLevelAsync("conclusion");
		}
	}
	
	void hidePWC(){
		if(PlayerPrefs.GetInt ("currentLevel") == 1) {
			Disable();
			GameObject dgObj = GameObject.FindGameObjectWithTag("KMainCamera");
			kitScript = (KitchenScript)dgObj.GetComponent(typeof(KitchenScript));
			kitScript.UnPause();
		}
		else if(PlayerPrefs.GetInt ("currentLevel") == 2) {
			Disable();
			GameObject dgObj = GameObject.FindGameObjectWithTag("DMainCamera");
			dgScript = (DrinkingGame)dgObj.GetComponent(typeof(DrinkingGame));
			dgScript.UnPause();
		}
		else if(PlayerPrefs.GetInt ("currentLevel") == 3) {
			useGUILayout = false;
			guiEnabled = false;
			GameObject dgObj = GameObject.FindGameObjectWithTag("SMainCamera");
			inScript = (InterrogationScript)dgObj.GetComponent(typeof(InterrogationScript));
			inScript.UnPause();
		}
		else if(PlayerPrefs.GetInt ("currentLevel") == 4) {
			useGUILayout = false;
			guiEnabled = false;
			GameObject dgObj = GameObject.FindGameObjectWithTag("cBackground");
			conclusion inScript;
			inScript = (conclusion)dgObj.GetComponent(typeof(conclusion));
			inScript.UnPause();
		}
	}
	
	public void onOffAudio(bool onOff){
		GameObject dgObj;
		if(PlayerPrefs.GetInt ("currentLevel") == 1) {
			//Disable();
			if(cs2Visited == false) {
				dgObj = GameObject.FindGameObjectWithTag("KMainCamera");
				kitScript = (KitchenScript)dgObj.GetComponent(typeof(KitchenScript));
				//kitScript.UnPause();
				//if(onOff == true){
				if(musicOnOff == true) {
					dgObj.audio.mute = false;
				} else {
					dgObj.audio.mute = true;
				}
			}
			else if(cs2Visited == true) {
				GameObject cs2Obj = GameObject.FindGameObjectWithTag("CS2MainCamera");
				CS2Script cs2Script = (CS2Script)cs2Obj.GetComponent(typeof(CS2Script));
				if(musicOnOff == true) {
					cs2Obj.audio.mute = false;
				} else {
					cs2Obj.audio.mute = true;
				}
			}
		}
		else if(PlayerPrefs.GetInt ("currentLevel") == 2) {
			//Disable();
			if(cs3Visited == false) {
				dgObj = GameObject.FindGameObjectWithTag("DMainCamera");
				dgScript = (DrinkingGame)dgObj.GetComponent(typeof(DrinkingGame));
				//dgScript.UnPause();
				//if(onOff == true){
				if(musicOnOff == true) {
					dgObj.audio.mute = false;
				} else {
					dgObj.audio.mute = true;
				}
			}
			else if(cs3Visited == true) {
				GameObject cs3Obj = GameObject.FindGameObjectWithTag("CS3MainCamera");
				CS3Script cs3Script = (CS3Script)cs3Obj.GetComponent(typeof(CS3Script));
				if(musicOnOff == true) {
					cs3Obj.audio.mute = false;
				} else {
					cs3Obj.audio.mute = true;
				}
			}
		}
		else if(PlayerPrefs.GetInt ("currentLevel") == 3) {
			//useGUILayout = false;
			//guiEnabled = false;
			dgObj = GameObject.FindGameObjectWithTag("SMainCamera");
			inScript = (InterrogationScript)dgObj.GetComponent(typeof(InterrogationScript));
			//inScript.UnPause();
			//if(onOff == true){
			if(musicOnOff == true) {
				dgObj.audio.mute = false;
			} else {
				dgObj.audio.mute = true;
			}		}
		else if (PlayerPrefs.GetInt ("currentLevel") == 4){
			dgObj = GameObject.FindGameObjectWithTag("cBackground");  //tag is missing -> background
			conScript = (conclusion)dgObj.GetComponent(typeof(conclusion));
			//if(onOff == true){
			if(musicOnOff == true) {
				dgObj.audio.mute = false;
			} else {
				dgObj.audio.mute = true;
			}
			dgObj = GameObject.FindGameObjectWithTag("Newspaper");
			conScript = (conclusion)dgObj.GetComponent(typeof(conclusion));
			//if(onOff == true){
			if(musicOnOff == true) {
				dgObj.audio.mute = false;
			} else {
				dgObj.audio.mute = true;
			}
		}
	}
	
	
	void NextButtonTexture(){
		
		// button getting only bigger
		curButtonTexture++;
		if(curButtonTexture >= buttonArr.Length-1) {
			curButtonTexture = 0;
		}
		
		/*
		// button getting bigger and smaller
		if(countUp == true){
			curButtonTexture++;
			if(curButtonTexture >= buttonArr.Length-1) {
				countUp = false;
				curButtonTexture = buttonArr.Length-1;
				curButtonTexture = 0;
			}
		} else if(countUp == false){
			curButtonTexture--;
			if(curButtonTexture <= 0) {
				countUp = true;
				curButtonTexture = 0;
			}
		}
		*/
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)) {
			hidePWC();
		}	
		// timer for animated map button
		buttonCounter++;
		//Debug.Log("animationButtonDelay = " + animationButtonDelay + " buttonCounter = " + buttonCounter + " curButtonTexture = " + curButtonTexture);
		if(buttonCounter >= animationButtonDelay) {
			buttonCounter = 0;
			NextButtonTexture();  
		}		
		//TODO:vk check if the screen size is changed and calculate Screen.width*0.5f values again
	}
	
	void OnGUI(){
		
		// Update screen size values only if changed
		// TODO check how to use with different display ratios 16:9, 16:10 and 4:3
		if(scWi != Screen.width || scHe !=Screen.height){
			setScreenValues();	
		}
		
		if(guiEnabled) {
			GUI.skin = pwcSkin;
			
			// BackgroundBorder image for PWC
			//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundBorder);
			GUI.DrawTexture(new Rect(0, 0, scWi, scHe), backgroundBorder);

			// BIG BUTTONS AREA
			
			//GUILayout.BeginArea(new Rect(Screen.width*5/1024, Screen.height*5/768, Screen.width*193/1024, Screen.height*760/768));
			GUILayout.BeginArea(new Rect(bigBtnX, bigBtnY, bigBtnWi, bigBtnHe));

				GUILayout.BeginVertical();
					// Select lime or dark green icons for buttons
					if(currentPWCpage == (int)PWC.inventory){
						GUILayout.Button(buTexture1, GUILayout.MaxHeight(he));
					} else {
						if(GUILayout.Button(buTexture1b, GUILayout.MaxHeight(he))){
							//TODO move to inventory page
							currentPWCpage = (int)PWC.inventory;
							backgroundImage = bgInventory;
						}
					}
					if(currentPWCpage == (int)PWC.map){
						GUILayout.Button(buTexture2, GUILayout.MaxHeight(he));
					} else {
						if(GUILayout.Button(buTexture2b, GUILayout.MaxHeight(he))){
							currentPWCpage = (int)PWC.map;
							backgroundImage = bgMap;
						}
					}
					if(currentPWCpage == (int)PWC.memo){
						GUILayout.Button(buTexture3, GUILayout.MaxHeight(he));
					} else {
						if(GUILayout.Button(buTexture3b, GUILayout.MaxHeight(he))){
							currentPWCpage = (int)PWC.memo;
							backgroundImage = bgMemo;
						}
					}
					if(currentPWCpage == (int)PWC.options){
						GUILayout.Button(buTexture4, GUILayout.MaxHeight(he));
					} else {
						if(GUILayout.Button(buTexture4b, GUILayout.MaxHeight(he))){
							currentPWCpage = (int)PWC.options;
							backgroundImage = bgOptions;
						}
					}	
				GUILayout.EndVertical();
			GUILayout.EndArea();
			
			// CONTENT AREA
			
			//GUILayout.BeginArea(new Rect(Screen.width * 202/1024, 0, Screen.width * (1024-202)/1024, Screen.height));
			GUILayout.BeginArea(new Rect(contX, contY, contWi, contHe));

				//if(GUI.Button(new Rect(Screen.width*(991-201)/1024, Screen.height*13/768, Screen.width*19/1024, Screen.height*19/768), close)){
				if(GUI.Button(new Rect(closeX, closeY, closeWi, closeHe), close)){
					hidePWC();
					//Debug.Log("close button pressed");
				}
			
				// PWC INVENTORY LAYOUT
				if(currentPWCpage == (int)PWC.inventory){
					// only if there is no picked objects
					//GUI.DrawTexture(new Rect(Screen.width*15/1024, Screen.height*174/768, Screen.width*785/1024,Screen.height*546/768), backgroundImage);
					GUI.DrawTexture(new Rect(empMapBgX, empMapBgY, empMapBgWi, empMapBgHe), backgroundImage);
				
					//draw the selection grid
					if(invButtonTextures != null) {
						//GUILayout.BeginArea(new Rect(Screen.width*15/1024, Screen.height*42/768, Screen.width*785/1024,Screen.height*131/768));
						GUILayout.BeginArea(new Rect(areaX, areaY, areaWi, areaHe));
						//selectedBox = GUILayout.SelectionGrid( activeIndex, invButtonTextures, 6, GUILayout.MinWidth(Screen.width*131/1024), GUILayout.MinHeight(Screen.height*131/768));
						selectedBox = GUILayout.SelectionGrid( activeIndex, invButtonTextures, 6, GUILayout.MinWidth(gridMinWi), GUILayout.MinHeight(gridMinHe));
						if(selectedBox == 0) {
							invButtonTextures[0] = invBtnBrightTextures[0];	//active
							invButtonTextures[1] = invBtnDarkTextures[1];
							invButtonTextures[2] = invBtnDarkTextures[2];
							invButtonTextures[3] = invBtnDarkTextures[3];
							invButtonTextures[4] = invBtnDarkTextures[4];
							invButtonTextures[5] = invBtnDarkTextures[5];
							itemBig = (Texture2D)invBigTextures[0];
							activeObjectName = names[0];
							activeIndex = 0;
							//Debug.Log ("ITEM 1 clicked " + activeObjectName);
						}
						else if(selectedBox == 1) {
							invButtonTextures[0] = invBtnDarkTextures[0];	
							invButtonTextures[1] = invBtnBrightTextures[1];
							invButtonTextures[2] = invBtnDarkTextures[2];
							invButtonTextures[3] = invBtnDarkTextures[3];
							invButtonTextures[4] = invBtnDarkTextures[4];
							invButtonTextures[5] = invBtnDarkTextures[5];
							itemBig = (Texture2D)invBigTextures[1];
							activeObjectName = names[1];
							activeIndex = 1;
							//Debug.Log ("ITEM 2 clicked ");
						}
						else if(selectedBox == 2) {
							invButtonTextures[0] = invBtnDarkTextures[0];	
							invButtonTextures[1] = invBtnDarkTextures[1];
							invButtonTextures[2] = invBtnBrightTextures[2];
							invButtonTextures[3] = invBtnDarkTextures[3];
							invButtonTextures[4] = invBtnDarkTextures[4];
							invButtonTextures[5] = invBtnDarkTextures[5];
							itemBig = (Texture2D)invBigTextures[2];
							activeObjectName = names[2];
							activeIndex = 2;
							//Debug.Log ("ITEM 3 clicked ");
						}
						else if(selectedBox == 3) {
							invButtonTextures[0] = invBtnDarkTextures[0];	
							invButtonTextures[1] = invBtnDarkTextures[1];
							invButtonTextures[2] = invBtnDarkTextures[2];
							invButtonTextures[3] = invBtnBrightTextures[3];
							invButtonTextures[4] = invBtnDarkTextures[4];
							invButtonTextures[5] = invBtnDarkTextures[5];
							itemBig = (Texture2D)invBigTextures[3];
							activeObjectName = names[3];
							activeIndex = 3;
							//Debug.Log ("ITEM 4 clicked ");
						}
						else if(selectedBox == 4) {
							invButtonTextures[0] = invBtnDarkTextures[0];	
							invButtonTextures[1] = invBtnDarkTextures[1];
							invButtonTextures[2] = invBtnDarkTextures[2];
							invButtonTextures[3] = invBtnDarkTextures[3];
							invButtonTextures[4] = invBtnBrightTextures[4];
							invButtonTextures[5] = invBtnDarkTextures[5];
							itemBig = (Texture2D)invBigTextures[4];
							activeObjectName = names[4];
							activeIndex = 4;
							//Debug.Log ("ITEM 5 clicked ");
						}
						else if(selectedBox == 5) {
							invButtonTextures[0] = invBtnDarkTextures[0];	
							invButtonTextures[1] = invBtnDarkTextures[1];
							invButtonTextures[2] = invBtnDarkTextures[2];
							invButtonTextures[3] = invBtnDarkTextures[3];
							invButtonTextures[4] = invBtnDarkTextures[4];
							invButtonTextures[5] = invBtnBrightTextures[5];
							itemBig = (Texture2D)invBigTextures[5];
							activeObjectName = names[5];
							activeIndex = 5;
							//Debug.Log ("ITEM 6 clicked ");
						}
					}
					GUILayout.EndArea(); // area for selection grid
					//GUI.DrawTexture(new Rect(Screen.width*15/1024, Screen.height*174/768, Screen.width*785/1024,Screen.height*546/768), itemBig);
					GUI.DrawTexture(new Rect(bigImgX, bigImgY, bigImgWi, bigImgHe), itemBig);
				}	
			
				// PWC MAP LAYOUT
				if(currentPWCpage == (int)PWC.map){
					//GUI.DrawTexture(new Rect(Screen.width*10/1024,Screen.height*42/768, Screen.width*799/1024, Screen.height*696/768), backgroundImage);
					GUI.DrawTexture(new Rect( mapBgX, mapBgY, mapBgWi, mapBgHe), backgroundImage);
					if(loc1Visible){
						//GUI.DrawTexture(new Rect(Screen.width*(0.4f-0.02f), Screen.height*(0.35f-0.08f), 128, 48), locName1);
						GUI.DrawTexture(new Rect( loc1NameX, loc1NameY, loc1NameWi, loc1NameHe), locName1);

						if(loc1Disabled == false){
							if (loc1Visited){	
								if(currentLoc == (int)Map.loc1){
									//if(GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.06f, Screen.height * 0.08f), buttonArr[curButtonTexture])){
									if(GUI.Button(new Rect(loc1X, loc1Y, loc1Wi, loc1He), buttonArr[curButtonTexture])){
										print ("loc1 visited");
										print ("enum = " + (int)PWC.inventory);		
										//TODO update current location to specific scene before coming here
										currentLoc = (int)Map.loc1;
										//TODO move to location1
									}
								} else {
									//if(GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.06f, Screen.height * 0.08f), map_greendot)){
									if(GUI.Button(new Rect(loc1X, loc1Y, loc1Wi, loc1He), map_greendot)){
										currentLoc = (int)Map.loc1;
										//TODO move to location1
									}
								}
							} else {
								if(GUI.Button(new Rect(loc1X, loc1Y, loc1Wi, loc1He), map_reddot)){
									loc1Visited = true;
									print ("loc1 not visited");
									currentLoc = (int)Map.loc1;
									//TODO move to location1
								}
							}	
						// there is no entry back to this scene after the clue has been got
						// 
						} else if(loc1Disabled == true) {
							//GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.06f, Screen.height * 0.08f), map_greendot);
							//GUI.Button(new Rect(loc1X, loc1Y, loc1Wi, loc1He), map_greendot);
							GUI.Button(new Rect(loc1X, loc1Y, loc1Wi, loc1He), map_greydot);
							//Debug.Log("loc1Disabled = true");
						}
					}


					if(loc2Visible){
						//GUI.DrawTexture(new Rect(Screen.width*(0.5f-0.02f), Screen.height*(0.6f-0.08f), 128, 48), locName2);
						GUI.DrawTexture(new Rect(loc2NameX, loc2NameY, loc2NameWi, loc2NameHe), locName2);
						// TODO set loc2Disabled = true after drinking game clue has been found
						if(loc2Disabled == false){
							if (loc2Visited){
								if(currentLoc == (int)Map.loc2)
								{
									//if(GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.6f, Screen.width * 0.06f, Screen.height * 0.08f), buttonArr[curButtonTexture])) 
									if(GUI.Button(new Rect(loc2X, loc2Y, loc2Wi, loc2He), buttonArr[curButtonTexture])) 
									{
										//GUI.DrawTexture(new Rect(100,100,200,200),bgMap);	//
										startDrinkingGame();
										//startStreets ();
										currentLoc = (int)Map.loc2;
									}
								} else {
									if(GUI.Button(new Rect(loc2X, loc2Y, loc2Wi, loc2He), map_greendot)) {
										//GUI.DrawTexture(new Rect(100,100,200,200),bgMap);	//
										startDrinkingGame();
										//startStreets ();
										currentLoc = (int)Map.loc2;
									}
								}
							} else {
								if(GUI.Button(new Rect(loc2X, loc2Y, loc2Wi, loc2He), map_reddot)) {
									loc2Visited = true;
									//GUI.DrawTexture(new Rect(100,100,200,200),bgMap);	//
									startDrinkingGame();
									//startStreets ();
									//GUI.DrawTexture(new Rect(100,100,200,200),bgMap);	//
									currentLoc = (int)Map.loc2;
								}
							}
						} else if(loc2Disabled == true) {
							//GUI.Button(new Rect(loc2X, loc2Y, loc2Wi, loc2He), map_greendot);
							GUI.Button(new Rect(loc2X, loc2Y, loc2Wi, loc2He), map_greydot);
						}
					}

					if(loc3Visible){
						//GUI.DrawTexture(new Rect(Screen.width*(0.2f-0.02f), Screen.height*(0.2f-0.08f), 128, 48), locName3);
						GUI.DrawTexture(new Rect(loc3NameX, loc3NameY, loc3NameWi, loc3NameHe), locName3);
						// TODO set loc3Disabled = true after interrogation clue has been got
						if(loc3Disabled == false){
							currentLoc = (int)Map.loc3;
							if (loc3Visited){
								if(currentLoc == (int)Map.loc3){
									//if(GUI.Button(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.06f, Screen.height * 0.08f),buttonArr[curButtonTexture] )){
									if(GUI.Button(new Rect(loc3X, loc3Y, loc3Wi, loc3He),buttonArr[curButtonTexture] )){
										//startStreets();
										currentLoc = (int)Map.loc3;
										//TODO go to location3
									}
								} else {
									if(GUI.Button(new Rect(loc3X, loc3Y, loc3Wi, loc3He),map_greendot )){
										//startStreets();
										currentLoc = (int)Map.loc3;
										//TODO go to location3
									}
								}
						
							} else {
								if(GUI.Button(new Rect(loc3X, loc3Y, loc3Wi, loc3He), map_reddot)){
									//startStreets();
									loc3Visited = true;
									currentLoc = (int)Map.loc3;
									//TODO go to location3
								}
							}
						} else if (loc3Disabled == true){
							//GUI.Button(new Rect(loc3X, loc3Y, loc3Wi, loc3He),map_greendot );
							GUI.Button(new Rect(loc3X, loc3Y, loc3Wi, loc3He),map_greydot);
						}
					
					}
	
				

				
					if(loc4Visible){
						//GUI.DrawTexture(new Rect(Screen.width*(0.15f-0.02f), Screen.height*(0.7f-0.08f), 128, 48), locName4);
						GUI.DrawTexture(new Rect(loc4NameX, loc4NameY, loc4NameWi, loc4NameHe), locName4);
						if(loc4Disabled == false) {
							if (loc4Visited){
								if(currentLoc == (int)Map.loc4){
									//if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.7f, Screen.width * 0.06f, Screen.height * 0.08f), buttonArr[curButtonTexture])){
									if(GUI.Button(new Rect(loc4X, loc4Y, loc4Wi, loc4He), buttonArr[curButtonTexture])){
											currentLoc = (int)Map.loc4;
										// TODO go to location4
										startConclusion();
									}
								} else {
									if(GUI.Button(new Rect(loc4X, loc4Y, loc4Wi, loc4He), map_greendot)){
										currentLoc = (int)Map.loc4;
										// TODO go to location4
										startConclusion();
									}
								}
							} else {
								if(GUI.Button(new Rect(loc4X, loc4Y, loc4Wi, loc4He), map_reddot)){
									loc4Visited = true;
									currentLoc = (int)Map.loc4;
									// TODO go to location4
									startConclusion();
								}
							}
						} else if (loc4Disabled == true){
							//GUI.Button(new Rect(loc4X, loc4Y, loc4Wi, loc4He),map_greendot );
							GUI.Button(new Rect(loc4X, loc4Y, loc4Wi, loc4He),map_greydot);
						}
					}
				}

				// PWC MEMO LAYOUT
				if(currentPWCpage == (int)PWC.memo){

					//GUI.DrawTexture(new Rect(Screen.width*15/1024,Screen.height*103/768, Screen.width*786/1024, Screen.height*619/768), backgroundImage);
					GUI.DrawTexture(new Rect(memoBgX, memoBgY, memoBgWi, memoBgHe), backgroundImage);

					//selectedBoxMemo = GUI.SelectionGrid(new Rect(Screen.width*15/1024, Screen.height*39/768, Screen.width*786/1024, Screen.height*45/768),99, memoButtonTextures, 4);
					selectedBoxMemo = GUI.SelectionGrid(new Rect(meGridX, meGridY, meGridWi, meGridHe),99, memoButtonTextures, 4);
					if (selectedBoxMemo == 0){
						memoButtonTextures[0] = memoBuAct1;
						memoButtonTextures[1] = memoBu2;
						memoButtonTextures[2] = memoBu3;
						memoButtonTextures[3] = memoBu4;
						showBox1Text = true;
						showBox2Text = false;
						showBox3Text = false;
						showBox4Text = false;
						//Debug.Log ("Button 1 clicked ");
					} else if(selectedBoxMemo == 1) {
						memoButtonTextures[0] = memoBu1;
						memoButtonTextures[1] = memoBuAct2;
						memoButtonTextures[2] = memoBu3;
						memoButtonTextures[3] = memoBu4;
						showBox1Text = false;
						showBox2Text = true;
						showBox3Text = false;
						showBox4Text = false;
						//Debug.Log ("Button 2 clicked ");
					} else if(selectedBoxMemo == 2) {
						memoButtonTextures[0] = memoBu1;
						memoButtonTextures[1] = memoBu2;
						memoButtonTextures[2] = memoBuAct3;
						memoButtonTextures[3] = memoBu4;
						showBox1Text = false;
						showBox2Text = false;
						showBox3Text = true;
						showBox4Text = false;
					
						//TODO for testing before clues have their trigger place ready
						/*AddNote("some text");
						AddNote("another text");
						AddNote("123456789 123456789 123456789 123456789 123456789 123456789 123456789 1 3 5 7 9 123456789 123456789 123456789 ");
						AddNote("123456789 123456789 123456789 123456789 123456789 123456789 123456789 1 3 5 7 9 123456789 123456789 123456789 " +
							"123456789 123456789 123456789 123456789 123456789 123456789 123456789 1 3 5 7 9 123456789 123456789 123456789 ");
						AddNote("another text");
						*/
						//Debug.Log ("Button 3 clicked ");
					} else if(selectedBoxMemo == 3) {
						memoButtonTextures[0] = memoBu1;
						memoButtonTextures[1] = memoBu2;
						memoButtonTextures[2] = memoBu3;
						memoButtonTextures[3] = memoBuAct4;
						showBox1Text = false;
						showBox2Text = false;
						showBox3Text = false;
						showBox4Text = true;
						//Debug.Log ("Button 4 clicked ");
					}
				
					if(showBox1Text == true){
						printInvNotes1();		
					} else if(showBox2Text == true){
						printPeoNotes2();
					} else if(showBox3Text == true){
						printLocNotes3();
					} else if(showBox4Text == true){
						//editoitava tekstialue
						GUI.Label(new Rect(30, 500, 650, 40), "You can add your own notes:");
						stringToEdit3 = GUI.TextField(new Rect(30, 550, 650, 40), stringToEdit3, 70);
						if(GUI.Button(new Rect(750, 550, 90, 35), returnBtn)){
							addText3 = true;
							if(own < ownNotes.Length){
								ownNotes[own] = stringToEdit3;
								stringToEdit3 = "";
								own++;
							} else {
								stringToEdit3 = "No space for extra notes, sorry.";
							}
						}
						//int x = 0;
						//int rHe = 0;
						int prevHe = 70;  //60
						string str = "";
						int heOwnNote = 0;
						for(int j = 0; j < ownNotes.Length; j++){
							int x = 0;
							//rHe = 40;
							str = ownNotes[j];
							heOwnNote = prevHe + 40;

							//GUI.Label(new Rect(30, 100+j*x*40, 600,100), ownNotes[j]);	
							GUI.Label(new Rect(30, heOwnNote, 600,100), ownNotes[j]);	

							x = 0;
						
							if(str.Length > 40){
								x = 40; 
							}	// 2 rows
							//height = 100 + j*40 + x;
							
							prevHe = heOwnNote + x;
							//prevHe = height;
							
						}
					
					}
					
				}
				// PWC OPTIONS LAYOUT
				if(currentPWCpage == (int)PWC.options){
					//GUI.Label(new Rect(Screen.width*(261-202)/1024, Screen.height*(47+30)/768, 100, 100), "GAME");
					GUI.Label(new Rect(Screen.width*(261-202)/1024, Screen.height*(47+30+10)/768, 100, 100), "GAME");
					//if(GUI.Button(new Rect(300, 400, 100, 100), "New game")){
					//if(GUI.Button(new Rect(300, 400, 100, 100), newGame)){
					/*
					if(GUI.Button(new Rect(Screen.width*(377-202)/1024, Screen.height*47/768, 100, 100), newGame)){
						Destroy(this.gameObject);
						Application.LoadLevel("cut_scene1");
					}
					*/
					//if(GUI.Button(new Rect(450, 400, 100, 100), "Quit")){
					
					//if(GUI.Button(new Rect(Screen.width*(377-202)/1024, Screen.height*124/768, 100, 100), quitGame)){
					if(GUI.Button(new Rect(Screen.width*(377-202)/1024, Screen.height*(47+30)/768, 100, 52), quitGame)){
						Application.Quit();
					}
				
					//GUI.DrawTexture(new Rect(Screen.width*15/1024,Screen.height*103/768, Screen.width*786/1024, Screen.height*619/768), backgroundImage);
					//GUI.DrawTexture(new Rect(opX, opY, opWi, opHe), backgroundImage);
					// Texture for sounds button will be changed
					//GUI.Label(new Rect(Screen.width*(261-202)/1024, Screen.height*(209+30+50)/768, 100, 100), "MUSIC");
					GUI.Label(new Rect(Screen.width*(261-202)/1024, Screen.height*(209+10)/768, 100, 100), "MUSIC");
					//if(GUI.Button(new Rect(300, 200, 100, 100), soundButton)){
					//if(GUI.Button(new Rect(Screen.width*(377-202)/1024, Screen.height*(209+50)/768, 100, 100), soundButton)){
					if(GUI.Button(new Rect(Screen.width*(377-202)/1024, Screen.height*(209)/768, 100, 52), soundButton)){
						if(soundButton == soundOn){
							musicOnOff = true;
							onOffAudio(true);
							//musicOnOff = false;
							soundButton = soundOff;
						} else {
							musicOnOff = false;
							onOffAudio(false);
							//musicOnOff = true;
							soundButton = soundOn;
						}
					}
				
					/*
					//if(GUI.Button(new Rect(300, 200, 100, 100), "On")){
					if(GUI.Button(new Rect(300, 200, 100, 100), soundOn)){
						onOffAudio(true);
					}
					//if(GUI.Button(new Rect(450, 200, 100, 100), "Off")){
					if(GUI.Button(new Rect(450, 200, 100, 100), soundOff)){
						onOffAudio(false);
					}
					*/

				
				}
				//GUILayout.EndVertical();
			GUILayout.EndArea();


		}			
			
	}
}
