using UnityEngine;
using System;
using RPG.Stats;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int startingLevel)
        {
            //characterClasses [0, 1, 2, 3]
            //                  | 
            //                   -> Player, stats [0, 1]
            //                                     |
            //                                      -> Health, [10, 20, 30, 40, 50]
            foreach (ProgressionCharacterClass progressionCharacter in characterClasses)
            {
                if (progressionCharacter.characterClass == characterClass)
                {
                    foreach (ProgressionStat progressionStat in progressionCharacter.stats)
                    {
                        if(progressionStat.stat == stat)
                        {
                            return progressionStat.levels[startingLevel-1];
                        }
                    }
                }
            }
            return 0;
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