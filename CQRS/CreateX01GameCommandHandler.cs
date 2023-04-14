using Amazon.Lambda.APIGatewayEvents;
using System.Threading;
using System.Threading.Tasks;
using Flyingdarts.Persistence;
using MediatR;

public class CreateX01GameCommandHandler : IRequestHandler<CreateX01GameCommand, APIGatewayProxyResponse>
{
    public CreateX01GameCommandHandler()
    {
    }
    public async Task<APIGatewayProxyResponse> Handle(CreateX01GameCommand request, CancellationToken cancellationToken)
    {
        return new APIGatewayProxyResponse { StatusCode = 200 };
    }
}