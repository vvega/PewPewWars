using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
	public Line[] lines;
	public string[] casterNames;

	public int damage = 0;

	public void Incant(string username, string words) {
		int stage = casterNames.Length;
		Line current_line = lines[stage];
		if (words.Equals(current_line.words)) {
			if (current_line.HasConstraint()) {
				if (username.Equals(casterNames[current_line.playerConstraint])) {
					casterNames[stage] = username;
				}
			}
		}
	}

	public bool IsReady() {
		return casterNames.Length.Equals(lines.Length);
	}

	// implementations need to reset progress
	// TODO: take in both teams as arguments, so the spells can apply their own effects
	public abstract void Cast();
}
