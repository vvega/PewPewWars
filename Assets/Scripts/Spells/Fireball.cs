using UnityEngine;
using System.Collections;

public class Fireball : Spell {
	private static int numLines = 3;

	void Start () {
		this.spellName = "fireball";
		this.damage = 10;

		this.lines = new Line[numLines];
		lines[0] = new Line("one");
		lines[1] = new Line("two");
		lines[2] = new Line("FIREBALL!");
		this.casterNames = new string[numLines];
	}
	
	public override void Cast() {
		// TODO: deal damage
	}
}
