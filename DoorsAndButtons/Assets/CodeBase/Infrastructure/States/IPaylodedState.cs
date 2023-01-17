using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}