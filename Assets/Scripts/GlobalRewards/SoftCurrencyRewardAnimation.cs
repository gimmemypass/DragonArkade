using System;
using Commands;
using Components;
using HECSFramework.Core;
using UnityEngine;
using VFX;

namespace Systems.GlobalRewards
{
    public class SoftCurrencyRewardAnimation
    {
        private readonly CollectConfig rewardVisualConfig;
        
        private readonly ICounter<int> visualCounter;

        public SoftCurrencyRewardAnimation(CollectConfig collectConfig)
        {
            rewardVisualConfig = collectConfig;
            visualCounter = EntityManager.Default.GetSingleComponent<PlayerTagComponent>().Owner.GetComponent<SoftValueCounterComponent>();
        }

        public void ApplyRewardAnimationFrom(ApplyRewardVisualCommand command)
        {
            if (command.HideFX)
                IncrementCounterAndUpdateView(command.RewardAmount);
            else
                ApplyRewardAnimation(command.RewardAmount, command.Sender, command.AdditionalContext, IncrementCounterAndUpdateView);
        }
        
        public void ApplyRewardAnimation(int amount, Entity sender, AdditionalContext? additionalContext, Action<int> onIncreaseValue)
        {
            GameObject defaultWalletView = default;
            
            Vector3 toPosition = default;
            Vector3 fromPosition = GetFromPosition(sender);
            Vector3 size = default;
            
            if (TryGetUICounterView(out defaultWalletView))
            {
                toPosition = defaultWalletView.transform.position;
                size = ((RectTransform)defaultWalletView.transform).sizeDelta;
            }
            
            int canvasId = AdditionalCanvasIdentifierMap.TopCanvas;

            if (additionalContext != null)
            {
                if (additionalContext.Value.ViewToDraw != null)
                {
                    UnityRectTransformComponent rectTransformComponent =
                        additionalContext.Value.ViewToDraw.GetComponent<UnityRectTransformComponent>();
                    
                    toPosition = rectTransformComponent.RectTransform.position;
                    size = rectTransformComponent.RectTransform.sizeDelta;
                    
                }

                if (additionalContext.Value.CanvasId != null) canvasId = additionalContext.Value.CanvasId.Value;
                if (additionalContext.Value.Size != null) size = additionalContext.Value.Size.Value;
                if (additionalContext.Value.To != null) toPosition = additionalContext.Value.To.Value;
                if (additionalContext.Value.From != null) fromPosition = additionalContext.Value.From.Value;
            }

            int moneyPerItem = 4;
            int itemsCount = amount / moneyPerItem;
            itemsCount = Mathf.Clamp(itemsCount, 5, 500);

            var effectData = new EffectData
            {
                From = fromPosition,
                To = toPosition,
                VfxConfig = rewardVisualConfig,
                Size = size,
                VfxId = FXIdentifierMap.SoftValue,
                    
                //todo ���������� �� ������ ������ ����������, ����� ������ ������� ����� 50 � 100 � 500
                //ItemsCount = rewardViewComponent.GetItemsCount(amount),
                ItemsCount = itemsCount,
                Sender = sender,
                CanvasId = canvasId,
            };

            var moneyToAdd = amount / (float)itemsCount;
            var moneySoftRewardUpdater = new MultipleRewardCounterUpdater(moneyToAdd, onIncreaseValue);
            var moneySoftRewardCommand = new MultipleItemsFxCommand
            {
                EffectData = effectData,
                OnSingleItemComplete = () => { moneySoftRewardUpdater.OnSingleItemComplete(); },
                OnAllItemsComplete = () => { moneySoftRewardUpdater.OnAllItemsCompleted(); }
            };

            EntityManager.Default.Command(moneySoftRewardCommand);
        }

        private void IncrementCounterAndUpdateView(int amount)
        {
            IncrementSoftCurrencyCounter(amount);
        }
        
        private Vector3 GetFromPosition(Entity entity)
        {
            if (entity == null)
                return CenterOfScreen();

            // if (entity.TryGetComponent<VfxPositionFromColliderComponent>(out var component))
            // {
            //     var camera = EntityManager.Default.GetFilter<MainCameraComponent>().FirstOrDefault();
            //     return camera.GetComponent<CameraProviderComponent>().Get.WorldToScreenPoint(component.GetPosition());
            // }

            return CenterOfScreen();
        }
        
        private bool TryGetUICounterView(out GameObject softCurrency)
        {
            var filter = EntityManager.Default.GetFilter<SoftCurrencyCounterMonoComponentProviderComponent>();
            foreach (var softCurrencyUI in filter)
            {
                softCurrency = softCurrencyUI.GetComponent<SoftCurrencyCounterMonoComponentProviderComponent>().Get
                    .CoinsPoint.gameObject;
                return true;
            }

            softCurrency = default;
            return false;
        }

        private void IncrementSoftCurrencyCounter(int amount)
        {
            visualCounter.ChangeValue(amount);
        }
        
        
        private static Vector3 CenterOfScreen() =>
            new(Screen.width / 2f, Screen.height / 2f);
    }
}