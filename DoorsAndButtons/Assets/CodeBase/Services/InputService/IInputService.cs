using Unity.Mathematics;

namespace CodeBase.Services.InputService
{
    public interface IInputService
    {
        bool CheckUserInput(out float3 moveToPosition);
    }
}