using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	
	public int sizeX, sizeY;
	public float baseSize = .45f;
	public float halfSize = .225f;
	public Mark markPrefab;
	public Cell cellPrefab;
	private int[,] matrix;
	private Dictionary<MarkPoint, Mark> marks;
	public Texture2D boardMap;
	private Color[] pixels;
	private Dictionary<string,Cell> cells;
	public List<Mark> allMarks = new List<Mark>();

	// Use this for initialization
	void Awake () {
		init ();
		//setUpMatrix ();
	}

	private void init(){
		setUpMap ();
		mapCellsMark ();
		mapMarksObservers ();
	}

	public void mapMarksObservers(){
		foreach (var pair in cells) {
			pair.Value.observeMarks ();
		}
	}

	public void mapCellsMark(){
		foreach (var pair in cells) {
			if (pair.Value.up_mark == null) {
				string[] keyPair = pair.Key.Split (':');
				int y = int.Parse(keyPair [1]) + 1;
				Cell c = cells [keyPair [0] + ":" + y];
				pair.Value.up_mark = c.down_mark;
			}
		}
		foreach (var pair in cells) {
			if (pair.Value.down_mark == null) {
				string[] keyPair = pair.Key.Split (':');
				int y = int.Parse(keyPair [1]) - 1;
				Cell c = cells [keyPair [0] + ":" + y];
				pair.Value.down_mark = c.up_mark;
			}
		}
		foreach (var pair in cells) {
			if (pair.Value.left_mark == null) {
				string[] keyPair = pair.Key.Split (':');
				int x = int.Parse(keyPair [0]) - 1;
				Cell c = cells [x + ":" + keyPair [1]];
				pair.Value.left_mark = c.right_mark;
			}
		}
		foreach (var pair in cells) {
			if (pair.Value.right_mark == null) {
				string[] keyPair = pair.Key.Split (':');
				int x = int.Parse(keyPair [0]) + 1;
				Cell c = cells [x + ":" + keyPair [1]];
				pair.Value.right_mark = c.left_mark;
			}
		}
	}

	private void setUpMap(){
		pixels = boardMap.GetPixels ();
		int indexPixels = -1;
		cells = new Dictionary<string, Cell> ();

		float xOffset = (baseSize * boardMap.width) / 2;
		float yOffset = (baseSize * boardMap.height) / 2;

		for (int y = 0; y < boardMap.height; y++) {
			for (int x = 0; x < boardMap.width; x++) {
				indexPixels++;

				if (pixels [indexPixels] == Color.white)
					continue;

				CellPoint up = new CellPoint (x, y + 1);
				string upKey = up.x + ":" + up.y;
				CellPoint down = new CellPoint (x, y - 1);
				string downKey = down.x + ":" + down.y;
				CellPoint left = new CellPoint (x - 1, y);
				string leftKey = left.x + ":" + left.y;
				CellPoint right = new CellPoint (x + 1, y);
				string rightKey = right.x + ":" + right.y;

				int type = 0;

				if (cells.ContainsKey (upKey))
					type += 1;
				if (cells.ContainsKey (downKey))
					type += 2;
				if (cells.ContainsKey (leftKey))
					type += 4;
				if (cells.ContainsKey (rightKey))
					type += 8;

				//0: ALL, 1: RDL, 2: LUR, 3: RL, 4: URD, 5: RD, 6: UR, 7: R, 8: DLU,
				//9: DL, 10: UL, 11: L, 12: UD, 13: D, 14: U, 15: NONE

				string key = x + ":" + y;
				Cell cell = getCellByType ((CellType)type);
				cell.transform.SetParent (transform);

				cell.transform.localPosition = new Vector3 ((x * baseSize),(y * baseSize), 0);

				cells.Add(key, cell);
			}
		}
	}

	private void setUpMatrix(){
		matrix = new int[sizeX, sizeY];
		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				placeCell (x, y);
			}
		}
	}

	private void placeCell(int x, int y){
		
	}

	private void makeMarks(int x, int y){
		MarkPoint mpH = new MarkPoint (x, y, Position.HORIZONTAL);
		MarkPoint mpV = new MarkPoint (x, y, Position.VERTICAL);
		MarkPoint mpH1 = new MarkPoint (x + 1, y, Position.HORIZONTAL);
		MarkPoint mpV1 = new MarkPoint (x, y + 1, Position.VERTICAL);
		if (!marks.ContainsKey (mpH)) {
			//Mark mark = MonoBehaviour.Instantiate (markPrefab).GetComponent<Mark> ();
			//marks.Add (mpH, mark);
		}
		if (!marks.ContainsKey (mpV)) {
			//marks.Add (mpV, Instantiate(markPrefab).GetComponent<Mark>());
		}
		if (!marks.ContainsKey (mpH1)) {
			//marks.Add (mpH1, Instantiate(markPrefab).GetComponent<Mark>());
		}
		if (!marks.ContainsKey (mpV1)) {
			//marks.Add (mpV1, Instantiate(markPrefab).GetComponent<Mark>());
		}
	}

	public void addMarkToCell(Cell cell, MarkPosition position){
		Mark mark = Instantiate (markPrefab);
		mark.transform.SetParent (cell.transform);

		switch (position) {
		case MarkPosition.UP:
			mark.transform.localPosition = new Vector3 (halfSize, baseSize, 0);
			mark.transform.localRotation = Quaternion.Euler (0, 0, 90);
			cell.up_mark = mark;
			break;
		case MarkPosition.DOWN:
			mark.transform.localPosition = new Vector3 (halfSize, 0, 0);
			mark.transform.localRotation = Quaternion.Euler (0, 0, 90);
			cell.down_mark = mark;
			break;
		case MarkPosition.LEFT:
			mark.transform.localPosition = new Vector3 (0, halfSize, 0);
			cell.left_mark = mark;
			break;
		case MarkPosition.RIGHT:
			mark.transform.localPosition = new Vector3 (baseSize, halfSize, 0);
			cell.right_mark = mark;
			break;
		}

		if (!allMarks.Contains (mark)) {
			allMarks.Add (mark);
		}
	}

	public Cell getCellByType(CellType cellType){
		Cell cell = Instantiate (cellPrefab);

		switch (cellType) {
		case CellType.ALL:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.LEFT);
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.D:
			addMarkToCell (cell, MarkPosition.DOWN);
					break;
			case CellType.DL:
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.LEFT);
					break;
			case CellType.DLU:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.LEFT);
					break;
			case CellType.L:
			addMarkToCell (cell, MarkPosition.LEFT);
					break;
			case CellType.LUR:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.LEFT);
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.NONE:
					break;
			case CellType.R:
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.RD:
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.RDL:
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.LEFT);
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.RL:
			addMarkToCell (cell, MarkPosition.LEFT);
			addMarkToCell (cell, MarkPosition.RIGHT);
					break;
			case CellType.U:
			addMarkToCell (cell, MarkPosition.UP);
					break;
			case CellType.UD:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.DOWN);
				break;
			case CellType.UL:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.LEFT);
				break;
			case CellType.UR:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.RIGHT);
				break;
			case CellType.URD:
			addMarkToCell (cell, MarkPosition.UP);
			addMarkToCell (cell, MarkPosition.DOWN);
			addMarkToCell (cell, MarkPosition.RIGHT);
				break;
		}
		return cell;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
public class CellPoint
{
	public int x;
	public int y;

	public CellPoint (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}