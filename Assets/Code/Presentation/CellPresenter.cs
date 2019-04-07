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

	public int X;
	public int Y;

	[Inject]
	public void Initialize(Game gameModel)
	{
		Button.onClick.AddListener(() => gameModel.Mark(X, Y));

		gameModel.Events
			.OfType<GameEvent, XMarkedEvent>()
			.Where(e => e.X == X && e.Y == Y)
			.Subscribe(_ => Text.text = "X");

		gameModel.Events
			.OfType<GameEvent, OMarkedEvent>()
			.Where(e => e.X == X && e.Y == Y)
			.Subscribe(_ => Text.text = "O");
	}
}
