using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersManager : MonoBehaviour
{
	public static PlayersManager main;
	public IList<Player> players;

	// Use this for initialization
	void Awake ()
	{
		main = this;
		players = new List<Player> ();
		DontDestroyOnLoad (gameObject);
	}

	public void addPlayer(Player player){
		addPlayer (player.name, player.color, player.type, player.dificulty);
	}

	public void addPlayer(string name, Color color, PlayerType type, PlayerDificulty dificulty){
		players.Add(new Player(name, color, type, dificulty));
	}

	public void reset(){
		players = new List<Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

