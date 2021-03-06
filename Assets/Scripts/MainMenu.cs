﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	public GameObject[] MenuOptions;
	public GameObject RegMenu;
	public GameObject ControlsMenu;
	public GameObject selector;
	public Vector2 oldPos;
	public Vector2 newPos;
	public int selected;
	public int max;
	public bool axisPressed;

	// Use this for initialization
	void Start ()
	{

		oldPos = RegMenu.transform.position;
		newPos = new Vector2 (5000, 0);
		selected = 0;
		selectionEffect ();
		axisPressed = false;
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (axisPressed == false) {
			if (Input.GetAxis ("MenuDPad") < 0) {

				print("Joystick working");

				if (selected == max) {

					selected = 0;

				} else {
					selected++;
				}

				selectionEffect ();
				axisPressed = true;
		
			} else if (Input.GetAxis ("MenuDPad") > 0) {

			//	deSelect ();

				if (selected == 0) {
				
					selected = max;
				
				} else {
					selected--;
				}

				selectionEffect ();
				axisPressed = true;
		
			}
		}

		if (Input.GetAxis ("MenuDPad") == 0) {
		
			axisPressed = false;
		
		}

		if(Input.GetButtonDown("Submit")){

			selectOption ();

		}

		if (ControlsMenu.activeSelf == true && Input.GetButtonDown ("Cancel")) {
		
			ControlsMenu.SetActive(false);
			selector.SetActive(true);
			RegMenu.transform.position = oldPos;
		
		}



	}

	void selectionEffect ()
	{
		if (selected != 2) {
			selector.transform.position = MenuOptions [selected].transform.position;
		} else {
		
			selector.transform.position = new Vector2(MenuOptions [selected].transform.position.x, MenuOptions [selected].transform.position.y-1);
		
		}
	}

	void deSelect ()
	{

		MenuOptions [selected].transform.localScale = new Vector2 (5f, 5f);

	}

	void selectOption(){

		if (MenuOptions [selected].name == "Controls") {
		
			RegMenu.transform.position = newPos;
			ControlsMenu.SetActive (true);
			selector.SetActive(false);
		
		} else {

			GameManager.ChooseLevel (MenuOptions [selected].name);
		}

	}

}
