namespace RPGTroopBattleGame.LootSystem;

public class Stats
{
    public int AttackBonus { get; set; }
    public int DefenseBonus { get; set; }
    public int HealthBonus { get; set; }
    public int CriticalChanceBonus { get; set; }

    public Stats()
    {
        AttackBonus = 0;
        DefenseBonus = 0;
        HealthBonus = 0;
        CriticalChanceBonus = 0;
    }
}