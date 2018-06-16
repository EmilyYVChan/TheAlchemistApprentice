using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {

    private static int currentCost;
    private static int currentIteration = 1;

    public static int getCurrentCost()
    {
        return currentCost;
    }

    public static int getCurrentIteration()
    {
        return currentIteration;
    }

    public static void addCost(int increment)
    {
        currentCost += increment;
    }

    public static void incrementIteration()
    {
        currentIteration++;
    }
}
