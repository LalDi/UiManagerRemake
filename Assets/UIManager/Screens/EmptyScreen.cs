using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0649

namespace BlitzyUI
{
    public class EmptyScreen : Window
    {
        public override void OnHierFixed()
        {
            // Run one-time setup operations here.
            Debug.Log($"[{key}] Play OnHierFixed");
        }

        public override void OnShowing(Data data)
        {
            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
            Debug.Log($"[{key}] Play OnSetData");
            EndInAnim();
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
            Debug.Log($"[{key}] Play OnHiding");
            EndOutAnim();
        }

        public override void OnReFocus()
        {
            Debug.Log($"[{key}] Play OnReFocus");
        }

        public override void OnFocusLost()
        {
            Debug.Log($"[{key}] Play OnFocusLost");
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