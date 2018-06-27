using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public static class LevelData
	{

		private static int currentCost;
		private static int currentIteration = 1;
		private static List<string> inspectedPotions = new List<string>();
	private static List<PotionPathIndexPair> executedPotionPathIndexPairs = new List<PotionPathIndexPair>();
	private static int currentActivePath;

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

	public static void addExecutedPotion(PotionPathIndexPair potionIndexPair){
		executedPotionPathIndexPairs.Add (potionIndexPair);
		}

	public static bool isPotionExecuted(PotionPathIndexPair potionIndexPair){
		return (executedPotionPathIndexPairs.Contains (potionIndexPair));
		}

	public static void setCurrentActivePath(int pathIndex){
		currentActivePath = pathIndex;
	}

	public static int getCurrentActivePath(){
		return currentActivePath;
	}

	public static List<PotionPathIndexPair> getExecutedPotionPathIndexPairs(){
		return executedPotionPathIndexPairs;
	}
}

