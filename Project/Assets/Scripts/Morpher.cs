using UnityEngine;
using System.Collections;

public enum Direction {
	NONE,
	INCREASE, 
	DECREASE
}

public class Morpher : MonoBehaviour {
	public float vVel;
	public float height, minHeight, maxHeight;
	public Direction vDirection;
	public Transform _transform {get; private set;}

	void Awake(){
		_transform = transform;
		height = _transform.localScale.y;
	}

	protected void UpdateSizes(){
		this.height = _transform.localScale.y;
	}

	protected void CustomUpdate(){
		UpdateSizes();
	}
}
