using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: GridAbstract {
	public static Cell cellPrefab;

	public override void initalize_grid () {
		int total_lenght = 2 * shops_length + 2 * empty_space_lenght + stairs_length;
		grid = new Cell[total_lenght, number_shops * shop_width];
		//Initalize grid
		for (int x = 0; x < total_lenght; x++) {
			for (int y = 0; y < number_shops * shop_width; y++) {
				Cell new_cell = MonoBehaviour.Instantiate (cellPrefab) as Cell;
				new_cell.name = "Cell: x:" + x + " y:" + y;
				new_cell.x = x;
				new_cell.y = y;
				grid [x,y] = new_cell;
				new_cell.transform.position = new Vector3 (x*10, 0, y*10);
			}
		}
	}
}
