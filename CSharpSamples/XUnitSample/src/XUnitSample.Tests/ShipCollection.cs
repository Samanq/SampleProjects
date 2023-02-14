using Xunit;

namespace XUnitSample.Tests;

[CollectionDefinition("Ship collection")]
public class ShipCollection : ICollectionFixture<ShipFixture>{}
