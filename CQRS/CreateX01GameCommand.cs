using System;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;

public class CreateX01GameCommand : IRequest<APIGatewayProxyResponse>
{
    public int Sets { get; set; }
    public int Legs { get; set; }
    public int StartScore { get; set; }
    public string RoomId { get; set; }
    public Guid UserId { get; set; }
}