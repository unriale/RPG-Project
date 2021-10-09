using UnityEngine;
using System;
using System.Collections.Generic;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        //characterClasses [0, 1, 2, 3]
        //                  | 
        //                   -> Player, stats [0, 1]
        //                                     |
        //                                      -> Health, [10, 20, 30, 40, 50]
        public float GetStat(Stat stat, CharacterClass characterClass, int startingLevel)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            if (levels.Length < 0) return 0;
            return levels[startingLevel - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            return lookupTable[characterClass][stat].Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats) 
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionCharacterClass.characterClass] = statLookupTable;
            }
        }

        [Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] public CharacterClass characterClass;
            [SerializeField] public ProgressionStat[] stats;
        }

        [Serializable]
        class ProgressionStat
        {
            [SerializeField] public Stat stat;
            [SerializeField] public float[] levels;
        }
    }
}