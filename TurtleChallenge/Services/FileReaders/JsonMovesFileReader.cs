using Newtonsoft.Json;
using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.FileReaders
{
    public class JsonMovesFileReader : IMovesFileReader
    {
        public JsonMovesFileReader()
        {
        }

        public IDictionary<string, IEnumerable<Moves>> ReadMovesFile(string path)
        {
            using (StreamReader r = new(path))
            {
                string json = r.ReadToEnd();
                var moves = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Moves>>>(json);
                if (moves != null)
                {
                    return moves;
                }

                throw new InvalidDataException("ERROR: Could not read the moves");
            }
        }
    }
}