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
        }

        public override void OnSetData(IScreen.Data data)
        {
            // Be sure to call PushFinished to signal the end of the push.
            //PushFinished();
        }

        public override void OnHiding()
        {
            // Be sure to call PopFinished to signal the end of the pop.
            //PopFinished();
        }

        public override void OnReFocus()
        {
        }

        public override void OnFocusLost()
        {
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