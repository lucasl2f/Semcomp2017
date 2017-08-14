using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	[SerializeField]
	float bulletForce, cooldown;

	Transform myTransform;

	bool gunShooting;
	float cooldownTemp;

	[SerializeField]
	GameObject BulletObj;
	[SerializeField]
	int pooledQuantity;
	[SerializeField]
	bool enemyGun;

	int i, activeBullet, bulletIndex;
	GameObject BulletTemp;

	List <GameObject> bulletsArsenal = new List<GameObject>();

	void Awake () {
		Populate();
	}

	void Start () {
		myTransform = gameObject.transform;
	}

	void Populate () {
		bulletsArsenal.Clear();
		for (i = 0; i < pooledQuantity; i++) {
			BulletTemp = (GameObject)Instantiate(BulletObj, gameObject.transform);
			bulletsArsenal.Add(BulletTemp);
		}
	}

	void Update () {
		if (GameController.instance.gameStart) {
			if (!enemyGun) { //PLAYER
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
					Shoot();	
				}

				
			} else { //ENEMY
				Shoot();
			}

			if (gunShooting) {
				cooldownTemp += Time.deltaTime;
				if (cooldownTemp > cooldown) {
					gunShooting = false;
					cooldownTemp = 0;
				}
			}
		}
	}

	public void Shoot () {
		if (!gunShooting) {
			GetActiveBullet().SetBulletActive(myTransform.position,
			                                  bulletForce);
			gunShooting = true;
		}
	}

	public Bullet GetActiveBullet () {
		bulletIndex = activeBullet % pooledQuantity;
		bulletsArsenal[bulletIndex].GetComponent<Bullet>().ResetBullet();

		activeBullet++;
			
		return 	bulletsArsenal[bulletIndex].GetComponent<Bullet>();
	}
}
