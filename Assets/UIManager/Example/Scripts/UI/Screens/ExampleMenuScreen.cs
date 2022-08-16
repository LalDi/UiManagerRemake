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

        public override void OnHierFixed()
        {
            // Run one-time setup operations here.
            buttonA.onClick.AddListener(HandleButtonAClicked);
            buttonB.onClick.AddListener(HandleButtonBClicked);
            buttonC.onClick.AddListener(HandleButtonCClicked);
            buttonD.onClick.AddListener(HandleButtonDClicked);

            Debug.Log($"[{key}] Play OnHierFixed");
        }

        public override void OnShowing(Data data)
        {
            headerLabel.text = "Click on a button...";

            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();

            Debug.Log($"[{key}] Play OnSetData");
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();

            Debug.Log($"[{key}] Play OnHiding");
        }

        public override void OnReFocus()
        {
            headerLabel.gameObject.SetActive(true);

            Debug.Log($"[{key}] Play OnReFocus");
        }

        public override void OnFocusLost()
        {
            headerLabel.gameObject.SetActive(false);

            Debug.Log($"[{key}] Play OnFocusLost");
        }

        private void HandleButtonAClicked()
        {
            DisplayPopup("You clicked a button, good job!");
            headerLabel.text = "Button A clicked. Click another...";
        }

        private void HandleButtonBClicked()
        {
            DisplayPopup("Look at those button mashing skills!");
            headerLabel.text = "Button B clicked. Click another...";
        }

        private void HandleButtonCClicked()
        {
            DisplayPopup("Your a natural, do you think you could click another but with more pizzazz?");
            headerLabel.text = "Button C clicked. Click another...";
        }

        private void HandleButtonDClicked()
        {
            DisplayPopup("If you keep clicking buttons like that, you are gonna put me out of the job!");
            headerLabel.text = "Button D clicked. Click another...";
        }

        private void DisplayPopup(string message)
        {
            var screenData = new Data();
            screenData.Add(ExamplePopupScreen.dataKey, message);
            screenData.Add(EExamplePopupData.msg.ToString(), message);

			UIManager.Instance.QueuePush(GameManager.ScreenId_ExamplePopup, screenData, EScreenKey.EXAMPLEPOPUP, (screen) =>
			{
                Debug.Log($"[{key}] Play Callback : End Push");
            });
        }

		public override void InAnimEnd()
        {
            Debug.Log($"[{key}] Play InAnimEnd");
            return;
		}

		public override void OutAnimEnd()
        {
            Debug.Log($"[{key}] Play OutAnimEnd");
            return;
		}
	}
}

#pragma warning restore 0649