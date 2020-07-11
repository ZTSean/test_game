using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant {
    // Scene names
    public const string SCENE_NAME_CAFE = "cafe";
    public const string SCENE_NAME_FACTORY = "factory";
    public const string SCENE_NAME_DROM = "dorm";
    public const string SCENE_NAME_MAIN = "main";

    // Scene indexes
    public const int SCENE_INDEX_CAFE = 0;
    public const int SCENE_INDEX_FACTORY = 1;
    public const int SCENE_INDEX_DORM = 2;
    public const int SCENE_INDEX_MAIN = 3;
    public static readonly int[] SCENE_INDEXES = { SCENE_INDEX_CAFE, SCENE_INDEX_FACTORY, SCENE_INDEX_DORM, SCENE_INDEX_MAIN };

    // Upgradation constants
    public const int UPGRADATION_DURATION_IN_MINS = 1;

    // Dorm constants
    public const int DORM_SANITY_RECOVER_LEVEL_1 = 3;
    public const int DORM_SANITY_RECOVER_LEVEL_2 = 6;

    // Factory constants
    public const int FACTORY_ENERGY_PRODUCE_LEVEL_1 = 3;
    public const int FACTORY_ENERGY_PRODUCE_LEVEL_2 = 6;
    public const int FACTORY_MATERIAL_COST_LEVEL_1 = -3;
    public const int FACTORY_MATERIAL_COST_LEVEL_2 = -6;
    public const int FACTORY_HUNGRY_COST_LEVEL_1 = -3;
    public const int FACTORY_HUNGRY_COST_LEVEL_2 = -6;
    public const int FACTORY_SANITY_COST_LEVEL_1 = -1;
    public const int FACTORY_SANITY_COST_LEVEL_2 = -2;

    // Cafe constants
    public const int CAFE_HUNGRY_RECOVER_LEVEL_1 = 3;
    public const int CAFE_HUNGRY_RECOVER_LEVEL_2 = 6;

    // Advanture constants
    public const int ADV_HUNGRY_COST_LEVEL_1 = 3;
    public const int ADV_HUNGRY_COST_LEVEL_2 = 6;
    public const int ADV_SANITY_COST_LEVEL_1 = 3;
    public const int ADV_SANITY_COST_LEVEL_2 = 6;

    // Avatar constants
    public const int AVATAR_MAX_HUNGRY_LEVEL_1 = 5;
    public const int AVATAR_MAX_SANITY_LEVEL_1 = 5;
    public const int AVATAR_MAX_HUNGRY_LEVEL_2 = 10;
    public const int AVATAR_MAX_SANITY_LEVEL_2 = 10;
    public const int AVATAR_LEVEL_1_TO_2_EXPERIENCE = 50;

    // Day State constants
    public const int DAY_STATE_LOOP_THRESHOLD = 3;
    public const int DAY_STATE_LOOP_HUNGRY_COST = -1;
    public const int DAY_STATE_LOOP_SANITY_COST = -1;
    public enum AvatarState
    {
         SLEEPING,
         WORKING,
         IDLING,
         EATING,
         ADVENTURE,
         LOW_HUNGRY,
         LOW_SANITY,
         DEAD
    }

    public enum DayState
    {
        MORNING,
        AFTERNOON,
        NIGHT
    }

    public enum Item
    {
        bibi,
        aiai,
        cici
    }
}
