using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Helpers;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

// System update MoveTo component on Doors according to they activity state
namespace CodeBase.GameLogic.Systems
{
    public class UpdateDoorMovingByDoorStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsWorld world;

        EcsFilter activatedDoorsFilter;
        EcsFilter deactivatedDoorsFilter;

        EcsPool<MoveTo> moveToPool;
        EcsPool<Position> positionPool;
        EcsPool<DoorSettings> doorSettingsPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            activatedDoorsFilter = world.Filter<Door>().Inc<DoorSettings>().Inc<Position>().Inc<Activated>().End();
            deactivatedDoorsFilter = world.Filter<Door>().Inc<DoorSettings>().Inc<Position>().Exc<Activated>().End();

            moveToPool = world.GetPool<MoveTo>();
            doorSettingsPool = world.GetPool<DoorSettings>();
            positionPool = world.GetPool<Position>();
        }

        public void Run(IEcsSystems systems)
        {
            ProcessActivatedDoors();
            ProcessDeactivatedDoors();
        }

        private void ProcessDeactivatedDoors()
        {
            foreach (var door in deactivatedDoorsFilter)
            {
                var targetPosition = doorSettingsPool.Get(door).ClosedPosition;
                SetMoveTo(targetPosition, door);
            }
        }

        private void ProcessActivatedDoors()
        {
            foreach (var door in activatedDoorsFilter)
            {
                var targetPosition = doorSettingsPool.Get(door).OpenPosition;
                SetMoveTo(targetPosition, door);
            }
        }

        void SetMoveTo(float3 targetPosition, int door)
        {
            var currentPosition = positionPool.Get(door).Value;
            bool isCorrectPosition = math.lengthsq(targetPosition - currentPosition) <= float.Epsilon;
            if (isCorrectPosition)
                return;

            ref MoveTo moveTo = ref EcsHelpers.GetOrAddComponent(door, moveToPool);
            moveTo.Value = targetPosition;
        }
    }
}
