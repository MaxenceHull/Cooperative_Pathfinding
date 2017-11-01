using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, CellInterface {
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

	// Update is called once per frame
	void Update () {
		if (isObstacle) {
			GetComponent<Renderer> ().material.color = Color.black;
		} else if (isPath) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
	}
		

}
