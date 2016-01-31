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
			if (!s.IncrementSpellLine(username, parameters))
				continue;
			
            if (s.SpellComplete()) {
				s.spell.Cast(team, gameMaster.GetOpposingTeam(team));
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

			SpellProgress newSpellProgress = new SpellProgress(s, username);

			// in case of one-liners:
			if (newSpellProgress.SpellComplete()) {
				newSpellProgress.spell.Cast(team, gameMaster.GetOpposingTeam(team));
			} else {
            	team.inProgressSpells.Add(newSpellProgress);
			}

            break;
        }
	}
}
