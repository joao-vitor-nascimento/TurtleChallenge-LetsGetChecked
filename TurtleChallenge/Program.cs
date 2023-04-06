using Microsoft.Extensions.DependencyInjection;
using TurtleChallenge.IoC;
using TurtleChallenge.Services;

public class Program
{
    private static void Main(string[] args)
    {
        var boardExtension = Path.GetExtension(args[0]);
        var movesExtension = Path.GetExtension(args[1]);

        var serviceProvider = new ServiceCollection()
            .AddServices(boardExtension, movesExtension)
            .BuildServiceProvider();

        var gameService = serviceProvider.GetService<IGameService>() ?? throw new Exception("ERROR: Could not finde the game dependency");
        gameService.StartGame(args[0], args[1]);
    }
}