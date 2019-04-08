using System.Collections;
using System.Collections.Generic;
using Assets.Code.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CellPresenter : MonoBehaviour
{
	public Button CellButton;
	public Text CellText;

	[Inject]
	public void Initialize(Game gameModel)
	{
		CellButton.onClick.AddListener(() => gameModel.MarkX(0, 0));

		gameModel.Events
			.OfType<GameEvent, XMarkedEvent>()
			.Subscribe(_ => CellText.text = "X");
	}
}
