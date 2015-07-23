using UnityEngine;
using System.Collections;

public class Enemy : Morpher {

	public void CustomUpdate(){
		Morph();
		base.CustomUpdate();
	}

	protected void Morph(){
		switch(vDirection){
		case Direction.NONE:
			break;
		case Direction.INCREASE:
			IncreaseVOrBounce();
			break;
		case Direction.DECREASE:
			DecreaseVOrBounce();
			break;
		}
	}

	void IncreaseVOrBounce(){
		_transform.localScale = new Vector3(_transform.localScale.x,
		                                    _transform.localScale.y + vVel * Time.deltaTime,
		                                    _transform.localScale.z);
		if(_transform.localScale.y > maxHeight)
			vDirection = Direction.DECREASE;
	}

	void DecreaseVOrBounce(){
		_transform.localScale = new Vector3(_transform.localScale.x,
		                                    _transform.localScale.y - vVel * Time.deltaTime,
		                                    _transform.localScale.z);
		if(_transform.localScale.y < minHeight)
			vDirection = Direction.INCREASE;
	}
}
