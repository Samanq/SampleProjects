namespace EntraExternalIdSample.Services;

public record AccessCode(string Code, string Password, string Label)
{
    /// Claim type added to the signed-in user for every access-code group found in the "groups" claim.
    public const string ClaimType = "access_code";

    private const string GroupPrefix = "AccessCode-";

    /// Entra security group that represents this code. Deterministic so nothing has to be stored locally.
    public string GroupName => GroupPrefix + Code;
}

public class AccessCodeService
{
    private static readonly AccessCode[] Codes =
    [
        new("ADVCD01", "alpha",   "Warehouse A"),
        new("ADVCD02", "bravo",   "Warehouse B"),
        new("ADVCD03", "charlie", "Finance reports"),
        new("ADVCD04", "delta",   "HR documents"),
        new("ADVCD05", "echo",    "Project Falcon"),
        new("ADVCD06", "foxtrot", "Project Osprey"),
        new("ADVCD07", "golf",    "Lab access"),
        new("ADVCD08", "hotel",   "Parking garage"),
        new("ADVCD09", "india",   "VIP lounge"),
        new("ADVCD10", "juliet",  "Beta features"),
    ];

    public IReadOnlyList<AccessCode> All => Codes;

    public AccessCode? Verify(string? code, string? password)
    {
        return Codes.FirstOrDefault(c => c.Code == code?.Trim() && c.Password == password);
    }

    public AccessCode? ByGroupName(string groupName)
    {
        return Codes.FirstOrDefault(c => c.GroupName == groupName);
    }

    public AccessCode? ByCode(string code)
    {
        return Codes.FirstOrDefault(c => c.Code == code);
    }
}
