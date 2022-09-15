using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0649

namespace BlitzyUI.UIExample
{
    public class ExampleMenuScreen : Window
    {
        public Text headerLabel;
        public Button buttonA;
        public Button buttonB;
        public Button buttonC;
        public Button buttonD;
        public GameObject dimming;

        private string nextHeader;

        public override void OnHierFixed()
        {
            // Run one-time setup operations here.
            Debug.Log($"[{key}] Play OnHierFixed");

            buttonA.onClick.AddListener(HandleButtonAClicked);
            buttonB.onClick.AddListener(HandleButtonBClicked);
            buttonC.onClick.AddListener(HandleButtonCClicked);
            buttonD.onClick.AddListener(HandleButtonDClicked);
        }

        public override void OnShowing(Data data)
        {
            Debug.Log($"[{key}] Play OnSetData");

            headerLabel.text = "Click on a button...";

            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
            EndInAnim();
        }

        public override void OnHiding()
        {
            Debug.Log($"[{key}] Play OnHiding");

            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
            EndOutAnim();
        }

        public override void OnReFocus()
        {
            Debug.Log($"[{key}] Play OnReFocus");

            headerLabel.gameObject.SetActive(true);
            dimming.gameObject.SetActive(false);

            headerLabel.text = nextHeader;
        }

        public override void OnFocusLost()
        {
            Debug.Log($"[{key}] Play OnFocusLost");

			//headerLabel.gameObject.SetActive(false);
			dimming.gameObject.SetActive(true);
        }

        private void HandleButtonAClicked()
        {
            DisplayPopup("You clicked a button, good job!");
            nextHeader = "Button A clicked. Click another...";
        }

        private void HandleButtonBClicked()
        {
            DisplayPopup("Look at those button mashing skills!");
            nextHeader = "Button B clicked. Click another...";
        }

        private void HandleButtonCClicked()
        {
            DisplayPopup("Your a natural, do you think you could click another but with more pizzazz?");
            nextHeader = "Button C clicked. Click another...";
        }

        private void HandleButtonDClicked()
        {
            var screenData = new Data();

            GameManager.uiManger.QueuePush(GameManager.ScreenId_ExampleMvp, screenData, EScreenKey.EXAMPLEMVP);

            //DisplayPopup("If you keep clicking buttons like that, you are gonna put me out of the job!");
            //nextHeader = "Button D clicked. Click another...";
        }

        private void DisplayPopup(string message)
        {
            var screenData = new Data();
            screenData.Add(ExamplePopupScreen.dataKey, message);
            screenData.Add(EExamplePopupData.msg.ToString(), message);

			GameManager.uiManger.QueuePush(GameManager.ScreenId_ExamplePopup, screenData, EScreenKey.EXAMPLEPOPUP, (screen) =>
			{
                headerLabel.gameObject.SetActive(false);

                Debug.Log($"[{key}] Play Callback : End Push");
            });
        }

		public override void EndInAnim()
        {
            Debug.Log($"[{key}] Play InAnimEnd");
            ShowFinished();
            return;
		}

		public override void EndOutAnim()
        {
            Debug.Log($"[{key}] Play OutAnimEnd");
            HideFinished();
            return;
		}
	}
}

#pragma warning restore 0649