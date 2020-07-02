using System.Collections.Generic;

public class Reward
{
    public int energy;
    public Dictionary<Constant.Item, int> items;

    public Reward()
    {
        this.energy = 0;
        this.items = new Dictionary<Constant.Item, int>();
    }

    public Reward(int energy, Dictionary<Constant.Item, int> items)
    {
        this.energy = energy;
        this.items = items;
    }
}
