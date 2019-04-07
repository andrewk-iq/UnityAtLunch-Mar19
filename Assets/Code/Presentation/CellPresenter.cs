using System.Collections;
using System.Collections.Generic;
using Assets.Code.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CellPresenter : MonoBehaviour
{
	public Button Button;
	public Text Text;

	[Inject]
	public void Initialize(Game gameModel)
	{
		Button.onClick.AddListener(() => gameModel.MarkX(0, 0));

		gameModel.Events
			.OfType<GameEvent, XMarkedEvent>()
			.Subscribe(_ => Text.text = "X");
	}
}
