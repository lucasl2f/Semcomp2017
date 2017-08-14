using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	float speed = 0.1f;

	bool active;
	Vector3 myPosition;
	Transform myTransform;

	void Awake () {
		myTransform = gameObject.transform;
		myPosition = myTransform.position;
		ResetEnemy();
	}

	public void ResetEnemy () {
		active = false;
		myPosition = Vector3.zero;
		myTransform.position = myPosition;
		gameObject.SetActive(false);
	}

	public void SetEnemyActive (Vector3 newPosition) {
		active = true;
		myPosition = newPosition;
		myTransform.localPosition = myPosition;
		gameObject.SetActive(true);
	}

	public void DestroyEnemy () {
		GameController.instance.IncreaseScore();
		ResetEnemy();
	}

	void Update () {
		if (active) {
			myPosition.y += speed;
			myTransform.localPosition = myPosition;
		}
	}
}
