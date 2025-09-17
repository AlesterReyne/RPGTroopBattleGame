namespace RPGTroopBattleGame;

public static class SpecialAbilities
{
    // === Mage Ability ===
    public static void Freeze(Character target)
    {
        // Freeze target for 2 turns
        target.IsFrozen = true;
        target.FrozenTurnsRemaining = 2;

        Console.WriteLine($"{target.Name} is frozen and cannot act for {target.FrozenTurnsRemaining} turns!");
    }


    // === Rogue Ability ===
    public static void Poison(Character target)
    {
        // Poison target for 3 turns, 5 damage per turn
        target.IsPoisoned = true;
        target.PoisonTurnsRemaining = 3;
        target.PoisonDamagePerTurn = 5;

        Console.WriteLine(
            $"{target.Name} is poisoned and will take {target.PoisonDamagePerTurn} damage each turn for {target.PoisonTurnsRemaining} turns!");
    }


    // === Priest Ability ===
    public static void Heal(Character player, int amount)
    {
        int healAmount = amount;
        int previousHp = player.CurrentHp;
        player.CurrentHp += healAmount;

        Console.WriteLine($"{player.Name} is healed for {healAmount} HP! ({previousHp} HP → {player.CurrentHp} HP)");
    }


    // === Knight Ability ===
    public static void Shield(Character target)
    {
        // Shield lasts 3 turns, absorbs 10 damage per hit
        target.IsShielded = true;
        target.ShieldTurnsRemaining = 3;
        target.ShieldStrength = 10;

        Console.WriteLine(
            $"{target.Name} is shielded and will absorb {target.ShieldStrength} damage from each attack for {target.ShieldTurnsRemaining} turns!");
    }


    // === Berserk Ability ===
    public static void Rage(Character player)
    {
        // Rage lasts 3 turns, adds +10 attack
        player.IsEnraged = true;
        player.RageTurnsRemaining = 3;
        player.RageAttackBoost = 10;

        Console.WriteLine(
            $"{player.Name} enters a rage state! Attack power increased by {player.RageAttackBoost} for {player.RageTurnsRemaining} turns!");
    }


    // === Witch Ability ===
    public static void Debuff(Character target)
    {
        // Debuff lasts 3 turns, reduces defense by 5
        target.IsDebuffed = true;
        target.DebuffTurnsRemaining = 3;
        target.DefenseReduction = 5;

        Console.WriteLine(
            $"{target.Name}'s defense is reduced by {target.DefenseReduction} for {target.DebuffTurnsRemaining} turns!");
    }


    // === Ability Descriptions for Menus ===
    public static string GetDescription(string className)
    {
        return className switch
        {
            "Mage"    => "Freeze enemy for 2 turns (cannot act).",
            "Rogue"   => "Poison enemy for 3 turns (5 damage per turn).",
            "Priest"  => "Restore HP to an ally instantly.",
            "Knight"  => "Absorb 10 damage per hit for 3 turns.",
            "Berserk" => "Increase attack power by +10 for 3 turns.",
            "Witch"   => "Reduce enemy defense by 5 for 3 turns.",
            _         => "Unknown ability"
        };
    }
}
