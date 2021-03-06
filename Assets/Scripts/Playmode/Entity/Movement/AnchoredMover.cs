﻿using System;
using UnityEngine;

namespace Playmode.Entity.Movement
{
	public class AnchoredMover : MonoBehaviour
	{
		[SerializeField] private float maxSpeed = 2f;
		[SerializeField] protected float RotateSpeed = 90f;
		
		public static readonly Vector3 Forward = Vector3.up;

		private Transform rootTransform;
		
		private float currentSpeed;
		
		public float MaxSpeed
		{
			get { return maxSpeed; }
			set { maxSpeed = value; }
		}

		protected void Awake()
		{
			ValidateSerialisedFields();
			InitializeComponent();
		}

		private void ValidateSerialisedFields()
		{
			if (MaxSpeed < 0)
				throw new ArgumentException("Speed can't be lower than 0.");
			
			if (RotateSpeed < 0)
				throw new ArgumentException("RotateSpeed can't be lower than 0.");
		}

		private void InitializeComponent()
		{
			rootTransform = transform.root;
			currentSpeed = MaxSpeed;
		}

		public void MoveRelativeToSelf(Vector3 direction)
		{
			rootTransform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.Self);
		}

		public void SetCurrentSpeed(float speed)
		{
			currentSpeed = speed;
		}
	}
}