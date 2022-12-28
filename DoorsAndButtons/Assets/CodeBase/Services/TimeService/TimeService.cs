using UnityEngine;

namespace CodeBase.Services.TimeService
{
    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}