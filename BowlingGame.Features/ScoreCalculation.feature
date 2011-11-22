Feature: Score Calculation
	In order to know my performance
	As a player
	I want the system to calculate my total score

Background:
	Given a new bowling game 
	 
Scenario: Gutter Game
	When all of my balls are landing in the gutter
	Then my total score should be 0

Scenario: Single Roll Scores
	When one roll scores single pin
	 And all other balls are landing in the gutter
	Then my total score should be 1

Scenario: Every Roll Scores
	When all rolls score one pin
	Then my total score should be 20

Scenario: Single Spare Scores
	When i hit 5 and 5 pins in a frame
	 And i hit 1 and 2 pins in the next frame
	 And all other balls are landing in the gutter
	Then my total score should be 14

Scenario: Double Spare Scores
	When i hit 5 and 5 pins in the first frame
	 And i hit 4 and 6 pins in the next frame
	 And i hit 2 and 3 pins in the following frame
	 And all other balls are landing in the gutter
	Then my total score should be 31

Scenario: Single Strike Scores
	When i hit 10 in a frame
	 And i hit 1 and 2 pins in the next frame
	 And all other balls are landing in the gutter
	Then my total score should be 16

Scenario: Zero/Ten Scores as a Spare
	When i hit 0 and 10 pins in a frame
	 And i hit 1 and 2 pins in the next frame
	 And all other balls are landing in the gutter
	Then my total score should be 14

Scenario: Double Strike Scores
	When i hit 10 in a frame
	 And i hit 10 in the next frame
	 And i hit 1 and 2 pins in the following frame
	 And all other balls are landing in the gutter
	Then my total score should be 37

Scenario: Perfect Game
	When all of my rolls are strikes
	Then my total score should be 300