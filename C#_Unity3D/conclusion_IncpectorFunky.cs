/*
*   conclusion_InspectorFunky.cs
*
*   Author Virpi Karhula
*   17.12.2013
*   Oulu Game Lab
*   Inspector Funky Project
*/
using UnityEngine;
using System.Collections;

public class conclusion : MonoBehaviour {
	
	public GUISkin concSkin;
	
	Ray ray;
	Camera mainCam;
	GameObject cCamObj;
	GameObject swollenObj;
	GameObject scratchObj;
	GameObject orangeObj;
	GameObject arrowObj;
	GameObject funkyObj;
	GameObject jackoObj;
	GameObject textfieldObj;
	GameObject bubble1Obj;
	GameObject bubble2Obj;
	GameObject newsObj;
	Vector3 bubble1Pos;
	Vector3 bubble2Pos;
	Vector3 arrowVect;
	public Texture2D bubbTex;
	public int arr;
	public enum Character {Swollen = 1, Scratch = 2, Orange = 3}
	public int suspect;
	public int prevSuspect;
	int arrowCounter;
	int arrowDelay;
	bool countUp;
	public string [] swoString;
	public string [] scrString;
	public string [] oraString;
	private string swoStrAll;
	public Texture2D greenBg;
	public Vector3 stringPos;
	private string actString;
	GameObject anInvObj;
	PWC_menu anMenuScript;
	bool bubblesOver;
	bool guiEnabled;
	
	public Texture2D button1;
	public Texture2D button2;
	public Texture2D button3;
	bool btn1Selected;
	bool btn3Selected;
	bool showCredits;
	
	Vector3 fwd;
	
	public Texture2D news;
	public Texture2D newsSwo;
	public Texture2D newsScr;
	public Texture2D newsOra;
	public Texture2D credits;
	
	public Vector2 scrollPosition;
	int prevSelChar;
	
	GameObject bubble3Obj;
	GameObject bubble4Obj;
	public Texture2D bub3Text;
	public Texture2D bub4Text;
	public Texture [] bub3Arr;
	public Texture [] bub4Arr;
	public Texture2D curText;
	Vector3 bubble3Pos;
	Vector3 bubble4Pos;
	int curBubIndex;
	int bubCounter;
	int animBubDelay;
	int startBubDelay;
	bool wait3;
	int waitCounter3;
	bool wait4;
	int waitCounter4;
	public Texture2D contText;
	GameObject backGroundObj;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("currentLevel", 4);
		fwd = transform.TransformDirection(Vector3.forward);
		
		cCamObj = GameObject.FindGameObjectWithTag("CMainCamera");
		mainCam = cCamObj.camera;
		
		swollenObj = GameObject.Find("Swollen");
		scratchObj = GameObject.Find("Scratch");
		orangeObj = GameObject.Find("Orange");
		arrowObj = GameObject.Find("Arrow");
		funkyObj = GameObject.Find("Funky");
		jackoObj = GameObject.Find("Jacko");
		textfieldObj = GameObject.Find("TextField");
		bubble1Obj = GameObject.Find("Bubble1");
		bubble2Obj = GameObject.Find("Bubble2");
		newsObj = GameObject.Find("Newspaper");
		//bubblePos = new Vector3(bubbleObj.transform.position.x, bubbleObj.transform.position.y, bubbleObj.transform.position.z);
		bubble3Obj = GameObject.Find("Bubble3");
		bubble4Obj = GameObject.Find("Bubble4");
		backGroundObj = GameObject.Find("Background_back_parent");
		
			
		arr = 0;
		arrowCounter = 0;
		arrowDelay = 1;
		countUp = true;
		swoString = new string[10];
		scrString = new string[10];
		oraString = new string[10];
		/*
        //BTW: not so good idea to use tables
		swoString[0] = "Swollen has something against me.";
		swoString[1] = "Swollen and Pickle were business partners.";
		swoString[2] = "Swollen has been in jail for stealing from Orange and Pickle.";
		scrString[0] = "Scratch and Spot are Swollen’s minions.";
		scrString[1] = "Can I trust Scratch?";
		scrString[2] = "Scratch has been in jail for manslaughter.";
		oraString[0] = "I feel like Orange did not tell me everything...";
		oraString[1] = "Hmm Orange had easy access to axe.";
		oraString[2] = "Orange and Pickle were arguing over new recipe of pie.";
		
		oraString[3] = "Orange and Pickle were arguing in their house.";
		oraString[4] = "Orange’s motive for treason was money.";
		oraString[5] = "Victim of Orange’s betrayal was Pickle.";
		*/
		actString = "";
		
