using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	float speed = 0.1f;

	bool active;
	//Vector3 myPosition;
	Transform myTransform;
	AudioSource enemyExplosion;

	void Awake () {
		myTransform = gameObject.transform;
		enemyExplosion = GetComponent<AudioSource>();
		ResetEnemy();
	}

	void Update () {
		if (active) {
			myTransform.Translate(Vector3.up * speed);

			if (myTransform.position.y < -12) {
				ResetEnemy();
			}
		}
	}

	void OnBecameInvisible () {
		ResetEnemy();
	}

	public void ResetEnemy () {
		active = false;
		myTransform.position = Vector3.zero;
		gameObject.SetActive(false);
	}

	public void SetEnemyActive (Vector3 newPosition) {
		active = true;
		myTransform.localPosition = newPosition;
		gameObject.SetActive(true);
	}

	public void DestroyEnemy () {
		GameController.instance.IncreaseScore();
		GameController.instance.PlayEnemyExplosion();
		ResetEnemy();
	}
}
