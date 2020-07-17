using System.Collections.Generic;

public class Reward
{
    public int materialGain;
    public int energyGain;
    public Dictionary<Item, int> items;

    public Reward()
    {
        this.materialGain = 0;
        this.energyGain = 0;
        this.items = new Dictionary<Item, int>();
    }

    public Reward(int materialGain, int energyGain, Dictionary<Item, int> items)
    {
        this.materialGain = materialGain;
        this.energyGain = energyGain;
        this.items = items;
    }
}
