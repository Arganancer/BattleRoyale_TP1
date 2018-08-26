﻿using Playmode.Entity.Movement;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Npc.BodyParts;
using Playmode.Npc.Strategies.BaseStrategies;

namespace Playmode.Npc.Strategies
{
	public class CamperBehavior : BaseNpcBehavior
	{
		public CamperBehavior(Mover mover, HandController handController, HitSensor hitSensor, Health health,
			NpcSensorSight npcSensorSight, NpcSensorSound npcSensorSound) : base(mover, handController, hitSensor,
			health, npcSensorSight, npcSensorSound)
		{
		}

		protected override void DoIdle()
		{
			throw new System.NotImplementedException();
		}

		protected override void DoRoaming()
		{
			throw new System.NotImplementedException();
		}

		protected override void DoEngaging()
		{
			throw new System.NotImplementedException();
		}

		protected override void DoInvestigating()
		{
			throw new System.NotImplementedException();
		}

		protected override void DoAttacking()
		{
			throw new System.NotImplementedException();
		}

		protected override void DoRetreating()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateIdle()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateRoaming()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateInvestigating()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateEngaging()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateAttacking()
		{
			throw new System.NotImplementedException();
		}

		protected override State EvaluateRetreating()
		{
			throw new System.NotImplementedException();
		}
	}
}