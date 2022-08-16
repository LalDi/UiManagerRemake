using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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

        private bool isCompleteAnim = false;

        public override void OnHierFixed()
        {
            Debug.Log($"[{key}] Play OnHierFixed");

            // Run one-time setup operations here.
            okButton.onClick.AddListener(HandleOkClicked);
        }

        public override void OnShowing(Data data)
        {
            Debug.Log($"[{key}] Play OnSetData");

            isCompleteAnim = false;

            messageLabel.text = data.Get<string>(dataKey);
            messageLabel.text = data.Get<string>(EExamplePopupData.msg.ToString());

            gameObject.transform.localScale = Vector3.zero;
            var cor = gameObject.transform.DOScale(Vector3.one, 5f)
                .OnComplete(EndInAnim);

            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
        }

        public override void OnHiding()
        {
            Debug.Log($"[{key}] Play OnHiding");

            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
            EndOutAnim();
        }

        private void HandleOkClicked()
        {
            if (isCompleteAnim == false) return;

            UIManager.Instance.QueuePop((id) =>
            {
                Debug.Log($"[{key}] Play Callback : End Pop");
            });
        }

		public override void EndInAnim()
        {
            Debug.Log($"[{key}] Play InAnimEnd");
            isCompleteAnim = true;
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