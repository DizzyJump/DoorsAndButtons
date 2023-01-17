using System.Threading.Tasks;

namespace CodeBase.UnityRelatedScripts.UI
{
    public interface IOverlay
    {
        Task Show();
        void Destroy();
    }
}