using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BlitzyUI.UIExample;
using UnityEngine.UI;

public class ExampleMvpScreen : Window
{
	public Button exitBtn;
	public float animTime;

    public override void OnHierFixed()
    {
		exitBtn?.onClick.AddListener(ClickExit);
		animTime = 0.5f;
    }

    public override void OnShowing(Data data)
    {
		transform.localPosition = new Vector3(0, -1000, 0);
		transform.DOLocalMove(Vector3.zero, animTime)
			.OnComplete(EndInAnim);
	}

    public override void EndInAnim()
    {
		ShowFinished();
    }

    public override void OnHiding()
    {
		transform.DOLocalMove(Vector3.up * -1000, animTime)
			.OnComplete(EndOutAnim);
    }

    public override void EndOutAnim()
    {
		HideFinished();
    }

    public override void OnFocusLost()
    {
    }

    public override void OnReFocus()
    {
    }

	public void ClickExit()
	{
		GameManager.uiManger.QueuePop();
	}
}
