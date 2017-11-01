using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour {

	public List<ImplicitCell> path;
	private List<ImplicitCell> currentPath;
	private static float timeFrame = 2f;
	// Use this for initialization
	void Start () {
		//StartCoroutine (startMoving());
		Reservation.Instance.subscribe(this);
	}

	// Update is called once per frame
	void Update () {
		if (path != null) {
			currentPath = path;
			path = null;
			StartCoroutine (startMoving());
		}
	}

	private IEnumerator moveOverSeconds (Vector3 end, float seconds) {
		float elapsedTime = 0;
		Vector3 startingPos = transform.position;
		while (elapsedTime < seconds) {
			transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		transform.position = end;
	}

	private IEnumerator stayOverSeconds (float seconds) {
		float elapsedTime = 0;
		while (elapsedTime < seconds) {
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator startMoving(){
		transform.position = currentPath[0].position3D;
		for (int i = 1; i < currentPath.Count; i++) {
			Debug.Log (currentPath [i].ToString());
			if (currentPath [i].position == currentPath [i - 1].position) {
				yield return StartCoroutine (stayOverSeconds (timeFrame));
			} else {
				yield return StartCoroutine (moveOverSeconds (currentPath [i].position3D, 2f));
			}
		}
		yield break;
	}

}


