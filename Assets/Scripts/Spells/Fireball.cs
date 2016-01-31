using UnityEngine;

public class Fireball : Spell {

	[Header("Templates")]
	public GameObject castTemplate;

	public override void Cast(Team myTeam, Team enemyTeam) {
		Debug.Log("BOOM");
		enemyTeam.TakeDamage(damage);

		GameObject cast = Instantiate(castTemplate);
		cast.transform.position = myTeam.gameObject.transform.position;
		cast.GetComponent<SpellSling>().target = enemyTeam.gameObject;
    }
}
