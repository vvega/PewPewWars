using UnityEngine;

public class Drain : Spell {

	[Header("Templates")]
	public GameObject castTemplate;

	public override void Cast (Team myTeam, Team enemyTeam) {
		Debug.Log("Spoopy vampire spell");
		enemyTeam.TakeDamage(damage);
		myTeam.TakeDamage(-damage/5);

		GameObject cast = Instantiate(castTemplate);
		cast.transform.position = myTeam.gameObject.transform.position;
		cast.GetComponent<SpellSling>().target = enemyTeam.gameObject;
	}
}
