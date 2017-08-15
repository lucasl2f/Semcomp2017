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
		//myPosition = myTransform.position;
	}

	void Update () {
		if (GameController.instance.gameStart) {
			if (Input.GetKey(KeyCode.A)) {
				if (myTransform.position.x - movementX >= -screenLimitX) {
					myTransform.Translate(-Vector3.right * movementX);
				}
			}

			if (Input.GetKey(KeyCode.D)) {
				if (myTransform.position.x - movementX <= screenLimitX) {
					myTransform.Translate(Vector3.right * movementX);
				}
			}
		}
	}
}
