using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public static class LevelData
	{

		private static int currentCost;
		private static int currentIteration = 1;
		private static List<string> inspectedPotions = new List<string>();
	private static int currentActivePath = -1;
	private static List<PotionPathIndexPair> executedPotionPathIndexPairs = new List<PotionPathIndexPair>();

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

		public static void addInspectedPotion(string potionName){
		inspectedPotions.Add (potionName);
		}
		
		public static bool isPotionInspected(string potionName){
		return (inspectedPotions.Contains (potionName));
		}

	public static void addExecutedPotion(string potionName){
		executedPotionPathIndexPairs.Add (new PotionPathIndexPair(potionName,currentActivePath));
		}

	public static bool isPotionExecuted(string potionName){
		return (executedPotionPathIndexPairs.Contains (new PotionPathIndexPair(potionName,currentActivePath)));
		}

	public static void setCurrentActivePath(int pathIndex){
		currentActivePath = pathIndex;
	}
}