		stringPos = mainCam.WorldToScreenPoint(textfieldObj.transform.position); 
		btn1Selected = false;
		btn3Selected = false;
		bubblesOver = false;
		guiEnabled = false;
		scrollPosition = Vector2.zero;
		prevSelChar = (int)Character.Scratch;
		showCredits = false;
			
		curBubIndex = 0;
		bubble3Obj.collider.enabled = true;
		bubCounter = 0;
		animBubDelay = 10;	//8
		//startBubDelay = 8;
		wait3 = true;
		waitCounter3 = 0;
		wait4 = true;
		waitCounter4 = 0;
		
		copyStringsFromPwc();  //xxx
	
		// Check if the music is selected in PWC to be on or off
		//GameObject anInvObj;  //moved to definitions
		//PWC_menu anMenuScript;  //moved to definitions
		// haetaan maincamera PWC scenestä peliobjektiksi tähän
		anInvObj = GameObject.FindGameObjectWithTag("PMainCamera");  
		// haetaan PWC_menu skripti
		anMenuScript = (PWC_menu)anInvObj.GetComponent(typeof(PWC_menu));
		// kutsutaan PWC_menun public onOffAudio()-funktiota ja viedään komponenttina
		// PWC_menu skriptin public muuttuja musicOnOff
		anMenuScript.onOffAudio(anMenuScript.musicOnOff);
		//anInvObj.onOffAudio(false);
		
