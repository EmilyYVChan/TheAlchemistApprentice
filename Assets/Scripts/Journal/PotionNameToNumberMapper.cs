using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PotionNameToNumberMapper {

    public static Dictionary<string, int> myDict = new Dictionary<string, int>
        {
            { "PinkPotion", 1 },
            { "RedPotion", 2 },
            { "YellowPotion", 3 },
            { "BluePotion", 4 }
        };

    public static int getPotionNumber(string potionName)
    {
        return myDict[potionName];
    }
}
