using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField]
	bool enemyShoot;

	float force;
	bool active;
	Vector3 myPosition;
	Transform myTransform, myParent;

	void Awake () {
		myTransform = gameObject.transform;
		myPosition = myTransform.position;
		myParent = myTransform.parent;
		ResetBullet();
	}

	public void ResetBullet () {
		active = false;
		myPosition = Vector3.zero;
		myTransform.position = myPosition;
		gameObject.SetActive(false);
		//myTransform.SetParent(myParent);
	}

	public void SetBulletActive (Vector3 pos, float bulletForce) {
		active = true;
		myPosition = pos;
		myTransform.position = myPosition;
		force = bulletForce;
		gameObject.SetActive(true);
		myTransform.SetParent(GameController.instance.ShotsParent);
	}

	void Update () {
		if (active) {
			myPosition.y += force;
			myTransform.position = myPosition;
		}
	}

	void OnBecameInvisible () {
		ResetBullet();
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log(other.tag);
		if (!enemyShoot) {
			if (other.CompareTag("Enemy")) {
				Debug.Log("Hit enemy");
				other.GetComponentInParent<Enemy>().DestroyEnemy();
				ResetBullet();
			}
		} else {
			if (other.CompareTag("Player")) {
				Debug.Log("Hit player");
				GameController.instance.LoseLife();
				ResetBullet();
			}	
		}
	}
}
