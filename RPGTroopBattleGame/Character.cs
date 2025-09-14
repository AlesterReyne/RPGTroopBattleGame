namespace RPGTroopBattleGame;

public class Character
{
    private string _name;

    public string Name
    {
        set { _name = value; }
        get { return _name; }
    }

    private int _currentHp;

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            if (_currentHp < 0)
                _currentHp = 0;
        }
    }

    private int _attackPower;

    public int AttackPower
    {
        get { return _attackPower; }
        set { _attackPower = value; }
    }

    private int _defensePower;

    public int DefensePower
    {
        set { _defensePower = value; }
        get { return _defensePower; }
    }

    public int GetDefence()
    {
        Random random = new Random();
        return random.Next(0, DefensePower + 1);
    }

    private int _criticalHitChance;

    public int CriticalHitChance
    {
        set { _criticalHitChance = value; }
        get { return _criticalHitChance; }
    }

    private int _damageMultiplier;

    public int DamageMultiplier
    {
        set { _damageMultiplier = value; }
        get { return _damageMultiplier; }
    }

    public int GetDamage()
    {
        Random random = new Random();
        if (random.Next(0, 101) < _criticalHitChance)
        {
            Console.WriteLine($"{_name} made a critical hit!");
            return _attackPower * _damageMultiplier;
        }

        return _attackPower;
    }

    // Special abilities status effects

    // Freeze effect
    public bool IsFrozen { get; set; }
    public int FrozenTurnsRemaining { get; set; }

    // Poison effect
    public bool IsPoisoned { get; set; }
    public int PoisonTurnsRemaining { get; set; }
    public int PoisonDamagePerTurn { get; set; }

    // Shield effect
    public bool IsShielded { get; set; }
    public int ShieldTurnsRemaining { get; set; }
    public int ShieldStrength { get; set; }

    // Rage effect
    public bool IsEnraged { get; set; }
    public int RageTurnsRemaining { get; set; }
    public int RageAttackBoost { get; set; }

    // Debuff effect
    public bool IsDebuffed { get; set; }
    public int DebuffTurnsRemaining { get; set; }
    public int DefenseReduction { get; set; }

    private int _levelUp;

    public int LevelUp
    {
        get { return _levelUp; }
    }

    private string _characterClass;

    public string CharacterClass
    {
        get { return _characterClass; }
        set { _characterClass = value; }
    }

    // Constructor update
    public Character(string name, int currentHp, int attackPower, int defence, int criticalHitChance,
        int damageMultiplier, string characterClass = "")
    {
        this.Name = name;
        this.CurrentHp = currentHp;
        this.AttackPower = attackPower;
        this.DefensePower = defence;
        this.CriticalHitChance = criticalHitChance;
        this.DamageMultiplier = damageMultiplier;
        this.CharacterClass = characterClass;

        // Initialize status effects
        this.IsFrozen = false;
        this.FrozenTurnsRemaining = 0;
        this.IsPoisoned = false;
        this.PoisonTurnsRemaining = 0;
        this.PoisonDamagePerTurn = 0;
        this.IsShielded = false;
        this.ShieldTurnsRemaining = 0;
        this.ShieldStrength = 0;
        this.IsEnraged = false;
        this.RageTurnsRemaining = 0;
        this.RageAttackBoost = 0;
        this.IsDebuffed = false;
        this.DebuffTurnsRemaining = 0;
        this.DefenseReduction = 0;

        // special abilities
        _levelUp = 0;
    }

    // Updated ToString to include class
    public override string ToString()
    {
        string statusEffects = "";
        if (IsFrozen) statusEffects += " [Frozen]";
        if (IsPoisoned) statusEffects += " [Poisoned]";
        if (IsShielded) statusEffects += " [Shielded]";
        if (IsEnraged) statusEffects += " [Enraged]";
        if (IsDebuffed) statusEffects += " [Debuffed]";

        return (
            $"Name: {_name}\nClass: {_characterClass}\nHP: {_currentHp}\nAttack: {_attackPower}\nDefence: {_defensePower}\nCritical: {_criticalHitChance}{statusEffects}\nExp:{_levelUp}");
    }

    // Update existing FinalState method
    public string FinalState()
    {
        return (
            $"Name: {_name}\nClass: {_characterClass}\nHP: {_currentHp}\nLevel: {_levelUp}\n Loot: (in progress)");
    }
}