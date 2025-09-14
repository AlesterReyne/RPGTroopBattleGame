namespace RPGTroopBattleGame;

public static class SpecialAbilities
{
    public static void Freeze(Character target)
    {
        // Freeze the target for 2 turns
        target.IsFrozen = true;
        target.FrozenTurnsRemaining = 2;

        Console.WriteLine($"{target.Name} is frozen and cannot act for {target.FrozenTurnsRemaining} turns!");
    }

    public static void Poison(Character target)
    {
        // Poison the target for 3 turns
        target.IsPoisoned = true;
        target.PoisonTurnsRemaining = 3;
        target.PoisonDamagePerTurn = 5; // Base poison damage

        Console.WriteLine(
            $"{target.Name} is poisoned and will take {target.PoisonDamagePerTurn} damage each turn for {target.PoisonTurnsRemaining} turns!");
    }

    public static void Heal(Character player, int amount)
    {
        // Calculate healing amount
        int healAmount = amount;
        int previousHp = player.CurrentHp;
        player.CurrentHp += healAmount;

        Console.WriteLine($"{player.Name} is healed for {healAmount} HP! ({previousHp} HP → {player.CurrentHp} HP)");
    }

    public static void Shield(Character target)
    {
        // Apply shield for 3 turns
        target.IsShielded = true;
        target.ShieldTurnsRemaining = 3;
        target.ShieldStrength = 10; // Absorbs 10 damage per hit

        Console.WriteLine(
            $"{target.Name} is shielded and will absorb {target.ShieldStrength} damage from each attack for {target.ShieldTurnsRemaining} turns!");
    }

    public static void Rage(Character player)
    {
        // Boost attack for 3 turns
        player.IsEnraged = true;
        player.RageTurnsRemaining = 3;
        player.RageAttackBoost = 10; // +10 attack power

        Console.WriteLine(
            $"{player.Name} enters a rage state! Attack power increased by user.RageAttackBoost = 10; for {player.RageTurnsRemaining} turns!");
    }

    public static void Debuff(Character target)
    {
        // Reduce target's defense for 3 turns
        target.IsDebuffed = true;
        target.DebuffTurnsRemaining = 3;
        target.DefenseReduction = 5; // Reduce defense by 5

        Console.WriteLine(
            $"{target.Name}'s defense is reduced by {target.DefenseReduction = 5} for {target.DebuffTurnsRemaining} turns!");
    }
}