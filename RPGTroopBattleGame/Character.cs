public class Character
{
    // Basic character properties
    private string _name;
    private string _characterClass;

    // Stats properties
    private int _maxHp;
    private int _currentHp;
    private int _attackPower;
    private int _defensePower;
    private int _criticalHitChance;
    private int _damageMultiplier;

    // Level and experience properties
    private int _currentLevel;
    private int _currentExp;
    private int _expForLevelUp;

    // Status effect properties
    public bool IsFrozen { get; set; }
    public int FrozenTurnsRemaining { get; set; }

    public bool IsPoisoned { get; set; }
    public int PoisonTurnsRemaining { get; set; }
    public int PoisonDamagePerTurn { get; set; }

    public bool IsShielded { get; set; }
    public int ShieldTurnsRemaining { get; set; }
    public int ShieldStrength { get; set; }

    public bool IsEnraged { get; set; }
    public int RageTurnsRemaining { get; set; }
    public int RageAttackBoost { get; set; }

    public bool IsDebuffed { get; set; }
    public int DebuffTurnsRemaining { get; set; }
    public int DefenseReduction { get; set; }

    // Basic properties accessors
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string CharacterClass
    {
        get { return _characterClass; }
        set { _characterClass = value; }
    }

    // Stats properties accessors
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            if (_currentHp < 0)
                _currentHp = 0;
            if (_currentHp > _maxHp)
                _currentHp = _maxHp;
        }
    }

    public int AttackPower
    {
        get { return _attackPower; }
        set { _attackPower = value; }
    }

    public int DefensePower
    {
        get { return _defensePower; }
        set { _defensePower = value; }
    }

    public int CriticalHitChance
    {
        get { return _criticalHitChance; }
        set { _criticalHitChance = value; }
    }

    public int DamageMultiplier
    {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    // Constructor
    public Character(string name, int maxHp, int attackPower, int defence, int criticalHitChance,
        int damageMultiplier, string characterClass = "")
    {
        Name = name;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        AttackPower = attackPower;
        DefensePower = defence;
        CriticalHitChance = criticalHitChance;
        DamageMultiplier = damageMultiplier;
        _currentLevel = 1;
        _currentExp = 0;
        _expForLevelUp = 1;
        CharacterClass = characterClass;

        InitializeStatusEffects();
    }

    // Combat methods
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

    public int GetDefence()
    {
        Random random = new Random();
        return random.Next(0, DefensePower + 1);
    }

    // Experience and leveling methods
    public void EnemyDefeated(int enemyMaxHp)
    {
        AddExperience();
        RecoverHp(enemyMaxHp);
    }

    public void AddExperience()
    {
        _currentExp++;
        if (_currentExp >= _expForLevelUp)
        {
            _currentLevel++;
            _currentExp = 0;
            _expForLevelUp *= 2;
            Console.WriteLine($"{_name} leveled up to level {_currentLevel}!");
        }
    }

    public void RecoverHp(int enemyMaxHp)
    {
        int recoveryValue = enemyMaxHp / 100 * 20;
        CurrentHp += recoveryValue;
    }

    // Helper methods
    private void InitializeStatusEffects()
    {
        IsFrozen = false;
        FrozenTurnsRemaining = 0;
        IsPoisoned = false;
        PoisonTurnsRemaining = 0;
        PoisonDamagePerTurn = 0;
        IsShielded = false;
        ShieldTurnsRemaining = 0;
        ShieldStrength = 0;
        IsEnraged = false;
        RageTurnsRemaining = 0;
        RageAttackBoost = 0;
        IsDebuffed = false;
        DebuffTurnsRemaining = 0;
        DefenseReduction = 0;
    }

    // String representation methods
    public override string ToString()
    {
        string statusEffects = "";
        if (IsFrozen) statusEffects += " [Frozen]";
        if (IsPoisoned) statusEffects += " [Poisoned]";
        if (IsShielded) statusEffects += " [Shielded]";
        if (IsEnraged) statusEffects += " [Enraged]";
        if (IsDebuffed) statusEffects += " [Debuffed]";

        return
            $"Name: {_name}\nClass: {_characterClass}\nHP: {_currentHp}\nAttack: {_attackPower}\nDefence: {_defensePower}\nCritical: {_criticalHitChance}{statusEffects}\nExp:{_currentLevel}";
    }

    public string FinalState()
    {
        return
            $"Name: {_name}\nClass: {_characterClass}\nHP: {_currentHp}\nLevel: {_currentLevel}\n Loot: (in progress)";
    }
}