namespace CodeBase.Services.TimeService
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        void SetScale(float newScale);
    }
}