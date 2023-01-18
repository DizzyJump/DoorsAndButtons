using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.UI
{
    // there are we use abstract class due to expect that in future appear and disappear animations could be implemented here
    public abstract class Overlay : MonoBehaviour
    {
        public abstract Task Show();
    }
}