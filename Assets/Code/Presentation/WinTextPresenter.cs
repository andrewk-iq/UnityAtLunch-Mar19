using Assets.Code.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinTextPresenter : MonoBehaviour
{
	public Text WinText;

	[Inject]
	public void Initialize(Game gameModel)
	{
		gameModel.Events
			.OfType<GameEvent, XWinsEvent>()
			.Subscribe(_ => WinText.text = "X Wins");

		gameModel.Events
			.OfType<GameEvent, OWinsEvent>()
			.Subscribe(_ => WinText.text = "O Wins");
	}
}
