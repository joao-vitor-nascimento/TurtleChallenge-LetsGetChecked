namespace TurtleChallenge.DTO.BoardCreation
{
    public class BoardInformationDTO
    {
        public int XSize { get; set; }
        public int YSize { get; set; }
        public IEnumerable<MineInformationDTO> MinesInformation { get; set; }
        public ExitPointInformationDTO ExitPointInformation { get; set; }
        public TurtleInformationDTO TurtleInformation { get; set; }
    }
}