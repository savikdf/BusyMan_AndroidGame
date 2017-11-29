using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	PlayerStats playerStats;
	Canvas HShop, VShop;
	MenuManager menuManager;
	//----------------------------------
	//for scrolling shit
	public RectTransform panel, center, panel2, center2;
	public Button[] bttn, bttn2;

	private float[] distance, distance2;
	private bool isDragging = false, isDragging2 = false;
	private int bttnDistance, bttnDistance2; 
	private int minButtonNum, minButtonNum2;

	//------------------

	void Start(){
		menuManager = GetComponent<MenuManager> ();
		playerStats = GetComponent<PlayerStats> ();

		//canvas
		HShop = GameObject.Find ("HShop").GetComponent<Canvas> ();
		VShop = GameObject.Find ("VShop").GetComponent<Canvas> ();
		//----
		int bttnLength = bttn.Length;
		distance = new float[bttnLength];
		bttnDistance = (int)Mathf.Abs (bttn[1].GetComponent<RectTransform>().anchoredPosition.x - 
		                                 bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
		//-----
		HShop.enabled = false;
		VShop.enabled = false;
	}

	void Update(){
		if (VShop.enabled == true) {
			for (int i = 0; i < bttn.Length; i++) {
				distance [i] = Mathf.Abs (center.transform.position.x - bttn [i].transform.position.x);
			}
			float minDistance = Mathf.Min (distance);
			for (int j = 0; j < bttn.Length; j++) {
				if (minDistance == distance [j]) {
					minButtonNum = j;
				}
			}
			if (!isDragging) {
				LerpToBttn (minButtonNum * - bttnDistance);
			}
		}
		if (HShop.enabled == true) {
			for (int i = 0; i < bttn2.Length; i++) {
				distance2 [i] = Mathf.Abs (center2.transform.position.x - bttn2 [i].transform.position.x);
			}
			float minDistance2 = Mathf.Min (distance);
			for (int j = 0; j < bttn2.Length; j++) {
				if (minDistance2 == distance2 [j]) {
					minButtonNum2 = j;
				}
			}
			if (!isDragging2) {
				LerpToBttn (minButtonNum2 * - bttnDistance2);
			}
		}
	}
	//---------------------------------------------
	void LerpToBttn(int position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 20f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
	}


	public void StartDrag(){
		isDragging = true;
	
	}
	public void EndDrag(){
		isDragging = false;
		
	}
	//-------------------------------------
	public void ShowShop(MenuManager.CanvasTypes orientationOfStore){
		if(orientationOfStore == MenuManager.CanvasTypes.Port){
			HShop.enabled = false;
			VShop.enabled = true;
		}
		else if(orientationOfStore == MenuManager.CanvasTypes.Land){
			HShop.enabled = true;
			VShop.enabled = false;
		}
	}

	public void CloseShop(){
		VShop.enabled = false;
		HShop.enabled = false;
		menuManager.OnShopClose ();
	}
	//-------------------------------------



}
