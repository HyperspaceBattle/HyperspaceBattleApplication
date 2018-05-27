private var maxUpAndDown       : float = .25;       // amount of meters going up and down
private var speed             : float = 1;      // up and down speed
var randMinSpeed				: float = .25;
var randMaxSpeed				: float = 5;
var randMinUpAndDown				: float = .25;
var randMaxUpAndDown				: float = 5;
var yOffset : float = 0;

protected var angle        : float = 0;       // angle to determin the height by using the sinus
protected var toDegrees     : float    = Mathf.PI/180;    // radians to degrees
 
function Start (){
	speed = Random.Range (randMinSpeed, randMaxSpeed);
	maxUpAndDown = Random.Range (randMinUpAndDown, randMaxUpAndDown);
}

function Update()
{
    angle += speed * Time.deltaTime;
  //  if (angle > 360) angle -= 360;
    transform.position.y = maxUpAndDown /** Mathf.Sin(angle * toDegrees)*/ + yOffset;
}