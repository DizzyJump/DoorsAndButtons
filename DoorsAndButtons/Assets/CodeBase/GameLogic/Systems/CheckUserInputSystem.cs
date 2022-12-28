using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Factories;
using CodeBase.GameLogic.LeoEcs;
using CodeBase.Services.InputService;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Systems
{
    public class CheckUserInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private IInputService inputService;
        private float3 moveToPosition;

        public CheckUserInputSystem(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void Init(IEcsSystems systems) => 
            world = systems.GetWorld();

        public void Run(IEcsSystems systems)
        {
            if (inputService.CheckUserInput(out moveToPosition)) 
                MovementRequestFactory.Create(world, moveToPosition);
        }
    }
}