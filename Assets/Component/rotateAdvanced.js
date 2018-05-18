//RotateAdvanced.js

// Unity3DTips, 2014 ©
///////////

var XRotateSpeed = 10.0;
var YRotateSpeed = 10.0;
var ZRotateSpeed = 10.0;

function Update () {
transform.Rotate( XRotateSpeed, Time.deltaTime * YRotateSpeed, ZRotateSpeed );
}