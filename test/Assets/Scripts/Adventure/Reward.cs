using System.Collections.Generic;

public class Reward
{
    public int energy;
    public Dictionary<Item, int> items;

    public Reward()
    {
        this.energy = 0;
        this.items = new Dictionary<Item, int>();
    }

    public Reward(int energy, Dictionary<Item, int> items)
    {
        this.energy = energy;
        this.items = items;
    }
}
