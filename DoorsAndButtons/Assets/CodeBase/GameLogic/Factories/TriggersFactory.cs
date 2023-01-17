using System;
using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Lifecycle;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Enums;
using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Factories
{
    public class TriggersFactory
    {
        public static int Create(EcsWorld world, LevelConfig.ButtonTriggerConfig config)
        {
            return Create(world, config.ButtonID, config.ActionType);
        }

        public static int Create(EcsWorld world, string configButtonID, TriggerActionTypes configActionType)
        {
            var entity = world.NewEntity();

            var buttonTriggerPool = world.GetPool<ButtonTrigger>();
            var levelSuccessPool = world.GetPool<LevelSuccessOnActivatedTrigger>();
            var levelFailedPool = world.GetPool<LevelFailedOnActivatedTrigger>();
            var buttonLinkPool = world.GetPool<ButtonLinkRequest>();

            buttonTriggerPool.Add(entity);

            ref var buttonLinkRequest = ref buttonLinkPool.Add(entity);
            buttonLinkRequest.ButtonID = configButtonID;
            
            switch (configActionType)
            {
                case TriggerActionTypes.LevelSuccess:
                    levelSuccessPool.Add(entity);
                    break;
                case TriggerActionTypes.LevelFailed:
                    levelFailedPool.Add(entity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(configActionType), configActionType, null);
            }

            return entity;
        }
    }
}