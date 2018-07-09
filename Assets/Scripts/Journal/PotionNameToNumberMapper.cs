using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PotionNameToNumberMapper {

    public static Dictionary<string, int> myDict = new Dictionary<string, int>
        {
            { "PinkPotion", 1 },
            { "RedPotion", 2 },
            { "YellowPotion", 3 },
            { "BluePotion", 4 },
            { "LightPinkPotion", 5 },
            { "PurplePotion", 6 },
            { "PurplePotion1", 7 },
            { "PurplePotion2", 8 },
            { "GreenPotion", 9 },
            { "GreenPotion1", 10 },
            { "GreenPotion2", 11 },
            { "RedPotion1", 12 },
            { "RedPotion2", 13 },
            { "NavyBluePotion", 14 },
            { "DarkBluePotion", 15 },
            { "TurquoisePotion", 16 },
            { "DarkBluePotion", 15 },
            { "OrangePotion1", 17 },
            { "OrangePotion2", 18 },
            { "OrangePotion3", 19 },
            { "LightBrownPotion", 20 }
        };

    public static int getPotionNumber(string potionName)
    {
        return myDict[potionName];
    }
}
