using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitGrid {

	int t {
		get;
		set;
	}

	public ImplicitCell[,] grid {
		get;
		set;
	}
		

	public ImplicitGrid(int t){
		this.t = t;
		initalize_grid ();
		initialize_shops ();
		initialize_stairs ();
	}

	public void initalize_grid(){
		int total_lenght = 2 * shops_length + 2 * empty_space_lenght + stairs_length;
		grid = new ImplicitCell[total_lenght, number_shops * shop_width];
		//Initalize grid
		for (int x = 0; x < total_lenght; x++) {
			for (int y = 0; y < number_shops * shop_width; y++) {
				ImplicitCell new_cell = new ImplicitCell ();
				new_cell.isObstacle = false;
				new_cell.isPath = false;
				new_cell.t = this.t;
				new_cell.x = x;
				new_cell.y = y;
				grid [x, y] = new_cell;
			}
		}
	}

	protected static int empty_space_lenght = 5;
	public static int stairs_length = 3;
	protected static int shops_length = 3;
	protected static int shop_width = 6;
	protected static int number_shops = 6;


	//Grid initialization
	private void initialize_shops(){
		for (int y = 0; y < grid.GetLength(1); y++) {
			if (y % shop_width == 0) {
				for (int x = 0; x < shops_length; x++) {
					grid [x, y].isObstacle = true;
				}
			} else if (y % shop_width != shop_width - 1) {
				grid [shops_length - 1, y].isObstacle = true;
			}
		}

		for (int y = 0; y < grid.GetLength(1); y++) {
			if (y % shop_width == 0) {
				for (int x = shops_length+2*empty_space_lenght+stairs_length; x < grid.GetLength(0); x++) {
					grid [x, y].isObstacle = true;
				}
			} else if (y % shop_width != shop_width - 1) {
				grid [grid.GetLength(0) - shops_length, y].isObstacle = true;
			}
		}
	}

	private void initialize_stairs(){
		int space_btw_stairs = Mathf.FloorToInt ((number_shops * shop_width) / 4.0f);
		for (int y = 0; y < grid.GetLength(1); y++) {
			if (y % space_btw_stairs != 4) {
				for (int x = shops_length + empty_space_lenght; x < shops_length + empty_space_lenght + stairs_length; x++) {
					grid [x, y].isObstacle = true;
				}
			}
		}
	}
}
