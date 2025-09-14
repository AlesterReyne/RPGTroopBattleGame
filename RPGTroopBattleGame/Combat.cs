namespace RPGTroopBattleGame;

public static class Combat
{
    public static Character Attack(Character attacker, Character defender)
    {
        // Check if attacker is enraged
        int attackBoost = 0;
        if (attacker.IsEnraged)
        {
            attackBoost = attacker.RageAttackBoost;
        }

        int damage = attacker.GetDamage() + attackBoost;

        // Calculate defense, considering debuff
        int defence = defender.GetDefence();
        if (defender.IsDebuffed)
        {
            defence -= defender.DefenseReduction;
            if (defence < 0) defence = 0;
        }

        int finalDamage = damage - defence;

        // Apply shield if active
        if (defender.IsShielded && finalDamage > 0)
        {
            int shieldAbsorption = Math.Min(finalDamage, defender.ShieldStrength);
            finalDamage -= shieldAbsorption;
            Console.WriteLine($"{defender.Name}'s shield absorbs {shieldAbsorption} damage!");
        }

        if (finalDamage < 0)
            finalDamage = 0;

        int previousHp = defender.CurrentHp;
        defender.CurrentHp -= finalDamage;

        Console.WriteLine(
            $"{attacker.Name} attacks {defender.Name}.\n" +
            $"{previousHp} HP - ({damage} Damage - {defence} Defence) = {defender.CurrentHp} HP left.");
        return defender;
    }

    // You can add special ability methods here in the future
    // For example:

    public static void UseSpecialAbility(Character player, Character target, string className)
    {
        // Implement special abilities based on character class
        switch (className)
        {
            case "Mage":
                // Freeze ability
                SpecialAbilities.Freeze(target);
                break;
            case "Rogue":
                // Poison ability
                SpecialAbilities.Poison(target);
                break;
            case "Witch":
                // Debuff ability
                SpecialAbilities.Debuff(target);
                break;
            case "Knight":
                // Shield ability
                SpecialAbilities.Shield(player);
                break;
            case "Priest":
                // Heal ability
                SpecialAbilities.Heal(player, 20); // Heal for 20 HP
                break;
            case "Berserk":
                // Rage ability
                SpecialAbilities.Rage(player);
                break;
        }
    }

    // Process status effects at the start of a character's turn
    public static Character ProcessStatusEffects(Character character)
    {
        // Process poison damage
        if (character.IsPoisoned)
        {
            int previousHp = character.CurrentHp;
            character.CurrentHp -= character.PoisonDamagePerTurn;
            Console.WriteLine(
                $"{character.Name} takes {character.PoisonDamagePerTurn} poison damage! ({previousHp} HP → {character.CurrentHp} HP)");

            character.PoisonTurnsRemaining--;
            if (character.PoisonTurnsRemaining <= 0)
            {
                character.IsPoisoned = false;
                Console.WriteLine($"{character.Name} is no longer poisoned.");
            }
        }

        // Check frozen status
        if (character.IsFrozen)
        {
            character.FrozenTurnsRemaining--;
            if (character.FrozenTurnsRemaining <= 0)
            {
                character.IsFrozen = false;
                Console.WriteLine($"{character.Name} is no longer frozen.");
            }
            else
            {
                Console.WriteLine(
                    $"{character.Name} is frozen and cannot act for {character.FrozenTurnsRemaining} more turns!");
            }
        }

        // Update shield duration
        if (character.IsShielded)
        {
            character.ShieldTurnsRemaining--;
            if (character.ShieldTurnsRemaining <= 0)
            {
                character.IsShielded = false;
                Console.WriteLine($"{character.Name}'s shield has dissipated.");
            }
        }

        // Update rage duration
        if (character.IsEnraged)
        {
            character.RageTurnsRemaining--;
            if (character.RageTurnsRemaining <= 0)
            {
                character.IsEnraged = false;
                Console.WriteLine($"{character.Name} is no longer enraged.");
            }
        }

        // Update debuff duration
        if (character.IsDebuffed)
        {
            character.DebuffTurnsRemaining--;
            if (character.DebuffTurnsRemaining <= 0)
            {
                character.IsDebuffed = false;
                Console.WriteLine($"{character.Name} is no longer debuffed.");
            }
        }

        return character;
    }
}