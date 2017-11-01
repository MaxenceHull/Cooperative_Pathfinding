using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitCell : CellInterface{
	public bool isObstacle {
		get;
		set;
	}

	public bool isPath {
		get;
		set;
	}

	public int x {
		get;
		set;
	}

	public int y {
		get;
		set;
	}

	public int t {
		get;
		set;
	}

	public Vector2 position {
		get {
			return new Vector2 (x, y);
		}
	}

	public Vector3 position3D {
		get {
			return new Vector3 (x * 10, 0, y * 10);
		}
	}

	public override bool Equals(object obj){
		ImplicitCell b = (ImplicitCell)obj;
		return this.x == b.x && this.y == b.y;
	}

	public override string ToString ()
	{
		return string.Format ("[ImplicitCell: x={0}, y={1}, t={2}]", x, y, t);
	}
}
