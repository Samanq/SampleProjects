namespace MapsterSample.Contracts.Students;

public record StudentResponse(
    int Id,
    string FirstName,
    string LastName,
    int Age);