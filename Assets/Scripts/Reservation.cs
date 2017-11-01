using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservation {

	//Singleton design pattern
	private static Reservation instance;
	public static Reservation Instance {
		get {
			if (instance == null) {
				instance = new Reservation();
			}
			return instance;
		}
	}

	//Subscribe
	List<Shopper> subscribers = new List<Shopper>();
	public void subscribe(Shopper shopper){
		subscribers.Add (shopper);
	}

	//Space-time grid
	public int window_size = 10;
	private List<ImplicitGrid> space_time_grid = new List<ImplicitGrid>();

	public void makeNextMove(){
		for (int i = 0; i < window_size; i++) {
			space_time_grid.Add (new ImplicitGrid(i));
		}
		Debug.Log ("Hello");
		Debug.Log (subscribers.Count);
		foreach (var shopper in subscribers) {
			ImplicitCell start = getRandomFreeCell (0);
			ImplicitCell goal = getRandomFreeCell (window_size - 1);
			Debug.Log ("Start shopper");
			shopper.path = A_star (start, goal);
		}
	}

	//A* by D.Silver (Cooperative Pathfinding)
	public List<ImplicitCell> A_star(ImplicitCell start, ImplicitCell goal){
		// Init all data structures
		List<ImplicitCell> closed_set = new List<ImplicitCell> ();
		List<ImplicitCell> open_set = new List<ImplicitCell> ();
		open_set.Add (start);
		Dictionary<ImplicitCell, ImplicitCell> comeFrom = new Dictionary<ImplicitCell, ImplicitCell> ();
		Dictionary<ImplicitCell, float> g_score = new Dictionary<ImplicitCell, float> ();
		Dictionary<ImplicitCell, float> f_score = new Dictionary<ImplicitCell, float> ();
		foreach (var grid in space_time_grid) {
			foreach (var cell in grid.grid) {
				comeFrom.Add ((ImplicitCell)cell, null);
				g_score.Add ((ImplicitCell)cell, Mathf.Infinity);
				f_score.Add ((ImplicitCell)cell, Mathf.Infinity);
			}
		}
		g_score [start] = 0f;
		f_score [start] = h (start, goal);
		int reached_depth = 0;


		//Algorithm
		while(open_set.Count != 0){
			ImplicitCell current = getMinScore (f_score, open_set);
			if (current.Equals(goal)) {
				return reconstructPath(current, comeFrom);
			}
			open_set.Remove (current);
			closed_set.Add (current);

			if (current.t > reached_depth) {
				reached_depth = current.t;
				if (reached_depth == window_size - 1) {
					return reconstructPath(current, comeFrom);
				}
			}

			foreach (var neighbor in getNeighbors(current)) {
				if(closed_set.Contains(neighbor)){
					continue;
				}
				if(!open_set.Contains(neighbor)){
					open_set.Add (neighbor);
				}

				float score = g_score [current] + 1;
				if (score >= g_score [neighbor]) {
					continue;
				}
				comeFrom [neighbor] = current;
				g_score [neighbor] = score;
				f_score [neighbor] = score + h (neighbor, goal);
			}
		}

		return null;
	}

	private float h(ImplicitCell start, ImplicitCell goal){
		return Vector2.Distance (start.position, goal.position);
	}

	private List<ImplicitCell> reconstructPath(ImplicitCell current, Dictionary<ImplicitCell, ImplicitCell> comeFrom){
		List<ImplicitCell> path = new List<ImplicitCell> ();
		ImplicitCell lastCell = current;
		while (current != null) {
			current.isPath = true;
			current.isObstacle = true;
			path.Add (current);
			current = comeFrom [current];
		}
		path.Reverse ();
		for (int t = path.Count; t < window_size; t++) {
			ImplicitCell temp = (ImplicitCell) space_time_grid [t].grid[lastCell.x, lastCell.y];
			temp.isObstacle = true;
			path.Add (temp);
		}
		return path;
	}

	private ImplicitCell getMinScore(Dictionary<ImplicitCell, float> f_score, List<ImplicitCell> open_set){
		ImplicitCell result = open_set [0];
		foreach(var cell in open_set){
			if(f_score[cell] < f_score[result]){
				result = cell;
			}
		}
		return result;
	}

	private List<ImplicitCell> getNeighbors(ImplicitCell cell){
		List<ImplicitCell> neighbors = new List<ImplicitCell> ();
		if (isInsideGrid (cell.x, cell.y + 1, cell.t+1) && !space_time_grid[cell.t+1].grid[cell.x, cell.y + 1].isObstacle) {
			neighbors.Add ((ImplicitCell)space_time_grid[cell.t+1].grid[cell.x, cell.y + 1]);
		}
		if (isInsideGrid (cell.x, cell.y - 1, cell.t+1) && !space_time_grid[cell.t+1].grid[cell.x, cell.y - 1].isObstacle) {
			neighbors.Add ((ImplicitCell)space_time_grid[cell.t+1].grid[cell.x, cell.y - 1]);
		}
		if (isInsideGrid (cell.x + 1, cell.y, cell.t+1) && !space_time_grid[cell.t+1].grid[cell.x + 1, cell.y].isObstacle) {
			neighbors.Add ((ImplicitCell)space_time_grid[cell.t+1].grid[cell.x + 1, cell.y]);
		}
		if (isInsideGrid (cell.x - 1, cell.y, cell.t+1) && !space_time_grid[cell.t+1].grid[cell.x - 1, cell.y].isObstacle) {
			neighbors.Add ((ImplicitCell)space_time_grid[cell.t+1].grid[cell.x - 1, cell.y]);
		}
		if (isInsideGrid (cell.x, cell.y, cell.t+1) && !space_time_grid[cell.t+1].grid[cell.x, cell.y].isObstacle) {
			neighbors.Add ((ImplicitCell)space_time_grid[cell.t+1].grid[cell.x, cell.y]);
		}
		return neighbors;
	}

	private bool isInsideGrid(int x, int y, int t){
		return x > 0 && y > 0 && t >= 0 &&
			x < space_time_grid[t].grid.GetLength (0) && y < space_time_grid[t].grid.GetLength (1) && t < window_size;
	}

	private ImplicitCell getRandomFreeCell(int t){
		int x = Random.Range (0, space_time_grid[0].grid.GetLength(0));
		int y = Random.Range (0, space_time_grid[0].grid.GetLength(1));
		ImplicitCell result = (ImplicitCell)space_time_grid [t].grid[x, y];
		while (result.isObstacle) {
			x = Random.Range (0, space_time_grid[0].grid.GetLength(0));
			y = Random.Range (0, space_time_grid[0].grid.GetLength(1));
			result = (ImplicitCell)space_time_grid [t].grid[x, y];
		}
		result.isObstacle = true;
		return result;
	}
		

}
