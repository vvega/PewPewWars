using UnityEngine;

public class Drain : Spell {

	public override void Cast (Team myTeam, Team enemyTeam) {
		enemyTeam.TakeDamage(damage);
		myTeam.TakeDamage(-damage/5);
		Debug.Log("Spoopy vampire spell");
	}
}
