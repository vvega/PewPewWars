using UnityEngine;

public class BloodMagic : Spell {

	[Header("Templates")]
	public GameObject castTemplate;

	public override void Cast (Team myTeam, Team enemyTeam) {
		Debug.Log("I HAVE THE SHINIEST MEAT BICYCLE!");
		enemyTeam.TakeDamage(damage);
		myTeam.TakeDamage(damage/2);

		GameObject cast = Instantiate(castTemplate);
		cast.transform.position = myTeam.gameObject.transform.position;
		cast.GetComponent<SpellSling>().target = enemyTeam.gameObject;
	}
}

