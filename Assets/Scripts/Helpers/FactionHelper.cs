using Components;
using HECSFramework.Core;

namespace Helpers
{
    public static class FactionHelper
    {
        public static int GetHealthSum(int factionId)
        {
            var sum = 0;
            var characters = EntityManager.Default.GetFilter<FactionComponent>();
            characters.ForceUpdateFilter();
            foreach (var character in characters)
            {
                if(character.GetComponent<FactionComponent>().FactionIdentifier.Id != factionId)
                    continue;
                sum += (int)character.GetComponent<HealthComponent>().Value;
            }
            return sum;
        }
         
    }
}