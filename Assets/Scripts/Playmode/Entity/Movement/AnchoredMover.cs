﻿using System;
using UnityEngine;

namespace Playmode.Entity.Movement
{
	public class AnchoredMover : MonoBehaviour
	{
		private Transform rootTransform;
		public static readonly Vector3 Forward = Vector3.up;
		[SerializeField] private float maxSpeed = 2f;

		public float MaxSpeed
		{
			get { return maxSpeed; }
			set { maxSpeed = value; }
		}

		private float currentSpeed = 2f;
		[SerializeField] protected float RotateSpeed = 90f;

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

		public void Rotate(float direction)
		{
			transform.RotateAround(
				rootTransform.position,
				Vector3.forward,
				(direction < 0 ? RotateSpeed : -RotateSpeed) * Time.deltaTime
			);
		}

		public void MoveRelativeToSelf(Vector3 direction)
		{
			rootTransform.Translate(direction.normalized * MaxSpeed * Time.deltaTime, Space.Self);
		}

		public float GetCurrentSpeed()
		{
			return currentSpeed;
		}

		public void SetCurrentSpeed(float speed)
		{
			currentSpeed = speed;
		}
	}
}