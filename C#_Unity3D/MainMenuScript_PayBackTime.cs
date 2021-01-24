/*
 *  MainMenuScript_PayBackTime.cs
 *
 *  by Virpi Karhula
 *  11.6.2013
 *  Oulu Game Lab 
 */
using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	private int boxWidth;	// Menu box measurements
	private int boxHeight;
	private int leftUpX;	// Location of left up corner of menu box
	private int leftUpY;
	private int buttonSpace;
	
	public Texture2D icon;
	public Texture2D background;		// width 1920 x height 1080
	public Texture2D menuBackground;	// width 848 x height 1060
	public Texture2D helpImage;			// width 1359 x height 1058
	public Texture2D startButton;		// width 528 x height 70
	public Texture2D helpButton;
	public Texture2D creditsButton;
	public Texture2D wwwButton;
	public Texture2D exitButton;
	public Texture2D backButton;
	public Texture2D startButtonPressed;
	public Texture2D helpButtonPressed;
	public Texture2D creditsButtonPressed;
	public Texture2D wwwButtonPressed;
	public Texture2D exitButtonPressed;
	public Texture2D backButtonPressed;
	
	public Texture2D bannerImage;
	public Texture2D blackImage;
	
	public Texture2D image1;
	public Texture2D image2;
	public Texture2D image3;
	public Texture2D image4;
	public Texture2D image5;
	public Texture2D image6;
	public Texture2D image7;
	public Texture2D image8;
	public Texture2D image9;
	public Texture2D image10;
	public Texture2D image11;
	public Texture2D image12;
	public Texture2D image13;
	public Texture2D image14;
	public Texture2D image15;
	public Texture2D image16;
	public Texture2D image17;
	public Texture2D image18;
	public Texture2D image19;
	public Texture2D image20;
	public Texture2D image21;
	public Texture2D image22;
	public Texture2D image23;
	public Texture2D image24;
	public Texture2D image25;
	public Texture2D image26;
	public Texture2D image27;
	public Texture2D image28;
	public Texture2D image29;
	public Texture2D image30;
	public Texture2D image31;
	public Texture2D image32;
	public Texture2D image33;
	public Texture2D image34;
	public Texture2D image35;
	public Texture2D image36;
	public Texture2D image37;
	public Texture2D image38;
	public Texture2D image39;
	public Texture2D image40;
	
	
	public Texture2D[] creditsImages;  // = new Texture2D[20];
	
	private float timerFstep;
	private int timerFindex;
	
	public Texture2D creditsTextImageRed;
	
	public Rect startButtonRect;
	public Rect helpButtonRect;
	public Rect creditsButtonRect;
	public Rect wwwButtonRect;
	public Rect exitButtonRect;
	public Rect exitCreditsButtonRect;
	public Rect backButtonRect;
	
	private bool startingGame;
	private bool showingHelp;
	private bool showingCredits;
	//private bool startingWww;
	//private bool exitingGame;
	
	private int imgCreditsLandscapeWidth;
	private int imgCreditsLandscapeHeight;
	
	public AudioClip clickSound;
	
	private int tableSize;  // TODO: should be const

	// parameters to check if the window size is changed
	int prevScreenWidth;
	int prevScreenHeight;
	
	// timer for changing the image in credits window
	float timerF;  // = 0.0f;
	
	float creditsPosition; // = 0.0f;
	float textImageStartLocation;  //not needed?
	float offset;
	
	
	public Texture2D image;
	
	// Credits view
	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;		
	private float textImageWidth = Screen.width/2 - (50+25);
	private float textImageHeight = Screen.height*3.1f;
	private float imageLeftX = Screen.width/2 + 25;
	private float imageLeftY = Screen.height/6;
	
	// Main menu view
	private float mainLeftX; // = leftUpX + 0.19F*boxWidth;
	private float mainLeftY;  // = leftUpY + 0.32F*boxHeight;
	private float mainWidth;  // = 0.62F*boxWidth;
	private float mainHeight;  // = 0.55F*boxHeight;
	
	// Help view	
	private float helpLeftX;  // = 0.15f*Screen.width;
	private float helpWidth;  // =  0.7f*Screen.width;
			

					
	
	// Function getTransMousePosition() transforms mouse position coordinates (0,0) from left down corner to left up corner
	// and takes into account the effect of menu background box window GUI.DrawTexture(..., menuBackground, ...) 
	// and GUI.BeginGroup()
	private Vector2 getTransMousePosition(){
		Vector2 vect;
		vect.x = Input.mousePosition.x - (leftUpX + 0.19F*boxWidth);
		vect.y = Screen.height - Input.mousePosition.y - (leftUpY + 0.32F*boxHeight);
		return (vect);
	}
	// Function getTransMousePosition2() transforms the mouse position coordinater for Credits Exit Button
	private Vector2 getTransMousePosition2(){
		Vector2 vect;
		vect.x = Input.mousePosition.x;
		vect.y = Screen.height - Input.mousePosition.y;
		return (vect);
	}
	// Function transforms the mouse position coordinates for Help Back Button
	private Vector2 getTransMousePosition3(){
		Vector2 vect;
		vect.x = Input.mousePosition.x;
		vect.y = Screen.height - Input.mousePosition.y;
		return (vect);
	}
	
	
	
	// Use this for initialization
	void Start () {
		startingGame = false;
		showingHelp = false;
		showingCredits = false;
		//startingWww = false;
		//exitingGame = false;

		
		// Loading textures
		background = (Texture2D)Resources.Load("start-menu-bg", typeof(Texture2D));
		menuBackground = (Texture2D)Resources.Load("start-menu-textbox", typeof(Texture2D));
		helpImage =  (Texture2D)Resources.Load("help-menu-textbox2", typeof(Texture2D)); 
		
		bannerImage = (Texture2D)Resources.Load("banner2", typeof(Texture2D));
		blackImage = (Texture2D)Resources.Load("Black", typeof(Texture2D));
		
		startButton  = (Texture2D)Resources.Load("button-start", typeof(Texture2D));		
		helpButton  = (Texture2D)Resources.Load("button-help", typeof(Texture2D));
		creditsButton  = (Texture2D)Resources.Load("button-credits", typeof(Texture2D));
		wwwButton  = (Texture2D)Resources.Load("button-web", typeof(Texture2D));
		exitButton = (Texture2D)Resources.Load("button-exit", typeof(Texture2D));
		backButton = (Texture2D)Resources.Load("button-back", typeof(Texture2D));
		startButtonPressed = (Texture2D)Resources.Load("alt-button-start", typeof(Texture2D));
		helpButtonPressed  = (Texture2D)Resources.Load("alt-button-help", typeof(Texture2D));
 		creditsButtonPressed = (Texture2D)Resources.Load("alt-button-credits", typeof(Texture2D));
		wwwButtonPressed = (Texture2D)Resources.Load("alt-button-web", typeof(Texture2D));
 		exitButtonPressed = (Texture2D)Resources.Load("alt-button-exit", typeof(Texture2D));
		backButtonPressed = (Texture2D)Resources.Load("alt-button-back", typeof(Texture2D));
		
		
		image1 = (Texture2D)Resources.Load("banner2", typeof(Texture2D));
		image2 = (Texture2D)Resources.Load("Zombie cake", typeof(Texture2D));
		image3 = (Texture2D)Resources.Load("IMG_2134", typeof(Texture2D));
		image4 = (Texture2D)Resources.Load("IMG_1947", typeof(Texture2D));
		image5 = (Texture2D)Resources.Load("IMG_04", typeof(Texture2D));
		image6 = (Texture2D)Resources.Load("IMG_05", typeof(Texture2D));
		image7 = (Texture2D)Resources.Load("Kybelvagen facebook", typeof(Texture2D));
		image8 = (Texture2D)Resources.Load("IMG_08", typeof(Texture2D));
		image9 = (Texture2D)Resources.Load("IMG_09", typeof(Texture2D));
		image10 = (Texture2D)Resources.Load("IMG_06", typeof(Texture2D));
		
		image11 = (Texture2D)Resources.Load("uv01", typeof(Texture2D));
		image12 = (Texture2D)Resources.Load("IMG_2160", typeof(Texture2D));
		image13 = (Texture2D)Resources.Load("UV02", typeof(Texture2D));
		image14 = (Texture2D)Resources.Load("UV03", typeof(Texture2D));
		image15 = (Texture2D)Resources.Load("group", typeof(Texture2D));
		image16 = (Texture2D)Resources.Load("OGLphoto3", typeof(Texture2D));
		image17 = (Texture2D)Resources.Load("isgXXMa", typeof(Texture2D));
		image18 = (Texture2D)Resources.Load("Tommu", typeof(Texture2D));
		image19 = (Texture2D)Resources.Load("farm_house", typeof(Texture2D));
		image20 = (Texture2D)Resources.Load("IMG_0211", typeof(Texture2D));
		
		image21 = (Texture2D)Resources.Load("IMG_2157", typeof(Texture2D));
		image22 = (Texture2D)Resources.Load("IMG_2168", typeof(Texture2D));
		image23 = (Texture2D)Resources.Load("red-v", typeof(Texture2D));
		image24 = (Texture2D)Resources.Load("IMG_2179", typeof(Texture2D));
		image25 = (Texture2D)Resources.Load("sandbox_UVtextures", typeof(Texture2D));
		image26 = (Texture2D)Resources.Load("detonator", typeof(Texture2D));  //
		image27 = (Texture2D)Resources.Load("IMG_0207", typeof(Texture2D));
		image28 = (Texture2D)Resources.Load("Poster", typeof(Texture2D));
		image29 = (Texture2D)Resources.Load("synagogue_textured2", typeof(Texture2D));
		image30 = (Texture2D)Resources.Load("IMG_2156", typeof(Texture2D));
		
		image31 = (Texture2D)Resources.Load("sign", typeof(Texture2D));
		image32 = (Texture2D)Resources.Load("IMG_3", typeof(Texture2D));
		image33 = (Texture2D)Resources.Load("IMG_0221", typeof(Texture2D));
		image34 = (Texture2D)Resources.Load("IMG_0197", typeof(Texture2D));
		image35 = (Texture2D)Resources.Load("IMG_20130522_204747", typeof(Texture2D));
		image36 = (Texture2D)Resources.Load("flyer-front2 (1)", typeof(Texture2D));
		image37 = (Texture2D)Resources.Load("IMG_1", typeof(Texture2D));
		image38 = (Texture2D)Resources.Load("IMG_2", typeof(Texture2D));
		image39 = (Texture2D)Resources.Load("IMG_0295", typeof(Texture2D));
		image40 = (Texture2D)Resources.Load("start-menu-bg", typeof(Texture2D));
		
		
		tableSize = 40;
				
		creditsImages = new Texture2D[tableSize];
		
		creditsImages[0] = (Texture2D) image1;
		creditsImages[1] = (Texture2D) image2;
		creditsImages[2] = (Texture2D) image3;
		creditsImages[3] = (Texture2D) image4;
		creditsImages[4] = (Texture2D) image5;
		creditsImages[5] = (Texture2D) image6;
		creditsImages[6] = (Texture2D) image7;
		creditsImages[7] = (Texture2D) image8;
		creditsImages[8] = (Texture2D) image9;
		creditsImages[9] = (Texture2D) image10;
		creditsImages[10] = (Texture2D) image11;
		creditsImages[11] = (Texture2D) image12; 
		creditsImages[12] = (Texture2D) image13;
		creditsImages[13] = (Texture2D) image14;
		creditsImages[14] = (Texture2D) image15;
		creditsImages[15] = (Texture2D) image16;
		creditsImages[16] = (Texture2D) image17;
		creditsImages[17] = (Texture2D) image18;
		creditsImages[18] = (Texture2D) image19;
		creditsImages[19] = (Texture2D) image20;
		creditsImages[20] = (Texture2D) image21;
		creditsImages[21] = (Texture2D) image22;
		creditsImages[22] = (Texture2D) image23;
		creditsImages[23] = (Texture2D) image24;
		creditsImages[24] = (Texture2D) image25;
		creditsImages[25] = (Texture2D) image26;
		creditsImages[26] = (Texture2D) image27;
		creditsImages[27] = (Texture2D) image28;
		creditsImages[28] = (Texture2D) image29;
		creditsImages[29] = (Texture2D) image30;
		creditsImages[30] = (Texture2D) image31;
		creditsImages[31] = (Texture2D) image32;
		creditsImages[32] = (Texture2D) image33;
		creditsImages[33] = (Texture2D) image34;
		creditsImages[34] = (Texture2D) image35;
		creditsImages[35] = (Texture2D) image36;
		creditsImages[36] = (Texture2D) image37;
		creditsImages[37] = (Texture2D) image38;
		creditsImages[38] = (Texture2D) image39;
		creditsImages[39] = (Texture2D) image40;
		
					
		timerF = 0.0f;
		creditsPosition = 0.0f;	
		timerFstep = 40.0f;
		timerFindex = 0;
		// the offset for text image where to start in order to show the title
		textImageStartLocation = 100.0f;  //wild guess
		offset = 2000; //wild guess
		
		// parameters to check if the window size is changed
		prevScreenWidth = 0;
		prevScreenHeight = 0;
		
		//imgCreditsLandscapeWidth = Screen.width;
		//imgCreditsLandscapeHeight = Screen.height;
		
			// Calculating the credits image width and height in current window size
			imgCreditsLandscapeWidth =  Screen.width/2 - (25 + 50);
			imgCreditsLandscapeHeight = imgCreditsLandscapeWidth*2/3;

		
		audio.clip = clickSound;
				
		creditsTextImageRed = (Texture2D)Resources.Load("End credits-red", typeof(Texture2D));	//jpg
		//creditsTextImageRed = (Texture2D)Resources.Load("End credits", typeof(Texture2D));  //png

		boxWidth = (int) Mathf.Round(0.44f * Screen.width);
		boxHeight = Screen.height; // 320;
		leftUpX = Screen.width / 2 - boxWidth / 2;
		leftUpY = 0; // Screen.height / 2 - boxHeight / 2;
		buttonSpace = (int) Mathf.RoundToInt( 0.50F*boxHeight / (4 + 5));  // space between buttons is the same as the height of the buttons
		
		startButtonRect = new Rect(0, 0*buttonSpace, 0.62F*boxWidth, buttonSpace);
		helpButtonRect = new Rect(0, 2*buttonSpace, 0.62F*boxWidth, buttonSpace);
		creditsButtonRect = new Rect(0, 4*buttonSpace, 0.62F*boxWidth, buttonSpace);
		wwwButtonRect = new Rect(0, 6*buttonSpace, 0.62F*boxWidth, buttonSpace);
		exitButtonRect = new Rect(0, 8*buttonSpace, 0.62F*boxWidth, buttonSpace);

		exitCreditsButtonRect = new Rect(Screen.width*3/4 - 120/2, Screen.height -200, 120, 40);

		backButtonRect = new Rect(0.15f*Screen.width + 40, 0.93f*Screen.height - 40, 0.16f*Screen.width, 0.07f*Screen.height);
		
		// Credits view
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		textImageWidth = Screen.width/2 - (50+25);
		textImageHeight = Screen.height*3.1f;
		imageLeftX = Screen.width/2 + 25;
		imageLeftY = Screen.height/6;	
		
		// Main menu view
		mainLeftX = leftUpX + 0.19F*boxWidth;
		mainLeftY = leftUpY + 0.32F*boxHeight;
		mainWidth = 0.62F*boxWidth;
		mainHeight = 0.55F*boxHeight;
		
		// Help view	
		helpLeftX = 0.15f*Screen.width;
		helpWidth =  0.7f*Screen.width;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// New values will be counted only when the window size changes
		// BTW: This if-statement will be driven through only once at the beginning of the program!
		if (Screen.width != prevScreenWidth || Screen.height != prevScreenHeight){
			
			// Main menu box location
			boxWidth = (int) Mathf.Round(0.44f * Screen.width);		
			boxHeight = Screen.height; 
			leftUpX = Screen.width / 2 - boxWidth / 2;
			leftUpY = 0; 
			
			buttonSpace = (int) Mathf.RoundToInt( 0.50F*boxHeight / (4 + 5));  // space between buttons is the same as the height of the buttons
				
			startButtonRect = new Rect(0, 0*buttonSpace, 0.62F*boxWidth, buttonSpace);
			helpButtonRect = new Rect(0, 2*buttonSpace, 0.62F*boxWidth, buttonSpace);
			creditsButtonRect = new Rect(0, 4*buttonSpace, 0.62F*boxWidth, buttonSpace);
			wwwButtonRect = new Rect(0, 6*buttonSpace, 0.62F*boxWidth, buttonSpace);
			exitButtonRect = new Rect(0, 8*buttonSpace, 0.62F*boxWidth, buttonSpace);

			exitCreditsButtonRect = new Rect(Screen.width*3/4 - 120/2, Screen.height*4/5, 120, 40);
			backButtonRect = new Rect(0.15f*Screen.width + 40, 0.93f*Screen.height - 40, 0.16f*Screen.width, 0.07f*Screen.height);
			//Debug.Log("Screen.width = " + prevScreenWidth + ", Screen.height = " + prevScreenHeight);
		
			// Credits
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			textImageWidth = Screen.width/2 - (50+25);
			textImageHeight = Screen.height*3.1f;
			imageLeftX = Screen.width/2 + 25;
			imageLeftY = Screen.height/6;
			// Calculating the credits image width and height in current window size
			imgCreditsLandscapeWidth =  Screen.width/2 - (25 + 50);
			imgCreditsLandscapeHeight = imgCreditsLandscapeWidth*2/3;
			
			if (screenHeight >= 1050){	
				offset = 2945.0f + 180.0f;  //TODO add and test bigger screen height
			} else if (screenHeight >= 1000){  
				offset = 2810.0f + 180.0f;				// 2270 + 
			} else if (screenHeight >= 900){  
				offset = 2540.0f + 180.0f;	
			} else if (screenHeight >= 800){  
				offset = 2270.0f + 180.0f;	
			} else if (screenHeight >= 700){
				offset = 2000.0f + 180.0f; 
			} else if (screenHeight >= 600){
				offset = 1730.0f + 180.0f;
			} else if (screenHeight >= 500){
				offset = 1400.0f + 180.0f;
			} else if (screenHeight >= 400){
				offset = 1150.0f + 180.0f;
			} else if (screenHeight >= 300){
				offset = 850.0f + 180.0f;
			} else {
				offset = 600.0f + 180.0f;	
			}
			Debug.Log("Screen.height = " + Screen.height + ", creditsPosition = " + creditsPosition);

			
			// Main menu view
			mainLeftX = leftUpX + 0.19F*boxWidth;
			mainLeftY = leftUpY + 0.32F*boxHeight;
			mainWidth = 0.62F*boxWidth;
			mainHeight = 0.55F*boxHeight;
			
			// Help view	
			helpLeftX = 0.15f*Screen.width;
			helpWidth =  0.7f*Screen.width;	
			
		}
		prevScreenWidth = Screen.width;
		prevScreenHeight = Screen.height;
		
		// Detecting the mouse selections

		if(Input.GetKeyDown(KeyCode.Mouse0)){
			if(startButtonRect.Contains(getTransMousePosition() )){
			//if(startButtonRect.Contains(Input.mousePosition)){  //(0,0) coordinate in left down corner
				startingGame = true;	//TODO need to check if this is needed -> here or more probably in game scene script
				Application.LoadLevel("GameStart");
					 			
			 	// In the game play, if the player wants to quit the game and return to MainMenu (not tested)
				//if (Input.GetKey("escape")){ 
				//	Application.LoadLevel("MainMenu");
			    //} 
				
			} else if(helpButtonRect.Contains(getTransMousePosition() )){
				showingHelp = true;
				
			} else if(creditsButtonRect.Contains(getTransMousePosition() )){
				showingCredits = true;
				audio.Play();
				timerF = 0.0f;  //Starting from first image each time
				timerFindex = 0;
				
			} else if(wwwButtonRect.Contains(getTransMousePosition()) ){
				//startingWww = true;
				Application.OpenURL("http://adeathtoofar.com/");
				
			} else if(exitButtonRect.Contains(getTransMousePosition() )){
				//exitingGame = true;
				Application.Quit();
			}
		}

		
		if (showingCredits){
					
			// Calculating the credits image width and height in current window size
			//imgCreditsLandscapeWidth =  Screen.width/2 - (25 + 50);
			//imgCreditsLandscapeHeight = imgCreditsLandscapeWidth*2/3;
				
			// Scrolling text image stops the scrolling 
  
			if(creditsPosition > -offset){   
				creditsPosition -= Time.deltaTime * 30.0f;
				//Debug.Log("creditsPosition = " + creditsPosition);  //
				//print ("creditsPosition = " + creditsPosition);  //
			} else {
				creditsPosition = Screen.height + 1.0f;  //+1f 
				//print ("ELSE creditsPosition " + creditsPosition); //
				//print("ELSE Screen.height =  " + Screen.height);  //
				//print("ELSE Screen.height +1f =  " + Screen.height + 1.0f);  //
			}
			timerF += Time.deltaTime * 10.0F;
			
			// Calculating index to creditImages[] table to show on OnGUI()
			if ((timerF >= -0.9f + timerFindex * timerFstep) && (timerF < (timerFindex + 1) * timerFstep)){
			} else {
				// Chance next image to screen
				timerFindex++;
				// Start from first image at the table
				if(timerFindex >= tableSize){	
					timerFindex = 0;	
					timerF = 0.0f; 
				}
			}
			
		}
		
	}

	
	void ShowMainMenu(){
		
		// Make a background box
		GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), background, ScaleMode.StretchToFill);
		
		// Basic menu window 
		GUI.DrawTexture(new Rect(leftUpX, leftUpY, boxWidth, boxHeight), menuBackground, ScaleMode.StretchToFill);
		
		//GUI.BeginGroup (new Rect(leftUpX + 0.19F*boxWidth, leftUpY + 0.32F*boxHeight, 0.62F*boxWidth, 0.55F*boxHeight));  //last parameter could maybe be bigger: 0.62F (?)
		GUI.BeginGroup (new Rect(mainLeftX, mainLeftY, mainWidth, mainHeight));
		
		// Buttons when mouse over
		if(startButtonRect.Contains(getTransMousePosition() )){
			GUI.DrawTexture(startButtonRect, startButtonPressed);
		} else {
			// buttons in normal view
			GUI.DrawTexture(startButtonRect, startButton);
		}
		if(helpButtonRect.Contains(getTransMousePosition() )){
			GUI.DrawTexture(helpButtonRect, helpButtonPressed);
		} else {
			GUI.DrawTexture(helpButtonRect, helpButton);
		}
		if(creditsButtonRect.Contains(getTransMousePosition() )){
			GUI.DrawTexture(creditsButtonRect, creditsButtonPressed);
		} else {
			GUI.DrawTexture(creditsButtonRect, creditsButton);
		}
		if(wwwButtonRect.Contains(getTransMousePosition() )){
			GUI.DrawTexture(wwwButtonRect, wwwButtonPressed);
		} else {
			GUI.DrawTexture(wwwButtonRect, wwwButton);
		}
		if(exitButtonRect.Contains(getTransMousePosition() )){
			GUI.DrawTexture(exitButtonRect, exitButtonPressed);
		} else {
			GUI.DrawTexture(exitButtonRect, exitButton);
		}
		
		GUI.EndGroup();
		
	}
	
	
	void OnGUI () {
		
		// MAIN MENU
		
		if (startingGame == false && showingHelp == false && showingCredits == false){   //&& startingWww == false && exitingGame == false ){ 
			ShowMainMenu();		
			
			creditsPosition = Screen.height -180 ;
			//Debug.Log("textImageStartLocation = " + textImageStartLocation);
			
		// CREDITS SCREEN
			
		} else if (showingCredits == true) {
			
			// Black background image
			GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), blackImage, ScaleMode.StretchToFill);
			
			// Scrolling image on the left side of the screen showing credits texts in an image
			GUI.DrawTexture(new Rect(50, creditsPosition, textImageWidth, textImageHeight), creditsTextImageRed, ScaleMode.StretchToFill);

			// Showing image on the right side of the screen. The image index (timerFindex) is calculated on Update() function		
			GUI.DrawTexture(new Rect(imageLeftX, imageLeftY, imgCreditsLandscapeWidth, imgCreditsLandscapeHeight), creditsImages[timerFindex], ScaleMode.ScaleToFit);
			

			// Credits Exit button actions			
			if(Input.GetKeyDown(KeyCode.Mouse0)){
				if(exitCreditsButtonRect.Contains(getTransMousePosition2() )){   //(0,0) coordinate in left down corner
					showingCredits = false;
					audio.Stop();
				}
			}
			if(exitCreditsButtonRect.Contains(getTransMousePosition2() )){
				GUI.DrawTexture(exitCreditsButtonRect, exitButtonPressed);
			} else {
				GUI.DrawTexture(exitCreditsButtonRect, exitButton);
			}
	
	
		// HELP SCREEN
			
		} else if (showingHelp == true) {
			
			// Make a background box
			GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), background, ScaleMode.StretchToFill);
			
			// Help menu window 
			GUI.DrawTexture(new Rect(helpLeftX, 0, helpWidth, screenHeight), helpImage, ScaleMode.StretchToFill);
			
			// Help Back button actions			
			if(Input.GetKeyDown(KeyCode.Mouse0)){
				if(backButtonRect.Contains(getTransMousePosition3() )){   //(0,0) coordinate in left down corner
					showingHelp = false;
				}
			}
			if(backButtonRect.Contains(getTransMousePosition3() )){
				GUI.DrawTexture(backButtonRect, backButtonPressed);
			} else {
				GUI.DrawTexture(backButtonRect, backButton);
			}

		}
		
	
	} // OnGUI()

} 
	

