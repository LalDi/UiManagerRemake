using UnityEngine;

namespace BlitzyUI.UIExample
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        // Screen Ids
        public static readonly IScreen.Id ScreenId_Empty = new IScreen.Id("Empty");
        public static readonly IScreen.Id ScreenId_ExampleMenu = new IScreen.Id("ExampleMenu");
        public static readonly IScreen.Id ScreenId_ExamplePopup = new IScreen.Id("ExamplePopup");

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Start() {
            // Push the example menu screen immediately.
            UIManager.Instance.QueuePush(ScreenId_ExampleMenu, null, EScreenKey.EXAMPLEMENU, null);
        }
    }
}
