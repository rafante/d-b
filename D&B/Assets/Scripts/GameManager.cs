using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager main;

	public Board board;
	public IList<Player> players {
		get{
			return PlayersManager.main.players;
		}
	}
	public int turn = 0;
	public List<Text> playersScore; 
	public PlayerCounter playerCounterPrefab;
	public Transform playersPanel;
	public List<Mark> remainingMarks;
	public GameObject gameOverPrefab;
	public bool end;
	public Text turnPlayer;

	// Use this for initialization
	void Start ()
	{
		if (main == null)
			main = this;

//		Player player1 = new Player ("Jogador 1", Color.yellow, PlayerType.HUMAN_LOCAL, PlayerDificulty.HUMAN);
//		main.players.Add (player1);
//		Player player2 = new Player ("Jogador 2", Color.red, PlayerType.HUMAN_LOCAL, PlayerDificulty.HUMAN);
//		main.players.Add (player2);

		foreach (Player player in players) {
			PlayerCounter pc = Instantiate (playerCounterPrefab);
			pc.player = player;

			pc.transform.SetParent (playersPanel);
			pc.transform.localPosition = Vector3.zero;
			pc.transform.localScale = Vector3.one;
		}

		//Mark[] allMarks = FindObjectsOfType<Mark>();
		//foreach (var mark in allMarks) {
		//	remainingMarks.Add (mark);
		//}
		remainingMarks = board.allMarks;
	}

	public void makeMove(){
		if (getTurnPlayer ().type != PlayerType.IA)
			return;
		Invoke ("machineMove", 1.2f);
	}

	public void machineMove(){
		getTurnPlayer ().move ();
	}

	public void gameOver(){
		if (end)
			return;
		end = true;
		Invoke ("setGameOverScreenVisible", 1f);
		Debug.Log ("Game Over");
	}

	public void setGameOverScreenVisible(){
		Player winner = getWinner ();
		string winTxt = winner != null ? "Parabéns\n" + winner.name : "Empate";
		gameOverPrefab.GetComponent<GameOverScreen> ().winnerTxt.text = winTxt;
		gameOverPrefab.SetActive (true);
	}

	public void checkGameOver(){
		if (remainingMarks.Count == 0) {
			gameOver ();
			return;
		}
	}

	public void changeTurn(bool forward){
		if (end)
			return;
		if (forward) {
			turn++;
			if (turn >= players.Count) {
				turn = 0;
			}
		} else {
			turn--;
			if (turn < 0)
				turn = players.Count - 1;
		}
	}

	public void nextTurn(){
		changeTurn (true);
	}

	public void previousTurn(){
		changeTurn (false);
	}

	public Player getTurnPlayer(){
		return players [turn];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public Player getWinner(){
		Player p1 = players [0];
		Player p2 = players [1];
		if (p1.points > p2.points)
			return p1;
		else if (p1.points < p2.points)
			return p2;
		else
			return null;
//		Player winner = players[0];
//		foreach (var player in players) {
//			if (player.points > winner.points)
//				winner = player;
//			else if (player.points == winner.points)
//				winner = null;
//		}
//		return winner;
	}

	public void resetGame(){
		SceneManager.LoadScene ("level1");
		foreach (var p in players) {
			p.points = 0;
		}
	}

	public void mainMenu(){
		SceneManager.LoadScene ("mainMenu");
	}

	void OnGUI(){
		Player p = getTurnPlayer ();
		turnPlayer.text = p.name;
		turnPlayer.color = p.color;
	}
}

