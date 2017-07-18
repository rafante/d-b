using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IMarkObserver {

	public Mark up_mark, down_mark, left_mark, right_mark;
	public SpriteRenderer cell_fill;

	// Use this for initialization
	void Start () {
		cell_fill.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void markPoint(){
		cell_fill.enabled = true;
		cell_fill.color = GameManager.main.getTurnPlayer ().color;
		GameManager.main.getTurnPlayer ().points++;
	}

	public void observeMarks(){
		if (!up_mark.observers.Contains (this)) {
			up_mark.observers.Add (this);
		}

		if (!down_mark.observers.Contains (this)) {
			down_mark.observers.Add (this);
		}

		if (!left_mark.observers.Contains (this)) {
			left_mark.observers.Add (this);
		}

		if (!right_mark.observers.Contains (this)) {
			right_mark.observers.Add (this);
		}
	}

	#region IMarkObserver implementation

	public bool onMarkSet (Mark mark)
	{
		if (up_mark.marked && down_mark.marked && left_mark.marked && right_mark.marked) {
			markPoint ();
			return true;
		}
		return false;
	}

	#endregion
}

