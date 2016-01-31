using UnityEngine;

public class Fireball : Spell {
	public override void Cast(Team myTeam, Team enemyTeam) {
		enemyTeam.TakeDamage(damage);
        Debug.Log("BOOM");
    }
}
