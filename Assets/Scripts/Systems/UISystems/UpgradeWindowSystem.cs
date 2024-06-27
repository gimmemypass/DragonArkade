using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Core.Helpers;
using Helpers;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "upgrade window")]
    public sealed class UpgradeWindowSystem : BaseSystem, IHaveActor
    {
        [Required] public UpgradeWindowComponent UpgradeWindowComponent;
        public Actor Actor { get; set; }
        private UpgradeWindowMonoComponent monoComponent;
        private PlayerUpgradeComponent playerUpgradeComponent;
        private SoftValueCounterComponent softValueCounterComponent;
        private Dictionary<int, UpgradeSlotUIMonoComponent> slots = new();
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            AsSingle(ref playerUpgradeComponent);
            softValueCounterComponent = EntityManager.Default.GetSingleComponent<PlayerTagComponent>().Owner
                .GetComponent<SoftValueCounterComponent>();
            foreach (var upgradeData in UpgradeWindowComponent.UpgradeDatas)
            {
                var slot = Object.Instantiate(monoComponent.UpgradeSlotPrefab, monoComponent.ContentParent);
                var counterId = upgradeData.ModifierBluePrint.GetModifier().ModifierID;
                slots.AddOrReplace(counterId, slot);
                UpdateSlot(slot, upgradeData);
                slot.Button.onClick.AddListener(() => Upgrade(counterId));
            }
            if(monoComponent.CloseButton != null)
                monoComponent.CloseButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            EntityManager.Default.Command(new HideUICommand()
            {
                UIViewType = UIIdentifierMap.Upgrade_UIIdentifier
            });
        }

        private void UpdateSlotLevel(int counterId)
        {
            UpgradeSlotUIMonoComponent slot = slots[counterId];
            var value = playerUpgradeComponent.CountersUpgrades.GetValueOrDefault(counterId, 0);
            slot.LevelText.text = $"Lvl {(value + 1).ToString()}";
        }

        private void UpdateSlot(UpgradeSlotUIMonoComponent slot, UpgradeData upgradeData)
        {
            if (upgradeData.ModifierBluePrint.GetModifier() is not CounterLevelModifier modifier)
                throw new Exception();
            var counterId = modifier.ModifierID;
            slot.Icon.sprite = upgradeData.Icon;
            slot.NameText.text = upgradeData.Name;
            UpdateSlotLevel(counterId);
            slot.PriceText.text = modifier.GetPrice.ToString();
        }

        private void Upgrade(int counterId)
        {
            var upgradeData = UpgradeWindowComponent.UpgradeDatas.First(a =>
                a.ModifierBluePrint.GetModifier().ModifierID == counterId);
            var price = ((CounterLevelModifier)upgradeData.ModifierBluePrint.GetModifier()).GetPrice;
            if (softValueCounterComponent.Value >= price)
            {
                softValueCounterComponent.ChangeValue(-price);
                var level = playerUpgradeComponent.CountersUpgrades.GetValueOrDefault(counterId, 0);
                playerUpgradeComponent.CountersUpgrades.AddOrReplace(counterId, level + 1);
                UpdateSlot(slots[counterId], upgradeData);
                EntityManager.Default.Command(new LevelUpCommand(){CurrentLevel = playerUpgradeComponent.CurrentLevel});
            }
            else
            {
                HECSDebug.LogError("No enough money");
            }
        }
    }
}