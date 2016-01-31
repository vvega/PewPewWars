using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpellProgress {
	public Spell spell;
	public string[] casterNames;
	public int currentLine = 1;
	public float timeLeft;

	public SpellProgress(Spell s, string c)
	{
		spell = s;
		casterNames = new string[spell.lines.Length];
		casterNames[0] = c;
	}

	public bool IsNextLine(string words)
	{
		return (spell.lines[currentLine].words == words);
	}

	public bool IncrementSpellLine(string username, string words)
	{
		Line line = spell.lines[currentLine];
		if (line.words == words) {
			if ((line.HasConstraint() && casterNames[line.playerConstraint] == username)
				|| (!line.HasConstraint() && casterNames[currentLine-1] != username)) {
					casterNames[currentLine] = username;
					currentLine++;
					return true;
			}
		}
		return false;
	}

	public bool SpellComplete()
	{
		return (currentLine >= spell.lines.Length);
	}
}
