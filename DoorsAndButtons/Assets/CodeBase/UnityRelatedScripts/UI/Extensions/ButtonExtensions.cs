using UnityEngine.UI;

namespace CodeBase.UnityRelatedScripts.UI.Extensions
{
    public static class ButtonExtensions
    {
        public static ButtonAwaiter GetAwaiter(this Button button) => 
            new ButtonAwaiter(button);
    }
}