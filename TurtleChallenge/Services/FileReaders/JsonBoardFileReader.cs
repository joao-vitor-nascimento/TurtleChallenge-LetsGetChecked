using Newtonsoft.Json;
using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.FileReaders
{
    public class JsonBoardFileReader : IBoardFileReader
    {
        public JsonBoardFileReader()
        {
        }

        public Board ReadBoardFile(string path)
        {
            using (StreamReader r = new(path))
            {
                string json = r.ReadToEnd();
                var myBoard = JsonConvert.DeserializeObject<Board>(json);
                if (myBoard != null)
                {
                    return myBoard;
                }

                throw new InvalidDataException("ERROR: Could not read the board");
            }
        }
    }
}