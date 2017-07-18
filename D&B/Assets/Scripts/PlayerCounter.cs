using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour {

	public Player player;
	public Text playerNameTxt;
	public Text pointsTxt;
	public Image backgound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		playerNameTxt.text = player.name;
		pointsTxt.text = player.points.ToString();
		backgound.color = player.color;
	}
}
