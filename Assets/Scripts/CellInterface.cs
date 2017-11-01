using UnityEngine;

public interface CellInterface {
	bool isObstacle {
		get;
		set;
	}

	bool isPath {
		get;
		set;
	}

	int x {
		get;
		set;
	}

	int y {
		get;
		set;
	}


	Vector2 position {
		get;
	}

	Vector3 position3D {
		get;
	}

}
