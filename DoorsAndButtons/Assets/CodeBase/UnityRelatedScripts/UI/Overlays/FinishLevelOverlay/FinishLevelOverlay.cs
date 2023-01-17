using System.Threading.Tasks;
using CodeBase.UnityRelatedScripts.UI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UnityRelatedScripts.UI.Overlays.FinishLevelOverlay
{
    public class FinishLevelOverlay : MonoBehaviour, IOverlay
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI messageLabel;

        private string message;

        [Inject]
        void Contruct(string msg)
        {
            message = msg;
        }
        
        public async Task Show()
        {
            messageLabel.text = message;
            await closeButton;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<string, FinishLevelOverlay>
        {
        }
    }
}