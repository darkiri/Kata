function BowlingGame(){
	this.pins = new Array()
}
BowlingGame.prototype.roll = function(pin){
	this.pins.push(pin)
	if (strikeRolled(this.pins)) this.roll(0)

	function strikeRolled(pins){
		return pins[pins.length-1] == 10 && pins.length % 2 != 0
	}
}
BowlingGame.prototype.getScore = function() {
	var pins = this.pins
	var scoreFrames = function() {
		var sum = 0
		for(var frame = 0; frame < 10; frame++) {
			sum += scoreSingleFrame(frame)
		}
		return sum
	}
	var scoreSingleFrame = function(frame) {
		return isStrike(frame) ? scoreStrike(frame)
			: isSpare(frame) ? scoreSpare(frame)
			: scoreNormal(frame)
	}
	var isStrike = function(frame) {
		return get(frame).firstRoll == 10
	}
	var isSpare = function(frame) {
		return scoreNormal(frame) == 10
	}
	var scoreStrike = function(frame) {
		return 10 + strikeBonus(frame+1)
	}
	var strikeBonus = function(frame) {
		return isStrike(frame)
			? 10 + get(frame+1).firstRoll
			: scoreNormal(frame)
	}
	var scoreSpare = function(frame) {
		return 10 + get(frame+1).firstRoll
	}
	var scoreNormal = function(frame) {
		return get(frame).firstRoll + get(frame).secondRoll
	}
	
	var get = function(frame) {
		return {
			firstRoll : pins[frame*2],
			secondRoll : pins[frame*2+1],
		}
	}
	return scoreFrames()
}
