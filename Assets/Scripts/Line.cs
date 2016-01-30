using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
	// what they actually have to type
	public string words;

	// index of the caster it needs to repeat.
	// should we otherwise assume it needs to be different from the last caster?
	public int playerConstraint = -1;

	public Line(string words) {
		this.words = words;
	}

	public Line(string words, int playerConstraint) {
		this.words = words;
		this.playerConstraint = playerConstraint;
	}

	public bool HasConstraint() {
		return playerConstraint >= 0;
	}
}
