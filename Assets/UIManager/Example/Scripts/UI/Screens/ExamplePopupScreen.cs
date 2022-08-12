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
        }

        public override void OnSetData(Data data)
        {
            messageLabel.text = data.Get<string>(dataKey);
            messageLabel.text = data.Get<string>(EExamplePopupData.msg.ToString());

            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
        }

        private void HandleOkClicked()
        {
            UIManager.Instance.QueuePop(null);
        }

		public override void OnShowing()
		{
			return;
		}

		public override void InAnimEnd()
		{
			return;
		}

		public override void OutAnimEnd()
		{
			return;
		}
	}
}

#pragma warning restore 0649