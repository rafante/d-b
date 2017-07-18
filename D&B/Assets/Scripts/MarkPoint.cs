using System;

public class MarkPoint
{
	public int x;
	public int y;
	public Position position;

	public MarkPoint (int x, int y, Position position)
	{
		this.x = x;
		this.y = y;
		this.position = position;

	}
}

public enum Position{
	VERTICAL, HORIZONTAL
}