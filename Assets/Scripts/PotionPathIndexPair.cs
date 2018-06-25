using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPathIndexPair : object {
    private string potionName;
    private int pathIndex;

    public PotionPathIndexPair(string potionName, int pathIndex)
    {
        this.potionName = potionName;
        this.pathIndex = pathIndex;
    }

	public override bool Equals(object obj)
    {
        // Check for null values and compare run-time types.
        if (obj == null || GetType() != obj.GetType())
            return false;

        PotionPathIndexPair potionPathIndexPair = (PotionPathIndexPair)obj;
        return (potionName.Equals(potionPathIndexPair.potionName)) && (pathIndex == potionPathIndexPair.pathIndex);
    }
}