		//stringPos.Set(jackoObj.transform.position.x, jackoObj.transform.position.y, jackoObj.transform.position.z);
		//greenBg = new Texture2D;		
		//Vector3 funkyPos = camera.WorldToScreenPoint(funkyObj.transform.position);
	}
	
	
	/* -> pwc:een audioOnOff()
			} else if (PlayerPrefs.GetInt ("currentLevel") == 4){
			dgObj = GameObject.FindGameObjectWithTag("CMainCamera");  //tag is missing -> background
			conScript = (conclusion)dgObj.GetComponent(typeof(conclusion));
			if(onOff == true){
				dgObj.audio.mute = false;
			} else {
				dgObj.audio.mute = true;
			}
			dgObj = GameObject.FindGameObjectWithTag("Newspaper");
			conScript = (conclusion)dgObj.GetComponent(typeof(conclusion));
			if(onOff == true){
				dgObj.audio.mute = false;
			} else {
				dgObj.audio.mute = true;
			}
		}
	*/
	public void UnPause() {
		Time.timeScale = 1.0f;
		useGUILayout = true;
		guiEnabled = true;
	}
	
	// Animate first bubble (bubble3)
	void NextBub3Texture(){
		if(waitCounter3 > 6){  //8
			wait3 = false;
		}
		if(wait3 == true){
			waitCounter3++;
		} else {
			curBubIndex++;
			if(curBubIndex >= bub3Arr.Length) {
				curBubIndex = 0;
				wait3 = true;
				waitCounter3 = 0;	
			}
		}
	}
	
		
	// Animate second bubble (bubble4)
	void NextBub4Texture(){
		if(waitCounter4 > 6){
			wait4 = false;
		}
		if(wait4 == true){
			waitCounter4++;
		} else {
			curBubIndex++;
			if(curBubIndex >= bub4Arr.Length) {
				curBubIndex = 0;
				wait4 = true;
				waitCounter4 = 0;	
			}
		}
	}
	
	void copyStringsFromPwc(){
		GameObject anInvObj;
		PWC_menu anMenuScript;
		anInvObj = GameObject.FindGameObjectWithTag("PMainCamera");
		anMenuScript = (PWC_menu)anInvObj.GetComponent(typeof(PWC_menu));
		int i = 0;	
		for(i = 0; i < anMenuScript.pSwoString.Length; i++){
			swoString[i] = "";
			if(anMenuScript.pSwoString[i] != null){
				swoString[i] = anMenuScript.pSwoString[i];
			}
		} 
		for(i = 0; i < anMenuScript.pScrString.Length; i++){
			scrString[i] = "";
			if(anMenuScript.pScrString[i] != null){
				scrString[i] = anMenuScript.pScrString[i];
			}
		} 
		for(i = 0; i < anMenuScript.pOraString.Length; i++){
			oraString[i] = "";
			if(anMenuScript.pOraString[i] != null){
				oraString[i] = anMenuScript.pOraString[i];
			}
		} 	
	}
	
	
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		if(Input.GetMouseButtonDown(0) || Input.touchCount == 1) {
			RaycastHit hit;
			if(Input.GetMouseButtonDown(0))
				ray = mainCam.ScreenPointToRay(Input.mousePosition);
			else if(Input.touchCount == 1) {
				Touch touch = Input.touches[1]; //index out of range
				if(touch.phase == TouchPhase.Began)
					ray = mainCam.ScreenPointToRay(touch.position);
			}
			Physics.Raycast (ray, out hit);

			if(hit.collider.tag == "cBubble3"){
				bubble3Obj.transform.position = bubble3Pos;
				bubble3Obj.renderer.material.mainTexture = curText;
				bubble3Obj.renderer.enabled = false;
				bubble4Obj.renderer.enabled = true;
				bubble4Obj.collider.enabled = true;
				//Debug.Log("hit.collider.tag == " + hit.collider.tag);
			}
			if(hit.collider.tag == "cBubble4"){
				bubble4Obj.transform.position = bubble4Pos;
				bubble4Obj.renderer.material.mainTexture = curText;
				bubble4Obj.renderer.enabled = false;
				bubblesOver = true;
				//start showing gui elements
				guiEnabled = true;
				//start showing red arrow and load starting values
				suspect = (int)Character.Scratch;	
				actString = combineStrings(scrString);
				arrowVect.Set(scratchObj.transform.position.x, scratchObj.transform.position.y + 1.9f, jackoObj.transform.position.z);
				arrowObj.renderer.enabled = true;
				//start showing red arrow and character strings		
				//Debug.Log("hit.collider.tag == " + hit.collider.tag);
			}
			
			/*  // Bubbles without animation
			if(hit.collider.tag == "BubbleConc1"){
				bubble1Obj.transform.position = bubble1Pos;
				bubble1Obj.renderer.material.mainTexture = bubbTex;
				bubble1Obj.renderer.enabled = false;
				bubble2Obj.renderer.enabled = true;
				bubble2Obj.collider.enabled = true;
				Debug.Log("hit.collider.tag == " + hit.collider.tag);
			}
			if(hit.collider.tag == "BubbleConc2"){
				bubble2Obj.transform.position = bubble2Pos;
				bubble2Obj.renderer.material.mainTexture = bubbTex;
				bubble2Obj.renderer.enabled = false;
				bubblesOver = true;
				//start showing gui elements
				guiEnabled = true;
				//start showing red arrow and load starting values
				suspect = (int)Character.Scratch;	
				actString = combineStrings(scrString);
				arrowVect.Set(scratchObj.transform.position.x, scratchObj.transform.position.y + 1.9f, jackoObj.transform.position.z);
				//arrowVect.Set(scratchObj.transform.position.x, scratchObj.transform.position.y + 1.9f, arrowObj.transform.position.z);
				arrowObj.renderer.enabled = true;
				//start showing red arrow and character strings
				
				Debug.Log("hit.collider.tag == " + hit.collider.tag);
			}*/
			
			
			
			
			
			// it is possible to select characters after bubbles have gone and accuse button has not been pressed yet
			if(bubblesOver && !btn1Selected){
				// at the beginning colliders of characters need to be false before bubbles are gone
				swollenObj.collider.enabled = true;
				scratchObj.collider.enabled = true;
				orangeObj.collider.enabled = true;
				if(hit.collider.tag == "Swollen") { 
					suspect = (int)Character.Swollen;
					arrowVect.Set(swollenObj.transform.position.x  + 0.6f, swollenObj.transform.position.y + 3.2f, arrowObj.transform.position.z);
					actString = combineStrings(swoString);
				} else if(hit.collider.tag == "Scratch") { 
					suspect = (int)Character.Scratch;
					arrowVect.Set(scratchObj.transform.position.x, scratchObj.transform.position.y + 1.9f, arrowObj.transform.position.z);
					actString = combineStrings(scrString);
				} else if(hit.collider.tag == "OrangeConcl") { 
					suspect = (int)Character.Orange;
					arrowVect.Set(orangeObj.transform.position.x, orangeObj.transform.position.y + 2.5f, arrowObj.transform.position.z);
					actString = combineStrings(oraString);
				} 
			}
			if(hit.collider.tag == "Newspaper"){
				// First audiosource in backGroundObj
				backGroundObj.audio.mute = true;
				// Start playing credits music
				// Second audio source in newsObj
				newsObj.audio.mute = false;
				if(anMenuScript.musicOnOff == true){
					newsObj.audio.Play();
				}
				
				// when newspaper screen is clicked, credits texture is shown
				if(showCredits == false){
					newsObj.renderer.material.mainTexture = credits;  
					showCredits = true;
				// when credits screen is clicked, game is ended and return to start view
				} else {
					GameObject pwcObj = GameObject.FindGameObjectWithTag("PMainCamera");
					Destroy(pwcObj);
					Application.Quit();
					//Application.LoadLevel("cut_scene1");
				}
			}
			
			if (hit.collider.tag == "CAntenna"){
				//GameObject anInvObj;		//moved to class definitions
				//PWC_menu anMenuScript;
				anInvObj = GameObject.FindGameObjectWithTag("PMainCamera");
				anMenuScript = (PWC_menu)anInvObj.GetComponent(typeof(PWC_menu));
				anMenuScript.loc3Disabled = true;
				Time.timeScale = 0.0f;
				useGUILayout = false;
				guiEnabled = false;
				anMenuScript.Revoke();
			}
			//Debug.Log("hit.collider.tag == " + hit.collider.tag);
		} //mouse
		
		//red arrow moving above the suspect person
		arrowCounter++;
		if(arrowCounter >= arrowDelay) {
			arrowCounter = 0;
			moveArrow(arrowVect); 
		}
		
		bubCounter++;
		if(bubCounter >= animBubDelay) {
			bubCounter = 0;
			if(bubble3Obj.renderer.enabled == true){
				NextBub3Texture();  
				curText = (Texture2D) bub3Arr[curBubIndex];
				bubble3Obj.renderer.material.mainTexture = curText;
			} else if (bubble4Obj.renderer.enabled == true){
				NextBub4Texture();  
				curText = (Texture2D) bub4Arr[curBubIndex];
				bubble4Obj.renderer.material.mainTexture = curText;
			}
		}

		// if user answered "No" to guestion
		if(btn3Selected){
			/*
			if(prevSuspect != suspect){
				prevSuspect = suspect;
				scrollPosition.y = 0;	
			}
			*/
			//jos suspecti eri kuin edellisellä kerralla, aseta scrollPosition.y = 0;
			
			if(suspect == (int)Character.Swollen){
					arrowVect.Set(swollenObj.transform.position.x  + 0.6f, swollenObj.transform.position.y + 3.2f, arrowObj.transform.position.z);
					actString = combineStrings(swoString);
			} else if(suspect == (int)Character.Scratch){
					arrowVect.Set(scratchObj.transform.position.x, scratchObj.transform.position.y + 1.9f, arrowObj.transform.position.z);
					actString = combineStrings(scrString);
			} else if(suspect == (int)Character.Orange){
					arrowVect.Set(orangeObj.transform.position.x, orangeObj.transform.position.y + 2.5f, arrowObj.transform.position.z);
					actString = combineStrings(oraString);
				
			} 
			
		}


		//parempi käyttää tätä
		if(Input.GetMouseButtonUp(0)){
			// raycastin lähettäminen
			//raycastin osumisen käsittely
			//Debug.Log("mouse up");
		}
	} //UpDate()
	
	void OnGUI(){
		
		GUI.skin = concSkin;
		
		if(guiEnabled == true){
		
			GUI.DrawTexture(new Rect(Screen.width/3, Screen.height*0.63f, Screen.width/3, Screen.height/3), greenBg);

			scrollPosition = GUI.BeginScrollView(new Rect(Screen.width*0.35f, Screen.height*0.66f, Screen.width*0.3f, Screen.height*0.18f), scrollPosition, new Rect(0, 0, Screen.width*0.28f, Screen.height), false, false);
			GUI.Label(new Rect(0, 0, Screen.width*0.28f, Screen.height*0.6f), actString);	
			GUI.EndScrollView();
			
			if(btn1Selected == false){
				if(GUI.Button(new Rect(Screen.width*462/1024, Screen.height*495/576, Screen.width*100/1024, Screen.height*52/576), button1)){
					scrollPosition.y = 0;
					actString = "Are you sure it was him?";
					btn1Selected = true;
					swollenObj.collider.enabled = false;
					scratchObj.collider.enabled = false;
					orangeObj.collider.enabled = false;
				}
			}
			if(btn1Selected == true){
				actString = "Are you sure it was him?";
				// Yes
				if(GUI.Button(new Rect(Screen.width*387/1024, Screen.height*476/576, Screen.width*100/1024, Screen.height*52/576), button2)){
					guiEnabled = false;
					if(suspect == (int)Character.Swollen){
						newsObj.renderer.material.mainTexture = newsSwo; 
					} else if (suspect == (int)Character.Scratch){
						newsObj.renderer.material.mainTexture = newsScr;
					} else if(suspect == (int)Character.Orange){
						newsObj.renderer.material.mainTexture = newsOra;
					} 				
					newsObj.renderer.enabled = true;
					newsObj.collider.enabled = true;
					//Debug.Log("Yes");
				}
				// No
				if(GUI.Button(new Rect(Screen.width*537/1024, Screen.height*476/576, Screen.width*100/1024, Screen.height*52/576), button3)){
					btn3Selected = true;
					btn1Selected = false;
					scrollPosition.y = 0;
					swollenObj.collider.enabled = true;
					scratchObj.collider.enabled = true;
					orangeObj.collider.enabled = true;					
					//move to update to update the strings and arrow positions
					//Debug.Log("No");
				}
			}
		// Show "click to continue" when newspaper is shown
		} else if(newsObj.renderer.enabled == true && showCredits == false){
			GUI.DrawTexture(new Rect(0,Screen.height-20,200,20), contText);
		}
	} //OnGUI()
	
	string combineStrings(string [] s){
		string all = "";	
		for(int i = 0; i <= 9; i++){
			if(s[i] != null){
			all = all + s[i] + '\n';	
			}
		}
		//Debug.Log("all = " + all);
		return all;
	}
	
	// Red arrow moving up and down
	void moveArrow(Vector3 vec){
		if(countUp == true){
			arr++;
			if(arr >= 20) {
				countUp = false;
			}
		} else {
			arr--;	
			if(arr <= 0) {
				countUp = true;
			}
		}
		vec.Set(vec.x, vec.y - arr*0.01f, vec.z);
		arrowObj.transform.position = vec;
	}

}
