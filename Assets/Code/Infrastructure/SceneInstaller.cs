using System.Collections;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<Game>().AsSingle();
	}
}
