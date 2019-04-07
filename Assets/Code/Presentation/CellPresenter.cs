using System.Collections;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CellPresenter : MonoBehaviour
{
	public Button Button;

	[Inject]
	public void Initialize(Game gameModel)
	{
		Debug.Log(gameObject.name);
	}
}
