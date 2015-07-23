using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public float tolerance = 1.4f;
	public float minMorphTime = 0.5f;
	public float maxMorphTime = 1.6f;

	public GameObject gameBeginMsg;
	public GameObject gameOverMsg;
	public GameObject scoreMsg;

	PlayerController pc;
	Enemy[] enemyMorphers;
	float initialHeight;
	bool morphAllowed;
	IEnumerator morphTimer;

	Direction vDirection;

	float levelStartTime;
	bool firstTime;
	bool gameStarted;
	bool gameOver;

	void Start () {
		firstTime = true;
		gameStarted = false;

		pc = GameObject.FindObjectOfType<PlayerController>();
		pc.activeControls = false;
		initialHeight = pc._transform.localScale.y;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyMorphers = new Enemy[enemies.Length];
		for(int i = 0; i<enemies.Length; i++){
			enemyMorphers[i] = enemies[i].GetComponent<Enemy>();
		}
	}

	void Update(){
		if(firstTime && Input.GetKeyDown(KeyCode.P)){
			firstTime = false;
			gameBeginMsg.SetActive(false);
			StartGame();
		}
		if(gameOver && Input.GetKeyDown(KeyCode.R)){
			ResetMorphers();
			StartGame();
		}
		
		if(gameOver){
			return;
		}
		
		foreach(Enemy m in enemyMorphers){
			m.CustomUpdate();
		}
		pc.CustomUpdate();
		if(morphAllowed){
			CheckDifference();
		}
		
	}

	void StartGame(){
		vDirection = Direction.NONE;

		levelStartTime = Time.realtimeSinceStartup;
		gameStarted = true;
		gameOver = false;

		scoreMsg.SetActive(false);
		gameOverMsg.SetActive(false);

		pc.activeControls = true;

		morphAllowed = true;

		morphTimer = ActionRepeater(MorphEnemies, morphAllowed, Random.Range(minMorphTime, maxMorphTime));
//		morphTimer = MorphTimer();
		StartCoroutine(morphTimer);
	}

	void ResetMorphers(){
		ResetHeight(pc._transform);
		foreach(Enemy m in enemyMorphers){
			ResetHeight(m._transform);
		}
	}

	void ResetHeight(Transform t){
		t.localScale = new Vector3(t.localScale.x,
		                           initialHeight,
		                           t.localScale.z);
	}

	void CheckDifference(){
		if( Mathf.Abs(enemyMorphers[0].height - pc.height) > tolerance){
			GameOver();
		}
	}

	void GameOver(){
		gameOver = true;
		morphAllowed = false;
		StopCoroutine(morphTimer);

		gameOverMsg.SetActive(true);
		scoreMsg.SetActive(true);

		TextMesh textMsg = scoreMsg.GetComponent<TextMesh>();
		int seconds = Mathf.CeilToInt(Time.realtimeSinceStartup - levelStartTime);
		textMsg.text = (seconds != 1) ? "Game Over. You've lasted " + seconds + " seconds." : "Game Over. You've lasted " + seconds + " second.";

	}

	void MorphEnemies(){
		SetHeightDirection();
		foreach(Morpher m in enemyMorphers){
			m.vDirection = vDirection;
		}
	}
	
	void SetHeightDirection(){
		float heightChange = Random.value;
		if(heightChange > 0.5f){
			vDirection = Direction.INCREASE;
		}else{
			vDirection = Direction.DECREASE;
		}
	}

//	IEnumerator MorphTimer(){
//		while(morphAllowed){
//			float time = Random.Range(minMorphTime, maxMorphTime);
//			yield return new WaitForSeconds(time);
//			MorphEnemies();
//		}
//	}

	IEnumerator ActionRepeater(System.Action ActionToPerform, bool meanwhileTrue, float timeInterval){
		while(meanwhileTrue){
			yield return new WaitForSeconds(timeInterval);
			ActionToPerform();
		}
	}
}
