using Assets.Code.Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartButtonPresenter : MonoBehaviour
{
	public Button RestartButton;

	[Inject]
	public void Initialize(Game gameModel)
	{
		RestartButton.onClick.AddListener(gameModel.Restart);
	}
}
