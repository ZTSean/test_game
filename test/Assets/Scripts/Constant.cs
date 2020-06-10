using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant {
    public const string SCENE_NAME_CAFE = "cafe";
    public const string SCENE_NAME_FACTORY = "factory";
    public const string SCENE_NAME_DROM = "dorm";
    public const string SCENE_NAME_MAIN = "main";

    public const int SCENE_INDEX_CAFE = 0;
    public const int SCENE_INDEX_FACTORY = 1;
    public const int SCENE_INDEX_DORM = 2;
    public const int SCENE_INDEX_MAIN = 3;
    public static readonly int[] SCENE_INDEXES = { SCENE_INDEX_CAFE, SCENE_INDEX_FACTORY, SCENE_INDEX_DORM, SCENE_INDEX_MAIN };
}
