using UnityEngine;

public class AcidCone : Spell {

	public override void Cast (Team myTeam, Team enemyTeam) {
		enemyTeam.TakeDamage(damage);
		Debug.Log("PFFFFFFFTTTTTTTTTTTTTTTTTTTTTTTTTTT. Butts.");
	}
}
