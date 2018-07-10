using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public static class LevelData
	{
		private static int totalCost;
		private static int currentMana;
		private static int currentIteration = 1;
		private static List<string> inspectedPotions = new List<string>();
		private static List<PotionPathIndexPair> executedPotionPathIndexPairs = new List<PotionPathIndexPair>();
		private static int currentActivePath;

		public static void setCurrentMana(int mana)
		{
			currentMana = mana;
		}

		public static int getCurrentMana()
		{
			return currentMana;
		}

		public static int getCurrentIteration()
		{
			return currentIteration;
		}

		public static void addCost(int increment)
		{
			currentMana += increment;
			totalCost += (increment * -1);
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

	public static int GetTotalCost(){
		return totalCost;
	}
}

