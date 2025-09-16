namespace RPGTroopBattleGame.LootSystem;

public class Inventory
{
    private List<Item> _items;
    private int _maxCapacity;
    private int _gold;

    public Inventory(int maxCapacity = 20)
    {
        _items = new List<Item>();
        _maxCapacity = maxCapacity;
        _gold = 0;
    }

    public void AddItem(Item item)
    {
        if (_items.Count >= _maxCapacity)
        {
            Console.WriteLine("Inventory is full! Cannot add more items.");
            return;
        }

        if (item.Type == ItemType.Gold)
        {
            _gold += item.Value;
            return;
        }

        _items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (item.Type == ItemType.Gold)
        {
            _gold -= item.Value;
            return;
        }

        _items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return _items.ToList();
    }

    public int GetGold()
    {
        return _gold;
    }
}