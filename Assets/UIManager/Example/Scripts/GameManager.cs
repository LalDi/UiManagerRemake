using UnityEngine;

namespace BlitzyUI.UIExample
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        // Screen Ids
        public static readonly ScreenOld.Id ScreenId_Empty = new ScreenOld.Id("Empty");
        public static readonly ScreenOld.Id ScreenId_ExampleMenu = new ScreenOld.Id("ExampleMenu");
        public static readonly ScreenOld.Id ScreenId_ExamplePopup = new ScreenOld.Id("ExamplePopup");

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
            UIManager.Instance.QueuePush(ScreenId_ExampleMenu, null, SCREENKEY.EXAMPLEMENU, null);
        }
    }
}
