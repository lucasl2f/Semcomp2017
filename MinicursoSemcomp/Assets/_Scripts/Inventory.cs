using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField]
	GameObject BulletObj;
	[SerializeField]
	int initialBulletsQuantity, pooledQuantity;
	[SerializeField]
	Text bulletsQuantityText;

	int i, bulletsQuantity, activeBullet, bulletIndex;
	GameObject BulletTemp;

	List <GameObject> bulletsArsenal = new List<GameObject>();

	void Start () {
		bulletsQuantity = initialBulletsQuantity;
		bulletsQuantityText.text = bulletsQuantity.ToString();
		Populate();
	}

	void Populate () {
		bulletsArsenal.Clear();
		for (i = 0; i < pooledQuantity; i++) {
			BulletTemp = (GameObject)Instantiate(BulletObj, gameObject.transform);
			bulletsArsenal.Add(BulletTemp);
		}
	}

	public Bullet GetActiveBullet () {
		if (bulletsQuantity > 0) {
			bulletIndex = activeBullet % pooledQuantity;
			bulletsArsenal[bulletIndex].GetComponent<Bullet>().ResetBullet();

			bulletsQuantity--;
			activeBullet++;
			bulletsQuantityText.text = bulletsQuantity.ToString();

			return 	bulletsArsenal[bulletIndex].GetComponent<Bullet>();
		} else {
			return null;
		}
	}
}
