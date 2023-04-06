using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Services.Builders
{
    public class MovesBuilderTest
    {
        private readonly IMovesBuilder _movesBuilder;

        public MovesBuilderTest()
        {
            _movesBuilder = new MovesBuilder();
        }

        [Fact]
        public void BuildMovesFromCsvRowTest_WithoutAnyEmptyString_ValidCsv()
        {
            //Arrange
            string[] csvRow = {"SequenceName", "move", "rotate" };
            var expected = new List<Moves> { Moves.Move, Moves.Rotate };

            //Act
            var result = _movesBuilder.BuildMovesFromCsvRow(csvRow);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BuildMovesFromCsvRowTest_WithEmptyStringOnFinish_ValidCsv()
        {
            //Arrange
            string[] csvRow = { "SequenceName", "move", "rotate" , ""};
            var expected = new List<Moves> { Moves.Move, Moves.Rotate };

            //Act
            var result = _movesBuilder.BuildMovesFromCsvRow(csvRow);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BuildMovesFromCsvRowTest_WithEmptyStringOnStart_InValidCsv()
        {
            //Arrange
            string[] csvRow = { "" ,"SequenceName", "move", "rotate", };
            //Act
            //Assert
            Assert.Throws<ArgumentException>( () =>  _movesBuilder.BuildMovesFromCsvRow(csvRow));
        }
    }
}
