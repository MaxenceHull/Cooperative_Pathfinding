using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Cell cellPrefab;
	public Shopper shopperPrefab;
	public int stairs_length;
	public int shoppers_number = 3;
	private bool moveDone = false;
	Reservation reservationSystem;

	// Use this for initialization
	void Start () {
		Grid.cellPrefab = cellPrefab;
		GridAbstract.stairs_length = stairs_length;

		//Init the reservation system
		reservationSystem = Reservation.Instance;

		//Init shoppers
		for (int i = 0; i < shoppers_number; i++) {
			Instantiate (shopperPrefab);
		}


		//Init the "real" grid (ie: gameobjects)
		new Grid();
	}
	
	// Update is called once per frame
	void Update () {
		if (!moveDone) {
			reservationSystem.makeNextMove();
			moveDone = true;
		}
	}
		
}
