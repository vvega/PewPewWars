using UnityEngine;

public class MagicMissile : Spell {

	public override void Cast (Team myTeam, Team enemyTeam) {
		Debug.Log("I cast magic missile at the darkness!");
	}
}
