using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BowlingGame.Features
{
    [Binding]
    public class ScoreCalculationSteps
    {
        private Game _game;
        private int _rollsMade;

        [Given(@"a new bowling game")]
        public void GivenANewBowlingGame()
        {
            _game = new Game();
        }

        [When(@".+ balls are landing in the gutter")]
        public void WhenAllOfMyBallsAreLandingInTheGutter()
        {
            RollMany(20-_rollsMade, 0);
        }

        private void RollMany(int times, int pins)
        {
            for (var i = 0; i < times; i++)
            {
                _game.Roll(pins);
            }
        }

        [When(@"one roll scores single pin")]
        public void WhenOneRollScoresSinglePin()
        {
            _game.Roll(1);
            _rollsMade = 1;
        }

        [When(@"all rolls score one pin")]
        public void WhenAllRollsScoreOnePin()
        {
            RollMany(20, 1);
        }

        [When(@"i hit (\d+) and (\d+) pins in .+ frame")]
        public void WhenIRollAFrame(int pins1, int pins2)
        {
            _game.Roll(pins1);
            _game.Roll(pins2);
            _rollsMade = 2;
        }

        [When(@"i hit 10 in .+ frame")]
        public void WhenIRollAStrike()
        {
            _game.Roll(10);
            _rollsMade = 2;
        }

        [When(@"all of my rolls are strikes")]
        public void WhenAllRollsAreStrikes()
        {
            RollMany(12, 10);
        }

        [Then(@"my total score should be (\d+)")]
        public void ThenMyTotalScoreShouldBe(int score)
        {
            Assert.That(_game.Score(), Is.EqualTo(score));
        }
    }
}
