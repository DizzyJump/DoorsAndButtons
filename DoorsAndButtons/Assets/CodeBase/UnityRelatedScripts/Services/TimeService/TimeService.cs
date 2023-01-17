using UnityEngine;

namespace CodeBase.Services.TimeService
{
    public class TimeService : ITimeService
    {
        private float scale = 1f;
        public float DeltaTime => Time.deltaTime * scale;

        public void SetScale(float newScale) => 
            scale = newScale;
    }
}