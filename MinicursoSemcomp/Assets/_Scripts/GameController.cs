using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField]
	GameObject PlayerObj, EnemyObj, GameOverScreen, GameStartScreen, HudTexts;
	[SerializeField]
	Transform EnemySpawnTransform;
	[SerializeField]
	int enemyPooledQuantity;
	[SerializeField]
	float positionXTemp;
	[SerializeField]
	TextMesh scoreText, playerLifeText, gameOverTotalScore, gameOverHighscore;

	bool gameOver, _gameStart;
	int i, score, enemyIndex, lastEnemySpawned, playerLife = 3;
	const int enemyScore = 100;

	GameObject _ShotsParent, EnemyTemp;
	Vector3 positionTemp;
	List <GameObject> enemiesList = new List<GameObject>();
	Enemy currentEnemy;
	AudioSource playerExplosion;

	public static GameController instance;

	public bool gameStart {
		get {
			return _gameStart;
		}
	}

	public Transform ShotsParent {
		get {
			if (_ShotsParent == null) {
				_ShotsParent = new GameObject("Shots Parent");
			}
			return _ShotsParent.transform;
		}
	}

	void Awake () {
		instance = this;
		scoreText.text = score.ToString();
		playerLifeText.text = playerLife.ToString();
		GameStartScreen.SetActive(true);
		GameOverScreen.SetActive(false);
		HudTexts.SetActive(false);
		playerExplosion = GetComponent<AudioSource>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyUp(KeyCode.Mouse1)) {
			if (!_gameStart && !gameOver) {
				GameStart();
			}

			if (gameOver) {
				UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
			}
		}
	}

	void PopulateEnemies () {
		enemiesList.Clear();
		for (i = 0; i < enemyPooledQuantity; i++) {
			EnemyTemp = (GameObject)Instantiate(EnemyObj, EnemySpawnTransform);
			enemiesList.Add(EnemyTemp);
			EnemyTemp.SetActive(false);
		}

		InvokeRepeating("SpawnEnemy", 1, 1);
	}

	void SpawnEnemy () {
		currentEnemy = enemiesList[enemyIndex].GetComponent<Enemy>();
		currentEnemy.ResetEnemy();
		positionTemp.x = Random.Range(-positionXTemp, positionXTemp);
		positionTemp.y = 0; //EnemySpawnTransform.position.y;
		positionTemp.z = 0;
		currentEnemy.SetEnemyActive(positionTemp);

		lastEnemySpawned++;
		enemyIndex = lastEnemySpawned % enemyPooledQuantity;
	}

	public void IncreaseScore () {
		score += enemyScore;
		scoreText.text = score.ToString();
	}

	public void LoseLife () {
		playerLife--;
		playerLifeText.text = playerLife.ToString();

		if (playerLife <= 0) {
			GameOver();
		}
	}

	public void GameStart () {
		PopulateEnemies();
		_gameStart = true;
		GameOverScreen.SetActive(false);
		GameStartScreen.SetActive(false);
		HudTexts.SetActive(true);
		PlayerObj.SetActive(true);

		lastEnemySpawned = 0;
		enemyIndex = 0;
		score = 0;
		playerLife = 3;

		scoreText.text = score.ToString();
		playerLifeText.text = playerLife.ToString();
	}

	public void GameOver () {
		_gameStart = false;
		gameOver = true;

		//Score
		gameOverTotalScore.text = "Score: " + score.ToString();
		if (PlayerPrefs.GetInt("Highscore", 0) < score) {
			PlayerPrefs.SetInt("Highscore", score);	
		}
		gameOverHighscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore",
		                                                            score).ToString();

		GameOverScreen.SetActive(true);
		HudTexts.SetActive(false);
		PlayerObj.SetActive(false);
		CancelInvoke();
		DestroyShots();

		for (i = 0; i < enemyPooledQuantity; i++) {
			Destroy(enemiesList[i]);
		}

		playerExplosion.Play();
	}

	void DestroyShots () {
		if (_ShotsParent != null) {
			Destroy(_ShotsParent);
		}
	}
}
