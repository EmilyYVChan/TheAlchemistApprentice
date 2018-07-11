using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JournalData
{
    private static List<string> inspectedPotionsByName = new List<string>();
    private static bool[] inspectedPotionsByNumber = new bool[20];
    public static bool hasNotInspectedAnyPotions = true;

    public static void addInspectedPotion(string potionName)
    {
        inspectedPotionsByName.Add(potionName);
        int potionNumber = PotionNameToNumberMapper.getPotionNumber(potionName);
        inspectedPotionsByNumber[potionNumber - 1] = true;
        hasNotInspectedAnyPotions = false;
    }

    public static bool isPotionInspected(string potionName)
    {
        return (inspectedPotionsByName.Contains(potionName));
    }

    public static bool[] getListOfInspectedPotionsThusFar()
    {
        return inspectedPotionsByNumber;
    }

    public static bool checkIfHasNotInspectedAnyComponents()
    {
        return hasNotInspectedAnyPotions;
    }
}
