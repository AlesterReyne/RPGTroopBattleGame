using RPGTroopBattleGame;
using RPGTroopBattleGame.LootSystem;

public class Character
{
    // === Basic Identity ===
    private string _name;
    private string _characterClass;

    // === Stats ===
    private int _maxHp;
    private int _currentHp;
    private int _attackPower;
    private int _defensePower;
    private int _criticalHitChance;
    private int _damageMultiplier;

    // === Level & Experience ===
    private int _currentLevel;
    private int _currentExp;
    private int _expForLevelUp;
    private float _levelModifier = 1f;

    // === Inventory ===
    public Inventory Inventory;

    // === Status Effects ===
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


    // === Properties ===
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

    public int MaxHp
    {
        get { return (int)(_attackPower * LevelModifier); } // note: currently scales with attackPower
        set { _maxHp = value; }
    }

    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            if (_currentHp < 0) _currentHp = 0;
            if (_currentHp > _maxHp) _currentHp = _maxHp;
        }
    }

    public int CurrentLevel => _currentLevel;

    public float LevelModifier
    {
        get { return _levelModifier; }
        set { _levelModifier = (float)(1 + 0.1 * _currentLevel); }
    }

    public int AttackPower
    {
        get { return (int)(_attackPower * LevelModifier); }
        set { _attackPower = value; }
    }

    public int DefensePower
    {
        get { return (int)(_defensePower * LevelModifier); }
        set { _defensePower = value; }
    }

    public int CriticalHitChance
    {
        get { return (int)(_criticalHitChance * LevelModifier); }
        set { _criticalHitChance = value; }
    }

    public int DamageMultiplier
    {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }


    // === Constructor ===
    public Character(string name, int maxHp, int attackPower, int defence, int criticalHitChance, string characterClass = "")
    {
        Name = name;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        AttackPower = attackPower;
        DefensePower = defence;
        CriticalHitChance = criticalHitChance;
        DamageMultiplier = 2;

        _currentLevel = 1;
        _currentExp = 0;
        _expForLevelUp = 1;
        CharacterClass = characterClass;

        Inventory = new Inventory();
        InitializeStatusEffects();
    }


    // === Combat Methods ===
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


    // === Experience & Leveling ===
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


    // === Helpers ===
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


    // === String Representations ===
    public override string ToString()
    {
        string statusEffects = "";
        if (IsFrozen) statusEffects += " [Frozen]";
        if (IsPoisoned) statusEffects += " [Poisoned]";
        if (IsShielded) statusEffects += " [Shielded]";
        if (IsEnraged) statusEffects += " [Enraged]";
        if (IsDebuffed) statusEffects += " [Debuffed]";

        return
            $"Name: {_name}\n" +
            $"Class: {_characterClass}\n" +
            $"HP: {_currentHp}\n" +
            $"Attack: {_attackPower}\n" +
            $"Defence: {_defensePower}\n" +
            $"Critical: {_criticalHitChance}{statusEffects}\n" +
            $"Exp: {_currentLevel}";
    }

    public string FinalState()
    {
        return
            $"Name: {_name}\n" +
            $"Class: {_characterClass}\n" +
            $"HP: {_currentHp}\n" +
            $"Level: {_currentLevel}\n" +
            $"Loot: (in progress)";
    }
}
