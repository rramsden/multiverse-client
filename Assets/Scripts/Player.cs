using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float playerSpeed = 0.03f;

	public bool isMoving = false;
	Vector3 currentPosition;

	void Start() {
		currentPosition = transform.position;
	}

	void Update () {
		updatePosition ();
	}

	private void updatePosition() {
		isMoving = currentPosition != transform.position;
		currentPosition = transform.position;

		float h = Input.GetAxis ("Horizontal") * playerSpeed;
		float v = Input.GetAxis ("Vertical") * playerSpeed;

		transform.Translate (h, 0, v);
	}

	private void positionToServer() {
		
	}
}
