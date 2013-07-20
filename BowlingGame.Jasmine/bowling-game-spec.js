Number.prototype.times = function(){
	return this;
}
describe("BowlingGame", function() {

	var bowlingGame

	beforeEach(function() {
		bowlingGame = new BowlingGame()
	})

	var roll = function(pins, times) {
		for (var i = 0; i < times; i++) {
			bowlingGame.roll(pins)
		}
	}

	var expectScore = function(expected) {
		expect(bowlingGame.getScore()).toEqual(expected)
	}

	it("when all the balls are landing in the gutter, the total score should be 0", function() {
		roll(0, 20)
		expectScore(0)
	})
	it("when one roll scores one pin, my total score should be 1", function(){
		roll(1, 1)
		roll(0, 19)
		expectScore(1)
	})
	it("when all rolls scores, my total score should be 20", function(){
		roll(1, 20)
		expectScore(20)
	})
	it("single spare should score", function(){
		roll(5, 2)
		roll(1, 1)
		roll(2, 1)
		roll(0, 16)
		expectScore(14)
	})
	it("double spare should score", function(){
		roll(5, 4)
		roll(1, 1)
		roll(2, 1)
		roll(0, 14)
		expectScore(29)
	})
	it("single strike should score", function(){
		roll(10, 1)
		roll(1, 1)
		roll(2, 1)
		roll(0, 16)
		expectScore(16)
	})
	it("zero/ten scores as a spare", function(){
		roll(0, 1)
		roll(10, 1)
		roll(1, 1)
		roll(2, 1)
		roll(0, 16)
		expectScore(14)
	})
	it("double strike should score", function(){
		roll(10, 2)
		roll(1, 1)
		roll(2, 1)
		roll(0, 14)
		expectScore(37)
	})
	it("perfect game should score to 300", function(){
		roll(10, 12)
		expectScore(300)
	})
})
