
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            float health = 0;
            foreach (ProgressionCharacterClass givenClass in characterClasses)
            {
                if (characterClass != givenClass.characterClass) continue;
                health = givenClass.health[level];
                break;
            }
            return health;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }  
}
