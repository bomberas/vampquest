using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour {

	public int points;
	public int heal;

	PlayerControl player;

	void Start () {
		player = FindObjectOfType<PlayerControl> ();	
	}
	
	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			player.CollectBonusItem (heal, points);
			gameObject.SetActive (false);
		}
	}

}
