using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public int score = 0;

	void Update () {
		GetComponent<GUIText>().text = "Score: " + score;
	}

}
