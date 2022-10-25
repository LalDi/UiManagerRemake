using System.Collections.Generic;
using UnityEngine;

namespace BlitzyUI.UIExample
{
    public class ExampleGameManager : MonoBehaviour
    {
        public static ExampleGameManager Instance { get; private set; }
        public static UIManager uiManger { get; private set; }

        // Screen Ids
        public static readonly Screen.Id ScreenId_Empty = new Screen.Id("Empty");
        public static readonly Screen.Id ScreenId_ExampleMenu = new Screen.Id("ExampleMenu");
        public static readonly Screen.Id ScreenId_ExamplePopup = new Screen.Id("ExamplePopup");
        public static readonly Screen.Id ScreenId_ExampleMvp = new Screen.Id("ExampleMvp");

        public List<ScreenKeyData> listScreenKey;
        public Canvas rootCanvas;

        private void Awake() 
        {
            DontDestroyOnLoad(this);

            if (Instance == null) {
                Instance = this;
            }
            if (uiManger == null)
			{
                uiManger = new UIManager();
                uiManger.Init(rootCanvas, listScreenKey);
			}
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Start() {
            // Push the example menu screen immediately.
            uiManger.QueuePush(ScreenId_ExampleMenu, null, EScreenKey.EXAMPLEMENU, null);
        }
    }
}
