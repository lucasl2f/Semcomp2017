using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Transform myTransform;
	Vector3 myPosition;

	[SerializeField]
	float movementX = 0.1f, screenLimitX;

	void Awake () {
		myTransform = gameObject.transform;
		myPosition = myTransform.position;
	}

	void Update () {
		if (GameController.instance.gameStart) {
			if (Input.GetKey(KeyCode.A)) {
				myPosition.x -= movementX;

				if (myPosition.x < -screenLimitX) {
					myPosition.x = -screenLimitX;
				}

				myTransform.position = myPosition;
			}

			if (Input.GetKey(KeyCode.D)) {
				myPosition.x += movementX;

				if (myPosition.x > screenLimitX) {
					myPosition.x = screenLimitX;
				}

				myTransform.position = myPosition;
			}
		}
	}
}
