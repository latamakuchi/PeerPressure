using UnityEngine;
using System.Collections;

public class PlayerController : Morpher {
	[HideInInspector]
	public bool activeControls;

	public void CustomUpdate(){
		CheckVInput();
		base.CustomUpdate();
	}

	void CheckVInput(){
		if(!activeControls)
			return;
		if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
			return;
		float verticalAxis = Input.GetAxis("Vertical");
		if(verticalAxis > 0){
			IncreaseClampV();
		}else{
			DecreaseClampV();
		}
	}

	void IncreaseClampV(){
		_transform.localScale = new Vector3(_transform.localScale.x,
		                                    Mathf.Clamp(_transform.localScale.y + vVel * Time.deltaTime, minHeight, maxHeight),
		                                    _transform.localScale.z);
	}
	
	void DecreaseClampV(){
		_transform.localScale = new Vector3(_transform.localScale.x,
		                                    Mathf.Clamp(_transform.localScale.y - vVel * Time.deltaTime, minHeight, maxHeight),
		                                    _transform.localScale.z);
	}

}
