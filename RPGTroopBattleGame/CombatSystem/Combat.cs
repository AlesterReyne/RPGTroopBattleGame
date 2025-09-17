using RPGTroopBattleGame.LootSystem;

namespace RPGTroopBattleGame;

public static class Combat
{
    // === Basic Attack Resolution ===
    public static void Attack(Character attacker, Character defender)
    {
        // Enrage bonus (if attacker is enraged)
        int attackBoost = 0;
        if (attacker.IsEnraged)
        {
            attackBoost = attacker.RageAttackBoost;
        }

        // Base damage (+ rage bonus)
        int damage = attacker.GetDamage() + attackBoost;

        // Defence roll (reduced if debuffed)
        int defence = defender.GetDefence();
        if (defender.IsDebuffed)
        {
            defence -= defender.DefenseReduction;
            if (defence < 0) defence = 0;
        }

        // Net damage before shields
        int finalDamage = damage - defence;

        // Shield absorption (if any)
        if (defender.IsShielded && finalDamage > 0)
        {
            int shieldAbsorption = Math.Min(finalDamage, defender.ShieldStrength);
            finalDamage -= shieldAbsorption;
            Console.WriteLine($"{defender.Name}'s shield absorbs {shieldAbsorption} damage!");
        }

        // Clamp non-negative damage
        if (finalDamage < 0) finalDamage = 0;

        // Apply damage
        int previousHp = defender.CurrentHp;
        defender.CurrentHp -= finalDamage;

        // Combat log
        Console.WriteLine(
            $"{attacker.Name} attacks {defender.Name}.\n" +
            $"{previousHp} HP - ({damage} Damage - {defence} Defence) = {defender.CurrentHp} HP left.");

        // On defeat: award attacker and generate loot
        if (defender.CurrentHp <= 0)
        {
            Console.WriteLine($"{defender.Name} has been defeated!");

            // Attacker rewards (exp/heal based on enemyMaxHp in caller design; here using attacker's MaxHp as in original)
            attacker.EnemyDefeated(attacker.MaxHp);

            // Loot drop
            Item loot = LootGenerator.GenerateLoot();
            attacker.Inventory.AddItem(loot);
            Console.WriteLine(
                $"{loot.Name} dropped! By type: {loot.Type} - Rarity: {loot.Rarity} - Gold value: {loot.Value} - Description: {loot.Description}");
        }
    }


    // === Special Abilities Dispatch ===
    // Note: gating at level 2 as per original logic
    public static void UseSpecialAbility(Character player, Character target, string className, int requiredLevel)
    {
        if (requiredLevel < 2)
        {
            Console.WriteLine("You must be level 2 or higher to use this ability!");
            return;
        }

        switch (className)
        {
            case "Mage":
                // Freeze target
                SpecialAbilities.Freeze(target);
                break;
            case "Rogue":
                // Poison target
                SpecialAbilities.Poison(target);
                break;
            case "Witch":
                // Debuff target
                SpecialAbilities.Debuff(target);
                break;
            case "Knight":
                // Shield self
                SpecialAbilities.Shield(player);
                break;
            case "Priest":
                // Heal self (fixed 20 HP)
                SpecialAbilities.Heal(player, 20);
                break;
            case "Berserk":
                // Enrage self
                SpecialAbilities.Rage(player);
                break;
        }
    }


    // === Start-of-Turn Status Processing ===
    public static void ProcessStatusEffects(Character character)
    {
        // Poison: apply damage and tick duration
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

        // Frozen: decrement duration; while frozen, the unit cannot act (handled by caller turn logic)
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

        // Shield: decrement duration, then expire
        if (character.IsShielded)
        {
            character.ShieldTurnsRemaining--;
            if (character.ShieldTurnsRemaining <= 0)
            {
                character.IsShielded = false;
                Console.WriteLine($"{character.Name}'s shield has dissipated.");
            }
        }

        // Rage: decrement duration, then expire
        if (character.IsEnraged)
        {
            character.RageTurnsRemaining--;
            if (character.RageTurnsRemaining <= 0)
            {
                character.IsEnraged = false;
                Console.WriteLine($"{character.Name} is no longer enraged.");
            }
        }

        // Debuff: decrement duration, then expire
        if (character.IsDebuffed)
        {
            character.DebuffTurnsRemaining--;
            if (character.DebuffTurnsRemaining <= 0)
            {
                character.IsDebuffed = false;
                Console.WriteLine($"{character.Name} is no longer debuffed.");
            }
        }
    }
}