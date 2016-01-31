using UnityEngine;

public class BloodMagic : Spell {

	public override void Cast (Team myTeam, Team enemyTeam) {
		enemyTeam.TakeDamage(damage);
		myTeam.TakeDamage(damage/2);
		Debug.Log("I HAVE THE SHINIEST MEAT BICYCLE!");
	}
}

