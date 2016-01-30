using UnityEngine;
using System.Collections.Generic;

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

    public void IncrementSpellLine(string username)
    {
        casterNames[currentLine] = username;
        currentLine++;
    }

    public bool SpellComplete()
    {
        return (currentLine >= spell.lines.Length);
    }
}

public class Incant : ChatCommand {
    public List<SpellProgress> inProgressSpells = new List<SpellProgress>();

    public override void ProcessCommand(GameMaster gameMaster, string username, string parameters)
	{
		GameObject playerObj = gameMaster.GetPlayerObject(username);
		if (playerObj == null)
			return;

        // Check in-progress spells
        for (int i = 0; i < inProgressSpells.Count; ++i)
        {
            SpellProgress s = inProgressSpells[i];
            if (s.IsNextLine(parameters) == false)
                continue;

            s.IncrementSpellLine(username);
            if (s.SpellComplete())
            {
                s.spell.Cast();
                inProgressSpells.Remove(s);
            }

            return;
        }

        // Start a new spell
        Spell[] spells = GetComponents<Spell>();
        foreach (Spell s in spells)
        {
            if (s.lines[0].words != parameters)
                continue;

            inProgressSpells.Add(new SpellProgress(s, username));
            break;
        }
	}
}
