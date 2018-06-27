using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPathIndexPair : object {
    private string potionName;
    private int pathIndex; // the id given to a specific control flow path
	private int IOIndex; // the index of the input outputs in the input output list of the potion

	public PotionPathIndexPair(string potionName, int pathIndex, int IOIndex)
    {
        this.potionName = potionName;
        this.pathIndex = pathIndex;
		this.IOIndex = IOIndex;
    }

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

	public int getIOIndex(){
		return IOIndex;
	}

	public string getPotionName(){
		return potionName;
	}
	public int getPathIndex(){
		return pathIndex;
	}
}
