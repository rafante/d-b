using System;
using UnityEngine;
using System.Collections.Generic;

public class Player
{
	public string name;
	public Color color;
	public int points;
	public PlayerType type;
	public PlayerDificulty dificulty;

	public Player(string name, Color color, PlayerType type, PlayerDificulty dificulty){
		this.name = name;
		this.color = color;
		this.points = 0;
		this.type = type;
		this.dificulty = dificulty;
	}

	public void move(){
		if (GameManager.main.remainingMarks.Count == 0)
			return;
		switch (dificulty) {
		case PlayerDificulty.HUMAN:
			break;
		case PlayerDificulty.IA_EASY:
			moveEasy ();
			break;
		case PlayerDificulty.IA_MEDIUM:
			moveMedium ();
			break;
		case PlayerDificulty.IA_HARD:
			break;
		}
	}

	public void moveMedium(){
		//Gets all the remaining possible moves
		List<Mark> marks = GameManager.main.remainingMarks;
		Dictionary<Mark, int> marksEval = new Dictionary<Mark, int> ();
		foreach (var mark in marks) {
			int points = 0;
			foreach (var observer in mark.observers) {
				Cell c = (Cell)observer;
				int marked = 0;
				if (c.up_mark.marked)
					marked++;
				if (c.down_mark.marked)
					marked++;
				if (c.left_mark.marked)
					marked++;
				if (c.right_mark.marked)
					marked++;
				if (marked == 0)
					points += 3;
				else if (marked == 1)
					points += 2;
				else if (marked == 2)
					points += 1;
				else if (marked == 3)
					points += 5;
			}
			marksEval.Add (mark, points);
		}
		KeyValuePair<Mark, int> selMark = new KeyValuePair<Mark, int>(marks[0], -1);
		foreach (var pair in marksEval) {
			if (selMark.Value == -1) {
				selMark = pair;
				continue;
			}
			if (pair.Value > selMark.Value)
				selMark = pair;
		}
		selMark.Key.setMarked ();
	}

	public void moveEasy(){
		//Gets all the remaining possible moves
		List<Mark> marks = GameManager.main.remainingMarks;
		Dictionary<Mark, int> marksEval = new Dictionary<Mark, int> ();
		foreach (var mark in marks) {
			int points = 0;
			foreach (var observer in mark.observers) {
				Cell c = (Cell)observer;
				int marked = 0;
				if (c.up_mark.marked)
					marked++;
				if (c.down_mark.marked)
					marked++;
				if (c.left_mark.marked)
					marked++;
				if (c.right_mark.marked)
					marked++;
				if (marked == 1)
					points += 2;
				else if (marked == 2)
					points += 1;
				else if (marked == 3)
					points += 3;
			}
			marksEval.Add (mark, points);
		}
		KeyValuePair<Mark, int> selMark = new KeyValuePair<Mark, int>(marks[0], -1);
		foreach (var pair in marksEval) {
			if (selMark.Value == -1) {
				selMark = pair;
				continue;
			}
			if (pair.Value > selMark.Value)
				selMark = pair;
		}
		selMark.Key.setMarked ();
	}
}
public enum PlayerType{
	HUMAN_LOCAL, HUMAN_REMOTE, IA
}

public enum PlayerDificulty{
	HUMAN, IA_EASY, IA_MEDIUM, IA_HARD
}