using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0649

namespace BlitzyUI.UIExample
{
    public enum EExamplePopupData
	{
        msg,
	}

    public class ExamplePopupScreen : Popup
    {
        public Text messageLabel;
        public Button okButton;

        public static readonly string dataKey = "message";

        public override void OnHierFixed()
        {
            // Run one-time setup operations here.
            okButton.onClick.AddListener(HandleOkClicked);

            Debug.Log($"[{key}] Play OnHierFixed");
        }

        public override void OnSetData(Data data)
        {
            messageLabel.text = data.Get<string>(dataKey);
            messageLabel.text = data.Get<string>(EExamplePopupData.msg.ToString());

            Debug.Log($"[{key}] Play OnSetData");

            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();

            Debug.Log($"[{key}] Play OnHiding");
        }

        private void HandleOkClicked()
        {
            UIManager.Instance.QueuePop(null);
        }

		public override void OnShowing()
        {
            Debug.Log($"[{key}] Play OnShowing");
            return;
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