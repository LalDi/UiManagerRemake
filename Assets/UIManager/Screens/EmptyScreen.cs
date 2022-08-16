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
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
            Debug.Log($"[{key}] Play OnHiding");
        }

        public override void OnReFocus()
        {
            Debug.Log($"[{key}] Play OnReFocus");
        }

        public override void OnFocusLost()
        {
            Debug.Log($"[{key}] Play OnFocusLost");
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