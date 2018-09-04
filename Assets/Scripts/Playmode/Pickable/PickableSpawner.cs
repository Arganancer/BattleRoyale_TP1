﻿using System;
using Playmode.Util.Collections;
using UnityEngine;
using Random = System.Random;

namespace Playmode.Pickable
{
	public class PickableSpawner : MonoBehaviour
	{
		private const string ZoneObjectName = "Zone";
		
		private ZoneController zoneController;

		private float worldSize;
		
		private Vector2 lastPickableCoordonate;
		private float minDistanceBetween2Pickable = 10;
		private int nbOfPickableToSpawn = 5;
		
		private float timeToSpawn = 100;
		private float timeLastSpawn;

		private static readonly TypePickable.TypePickable[] DefaultTypePickable =
		{
			TypePickable.TypePickable.Medicalkit,
			TypePickable.TypePickable.Shotgun,
			TypePickable.TypePickable.Uzi
		};

		[SerializeField] private GameObject pickablePrefab;

		private void Awake()
		{
			ValidateSerialisedFields();
			zoneController = GameObject.Find(ZoneObjectName).GetComponentInChildren<ZoneController>();
			worldSize = zoneController.CurrentRadius;
			timeLastSpawn = 0;
		}

		private void ValidateSerialisedFields()
		{
			if (pickablePrefab == null)
				throw new ArgumentException("Can't spawn null ennemy prefab.");
		}

		private void Start()
		{
			//SelectNbOfPickableToSpawn();
			SpawnPickables();
		}

		private void Update()
		{
			if (Time.time - timeLastSpawn > timeToSpawn && zoneController.ZoneIsNotShrinking)
			{
				//SelectNbOfPickableToSpawn();
				SpawnPickables();
				timeLastSpawn = Time.time;
			}
		}

		private void SpawnPickables()
		{
			var pickableTypeProvider = new LoopingEnumerator<TypePickable.TypePickable>(DefaultTypePickable);
			for (int i = 0; i < nbOfPickableToSpawn; ++i)
			{
				CreatePickable(
					CreateRandomCoordonate(),
					pickableTypeProvider.Next()
				);
			}
		}

		private Vector2 CreateRandomCoordonate()
		{
			int nbOfTry = 0;
			 Vector2 currentPickableCoordonate = new Vector2(UnityEngine.Random.Range(0, worldSize),
				UnityEngine.Random.Range(0, worldSize));
			while (Math.Abs(currentPickableCoordonate.x - lastPickableCoordonate.x) < minDistanceBetween2Pickable &&
			       Math.Abs(currentPickableCoordonate.y - lastPickableCoordonate.y) < minDistanceBetween2Pickable || 
			       nbOfTry <5)
			{
				currentPickableCoordonate = new Vector2(UnityEngine.Random.Range(-worldSize, worldSize),
					UnityEngine.Random.Range(-worldSize, worldSize));
				nbOfTry++;
			}

			lastPickableCoordonate = currentPickableCoordonate;
			return currentPickableCoordonate;
		}

		private void CreatePickable(Vector3 position, TypePickable.TypePickable strategy)
		{
			#if UNITY_EDITOR
			//position = transform.position;
			//strategy = TypePickable.TypePickable.Uzi;
			#endif
			
			GameObject test = Instantiate(pickablePrefab, position, Quaternion.identity);
				test.GetComponentInChildren<PickableController>()
				.Configure(strategy);
		}

		private void SelectNbOfPickableToSpawn()
		{
			nbOfPickableToSpawn = UnityEngine.Random.Range(1, 3);
		}
	}
}