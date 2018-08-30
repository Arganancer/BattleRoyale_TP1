﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
	[SerializeField] float timeBufferToMoveZone = 10f;
	[SerializeField] private int startingRadiusZoneSize = 90;
	private float timeOfLastShrink = 0;
	private CircleCollider2D zoneCollider2D;
	[SerializeField] private int minimunSizeShrink = 10;
	[SerializeField] private int maximumSizeShrink = 30;
	private const float SPRITESCALESIZE = 0.25f;
	private const int MAXSHRINKINGSIZE = 10;
	private float nextRadius = 0;
	[SerializeField] private int sizeReduction = 2;
	private int shrinkSpeedBuffer = 0;
	private const int MAXSHRINKSPEEDBUFFER = 16;
	private GameObject zoneRenderer;
	private void Awake()
	{
		zoneCollider2D = transform.root.GetComponentInChildren<CircleCollider2D>();
		zoneRenderer = GameObject.Find("ZoneRenderer");
		zoneCollider2D.radius = startingRadiusZoneSize;
		nextRadius = startingRadiusZoneSize;
		zoneRenderer.transform.localScale = new Vector3(11.5f,11.5f,0);
	}

	private void Update()
	{
		ChangeZonePositionAndSize();
	}

	private void ChangeZonePositionAndSize()
	{
		if (Time.time - timeOfLastShrink > timeBufferToMoveZone && GetZoneRadius() <= nextRadius)
		{
			if (GetZoneRadius() > MAXSHRINKINGSIZE)
			{
				ChangeZoneColliderOffset();
			}

			timeOfLastShrink = Time.time;
		}
		else if (GetZoneRadius() > nextRadius)
		{
			ShrinkZone();
		}
	}

	void ShrinkZone()
	{
		if (shrinkSpeedBuffer > MAXSHRINKSPEEDBUFFER)
		{
			zoneCollider2D.radius -= sizeReduction;
			ChangeSpriteScale();
			ChangeSpritePosition();
			shrinkSpeedBuffer = 0;
		}

		shrinkSpeedBuffer++;
	}
	void ChangeZoneColliderOffset()
	{
		Vector2 offset = GetRandomZoneOffSetWithinCurrentCircle();
		zoneCollider2D.offset = offset;
	}


	float GetZoneRadius()
	{
		return zoneCollider2D.radius;
	}

	Vector2 GetRandomZoneOffSetWithinCurrentCircle()
	{
		nextRadius = GetZoneRadius() - GetRandomZoneRadiusSize();
		float maxOffset = GetZoneRadius() - nextRadius;
		int y = (int)Random.Range(1, maxOffset);
		int x = (int)Random.Range(1, maxOffset);
		return new Vector2(x,y);

	}

	int GetRandomZoneRadiusSize()
	{
		return Random.Range(minimunSizeShrink, maximumSizeShrink);
	}

	float GetCurrentZoneSpriteScale()
	{
		return zoneRenderer.transform.localScale.x;
	}

	void ChangeSpriteScale()
	{
		zoneRenderer.transform.localScale = 
			new Vector3(GetCurrentZoneSpriteScale() - SPRITESCALESIZE,
			GetCurrentZoneSpriteScale() - SPRITESCALESIZE,0);
	}

	void ChangeSpritePosition()
	{
		zoneRenderer.transform.localPosition =
			zoneCollider2D.offset;
	}
}
