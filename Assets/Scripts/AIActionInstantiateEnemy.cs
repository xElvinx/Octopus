using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionInstantiateEnemy : AIAction
{
	public Character enemy;
	public int maxInstances;

	private List<GameObject> _enemies;

	/// <summary>
	/// Initializes the action. Meant to be overridden
	/// </summary>
	public override void Initialization()
	{
		_enemies = new List<GameObject>();
		for (int i = 0; i < maxInstances; i++)
		{
			GameObject instance = Instantiate(enemy.gameObject);
			instance.SetActive(false);
			_enemies.Add(instance);
		}
	}
	
	public override void PerformAction()
	{
		for (int i = 0; i < _enemies.Count; i++)
		{
			if (!_enemies[i].activeSelf)
			{
				_enemies[i].SetActive(true);
				_enemies[i].transform.position = transform.position;
				_enemies[i].MMGetComponentNoAlloc<Health>().Revive();
				return;
			}
		}
	}
}
