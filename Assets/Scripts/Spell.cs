using UnityEngine;

public abstract class Spell : MonoBehaviour {
    public string spellName;
    public int damage;
    public float timeToCast;

    public Line[] lines;

	public abstract void Cast(Team myTeam, Team enemyTeam);
}
