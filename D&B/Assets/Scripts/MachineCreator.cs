using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MachineCreator : MonoBehaviour
{
	public Image outPanel;
	public Player player;
	private string playerName;
	private Color playerColor;
	public PlayerDificulty dificulty = PlayerDificulty.IA_EASY;

	public static MachineCreator main;

	// Use this for initialization
	void Start ()
	{
		main = this;
		transform.SetParent(GameObject.FindGameObjectWithTag("PlayerCreatorCanvas").transform);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		reset ();
	}
	
	public void reset(){
		player = null;
		setAttributes ();
		setColor ("#FF0000FF");
	}

	public void setAttributes (){
		if (player == null)
			player = new Player ("Máquina", outPanel.color, PlayerType.IA, dificulty);
		else {
			player.color = outPanel.color;
			player.dificulty = dificulty;
		}
	}

	public void setDificulty(int dificulty){
		this.dificulty = (PlayerDificulty)dificulty;
	}

	public void setColor(string colorHex){
		Color color;
		ColorUtility.TryParseHtmlString (colorHex, out color);
		outPanel.color = color;
	}

	public void confirm(){
		setAttributes ();
		PlayersManager.main.addPlayer (player);
		MainMenu.main.start ();
	}

	public void createMachine(){

	}

	// Update is called once per frame
	void Update ()
	{

	}
}

