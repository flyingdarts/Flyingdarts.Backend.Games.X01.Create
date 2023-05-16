using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Flyingdarts.Persistence;
using Flyingdarts.Shared;
using MediatR;
using Microsoft.Extensions.Options;

public class CreateX01GameCommandHandler : IRequestHandler<CreateX01GameCommand, APIGatewayProxyResponse>
{
    private readonly IDynamoDBContext _dbContext;
    private readonly IOptions<ApplicationOptions> _applicationOptions;

    public CreateX01GameCommandHandler(IDynamoDBContext DbContext, IOptions<ApplicationOptions> ApplicationOptions)
    {
        _dbContext = DbContext;
        _applicationOptions = ApplicationOptions;
    }
    public async Task<APIGatewayProxyResponse> Handle(CreateX01GameCommand request, CancellationToken cancellationToken)
    {
        var game = Game.Create(2, X01GameSettings.Create(1, 3), request.RoomId);
        var gamePlayer = GamePlayer.Create(game.GameId, request.UserId);
        
        var gameWrite = _dbContext.CreateBatchWrite<Game>(_applicationOptions.Value.ToOperationConfig());
        gameWrite.AddPutItem(game);

        var gamePlayerWrite = _dbContext.CreateBatchWrite<GamePlayer>(_applicationOptions.Value.ToOperationConfig());
        gamePlayerWrite.AddPutItem(gamePlayer);

        await gameWrite.Combine(gamePlayerWrite).ExecuteAsync(cancellationToken);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(new { Game = game, Players = new[]{ gamePlayer }})
        };
    }
}