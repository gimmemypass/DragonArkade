using System;
using System.Collections;
using System.Linq;
using Commands;
using HECSFramework.Core;
using HECSFramework.Unity;
using HECSFramework.Unity.Helpers;
using Helpers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public sealed class AbilityUIButtonMonoComponent : MonoBehaviour
    {
        [SerializeField]
        [ValueDropdown("GetAbilities")]
        private int abilityId;
        
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable() => button.onClick.AddListener(TryEnableAbility);
        private void OnDisable() => button.onClick.RemoveListener(TryEnableAbility);

        private void TryEnableAbility()
        {
            var mainChar = EntityManager.GetSingleComponent<MainCharacterTagComponent>().Owner;
            var target = mainChar.GetComponent<TargetEntityComponent>().Target;
            if (AbilitiesHelper.CheckAbilityIsReady(mainChar, abilityId, target))
            {
                mainChar.Command(new ExecuteAbilityByIDCommand()
                {
                    AbilityIndex = abilityId,
                    Enable = true,
                    IgnorePredicates = true,
                    Owner = mainChar,
                    Target = target
                }); 
            }
        }
        
        private IEnumerable GetAbilities()
        {
            var nameToId = new ValueDropdownList<int>();
            var abilities = new SOProvider<EntityContainer>().GetCollection().Where(x => x.IsHaveComponent<AbilityTagComponent>());
            foreach (var ability in abilities)
            {
                nameToId.Add(ability.name, ability.ContainerIndex);  
            }
            return nameToId;
        }
    }
}