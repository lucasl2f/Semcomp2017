using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField]
	bool enemyShoot;

	float force;
	bool active;
	Transform myTransform, myParent;

	void Awake () {
		myTransform = gameObject.transform;
		myParent = myTransform.parent;
		ResetBullet();
	}

	public void ResetBullet () {
		active = false;
		myTransform.position = Vector3.zero;
		gameObject.SetActive(false);
	}

	public void SetBulletActive (Transform gunTransform, float bulletForce) {
		active = true;
		myTransform.position = gunTransform.position;
		myTransform.rotation = gunTransform.rotation;
		force = bulletForce;
		gameObject.SetActive(true);
		myTransform.SetParent(GameController.instance.ShotsParent);
	}

	void Update () {
		if (active) {
			myTransform.Translate(Vector3.up * force);
		}
	}

	void OnBecameInvisible () {
		ResetBullet();
	}

	void OnTriggerEnter (Collider other) {
		if (!enemyShoot) {
			if (other.CompareTag("Enemy")) {
				other.GetComponentInParent<Enemy>().DestroyEnemy();
				ResetBullet();
			}
		} else {
			if (other.CompareTag("Player")) {
				GameController.instance.LoseLife();
				ResetBullet();
			}

			if (other.CompareTag("Bullet")) {
				other.GetComponent<Bullet>().ResetBullet();
				ResetBullet();
			}
		}
	}
}
