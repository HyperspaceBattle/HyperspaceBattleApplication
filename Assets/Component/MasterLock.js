#pragma strict
	//position
	var lockPositionX = false;
	var lockPositionY = false;
	var lockPositionZ = false;
	var offsetPositionX = 0.0;
	var offsetPositionY = 0.0;
	var offsetPositionZ = 0.0;
	//rotation
	var lockRotationX = false;
	var lockRotationY = false;
	var lockRotationZ = false;
	var offsetRotationX = 0.0;
	var offsetRotationY = 0.0;
	var offsetRotationZ = 0.0;
	//scale
	var lockScaleX = false;
	var lockScaleY = false;
	var lockScaleZ = false;
	var offsetScaleX = 1.0;
	var offsetScaleY = 1.0;
	var offsetScaleZ = 1.0;
function Update () {
	//position
	if (lockPositionX){transform.localPosition.x = offsetPositionX;}
	if (lockPositionY){transform.localPosition.y = offsetPositionY;}
	if (lockPositionZ){transform.localPosition.z = offsetPositionZ;}
	//rotation
	if (lockRotationX){transform.localRotation.x = offsetRotationX;}
	if (lockRotationY){transform.localRotation.y = offsetRotationY;}
	if (lockRotationZ){transform.localRotation.z = offsetRotationZ;}
	//scale	
	if (lockScaleX){transform.localScale.x = offsetScaleX;}
	if (lockScaleY){transform.localScale.y = offsetScaleY;}
	if (lockScaleZ){transform.localScale.z = offsetScaleZ;}
}