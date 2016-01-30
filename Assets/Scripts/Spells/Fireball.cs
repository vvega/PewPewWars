using UnityEngine;
using System.Collections;

public class Fireball : Spell {
	
	void Start () {
		this.name = "Fireball";
		this.damage = 10;

		this.lines = new Line[3];
		lines[0] = new Line("one");
		lines[1] = new Line("two");
		lines[2] = new Line("FIREBALL!");
	}
	
	public override void Cast() {
		// TODO: deal damage
	}
}
