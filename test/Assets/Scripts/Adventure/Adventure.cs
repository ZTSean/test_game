using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure
{
    public int index;
    public string description;
    public int hungryCost;
    public int sanityCost;
    public int materialGain;
    public int energyGain;
    public int exprGain;
    public List<KeyValuePair<Item, int>> items = new List<KeyValuePair<Item, int>>();

    public Adventure(int index, string description, int hungryCost, int sanityCost, int materialGain, int energyGain, int exprGain, List<KeyValuePair<Item, int>> items)
    {
        this.index = index;
        this.description = description;
        this.hungryCost = hungryCost;
        this.sanityCost = sanityCost;
        this.materialGain = materialGain;
        this.energyGain = energyGain;
        this.exprGain = exprGain;
        this.items = items;
    }
}
