using UnityEngine;
using System.Collections.Generic;

public class Incant : ChatCommand {

    public override void ProcessCommand(GameMaster gameMaster, string username, string parameters)
	{
		GameObject playerObj = gameMaster.GetPlayerObject(username);
		if (playerObj == null)
			return;

		Player playerComponent = playerObj.GetComponent<Player>();
		Team team = playerComponent.getTeam();

        // Check in-progress spells
        for (int i = 0; i < team.inProgressSpells.Count; ++i)
        {
            SpellProgress s = team.inProgressSpells[i];
            if (s.IsNextLine(parameters) == false)
                continue;

            s.IncrementSpellLine(username);
            if (s.SpellComplete())
            {
                s.spell.Cast();
                team.inProgressSpells.Remove(s);
            }

            return;
        }

        // Start a new spell
        Spell[] spells = GetComponents<Spell>();
        foreach (Spell s in spells)
        {
            if (s.lines[0].words != parameters)
                continue;

            team.inProgressSpells.Add(new SpellProgress(s, username));
            break;
        }
	}
}
