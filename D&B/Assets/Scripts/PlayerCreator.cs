using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCreator : MonoBehaviour
{
	public Image outPanel;
	public Player player;
	public Text playerNameTxt;
	public string playerName;
	public Color playerColor;
	public GamePlayerType gamePlayerType;
	public MachineCreator machineCreatorPrefab;
	public LayoutGroup colors;

	public static PlayerCreator main;

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
		string pColor = "#FF0000FF";
//		foreach (var p in PlayersManager.main.players) {
//			string hexColor = ColorUtility.ToHtmlStringRGB (p.color);
//			if (hexColor.Equals (pColor))
//				pColor = "FFF800FF";
//		}
		setColor (pColor);
		resetPlayerName ();
	}

	public void resetPlayerName(){
		playerNameTxt.text = "1";
		playerNameTxt.text = "";
	}

	public void setAttributes (){
		if (player == null) {
			string pName = playerName.Equals ("") ? playerNameTxt.text.Equals ("") ? 
				"Jogador " + PlayersManager.main.players.Count + 1 : playerNameTxt.text : playerName;
			Color pColor = outPanel.color;
			player = new Player (pName, pColor, PlayerType.HUMAN_LOCAL, PlayerDificulty.HUMAN);
		} else {
			player.name = playerNameTxt.Equals ("") ? "Jogador " + PlayersManager.main.players.Count + 1 : playerNameTxt.text;
			player.color = outPanel.color;
		}
	}

	public void setColor(string colorHex){
		Color color;
		ColorUtility.TryParseHtmlString (colorHex, out color);
		outPanel.color = color;
	}

	public void confirm(){
		setAttributes ();
		PlayersManager.main.addPlayer (player);
		if (PlayersManager.main.players.Count == 2) {
			MainMenu.main.start ();
		}
		switch (gamePlayerType) {
		case GamePlayerType.PVPOFFLINE:
			reset ();
			break;
		case GamePlayerType.PVMOFFLINE:
			reset ();
			createMachine ();
			break;
		case GamePlayerType.PVPONLINE:
			break;
		}
		//DestroyImmediate (gameObject);
	}

	public void createMachine(){
		Instantiate (machineCreatorPrefab);
		DestroyImmediate (gameObject);
	}

	public void setGameType(GamePlayerType gamePlayerType){
		this.gamePlayerType = gamePlayerType;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

