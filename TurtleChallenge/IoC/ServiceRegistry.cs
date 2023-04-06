using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TurtleChallenge.Services;
using TurtleChallenge.Services.Builders;
using TurtleChallenge.Services.FileReaders;

namespace TurtleChallenge.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistry
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string boardFileExtension, string movesFileExtension)
        {
            services
                .AddSingleton<ITurtleBuilder, TurtleBuilder>()
                .AddSingleton<IExitPointBuilder, ExitPointBuilder>()
                .AddSingleton<IMineBuilder, MineBuilder>()
                .AddSingleton<IBoardBuilder, BoardBuilder>();

            if (string.IsNullOrEmpty(boardFileExtension) || string.IsNullOrEmpty(movesFileExtension))
            {
                throw new ArgumentException("ERROR: Board file or moves file without extension!");
            }

            AddBoardFileReader(services, boardFileExtension);
            AddMovesFileReader(services, movesFileExtension);

            services.AddSingleton<IGameService, GameService>();
            return services;
        }

        private static void AddBoardFileReader(IServiceCollection services, string boardFileExtension)
        {
            switch (boardFileExtension)
            {
                case ".json":
                    services.AddSingleton<IBoardFileReader, JsonBoardFileReader>();
                    break;

                case ".csv":
                    services.AddSingleton<IBoardFileReader, CsvBoardFileReader>();
                    break;

                default:
                    throw new ArgumentException("ERROR: Invalid file extension for boards file");
            }
        }

        private static void AddMovesFileReader(IServiceCollection services, string movesFileExtension)
        {
            switch (movesFileExtension)
            {
                case ".json":
                    services.AddSingleton<IMovesFileReader, JsonMovesFileReader>();
                    break;

                case ".csv":
                    services.AddSingleton<IMovesFileReader, CsvMovesFileReader>();
                    break;

                default:
                    throw new ArgumentException("ERROR: Invalid file extension for moves file");
            }
        }
    }
}