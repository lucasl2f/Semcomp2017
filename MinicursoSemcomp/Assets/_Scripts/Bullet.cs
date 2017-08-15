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

	void Update () {
		if (active) {
			myTransform.Translate(Vector3.up * force);
		}
	}

	void OnBecameInvisible () {
		ResetBullet();
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log(other.gameObject.name);
		if (!enemyShoot) {
			if (other.GetComponentInParent<Enemy>()) {
				other.GetComponentInParent<Enemy>().DestroyEnemy();
				ResetBullet();
			}
		} else {
			if (other.GetComponent<Bullet>()) {
				other.GetComponent<Bullet>().ResetBullet();
				ResetBullet();
			}

			if (other.transform.parent.name == "Player") {
				GameController.instance.LoseLife();
				ResetBullet();
			}


		}
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
}
