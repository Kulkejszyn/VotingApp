namespace Domain.Dtos.User;

public sealed record UserDto
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
}
