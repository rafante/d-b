using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mark : MonoBehaviour {

	public SpriteRenderer markSprite;
	public SpriteRenderer point1, point2;
	public List<IMarkObserver> observers = new List<IMarkObserver> ();
	public bool marked;
	public Player player;

	// Use this for initialization
	void Start () {
		markSprite = GetComponent<SpriteRenderer> ();
		markSprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (marked || GameManager.main.end || GameManager.main.getTurnPlayer().type == PlayerType.IA)
			return;
		setMarked ();
	}

	public void setMarked(){
		marked = true;
		setPlayer (GameManager.main.getTurnPlayer ());
		markSprite.enabled = true;
		bool nextPlayer = true;
		foreach (var c in observers) {
			if (c.onMarkSet (this))
				nextPlayer = false;
		}
		if (nextPlayer) 
			GameManager.main.nextTurn ();
		GameManager.main.remainingMarks.Remove (this);
		GameManager.main.checkGameOver ();
		if (GameManager.main.getTurnPlayer ().type == PlayerType.IA)
			GameManager.main.makeMove ();
	}

	public void setPlayer(Player player){
		this.player = player;
		markSprite.color = player.color;
	}
}
public interface IMarkObserver{
	bool onMarkSet(Mark mark);
}
