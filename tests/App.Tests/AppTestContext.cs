using Microsoft.FeatureManagement;
using NSubstitute;

namespace App.Tests;

public abstract class AppTestContext : TestContext
{
    private readonly IFeatureManager _featureManager = Substitute.For<IFeatureManager>();

    protected AppTestContext()
    {
        _featureManager.IsEnabledAsync(Arg.Any<string>()).Returns(true);
        Services.AddSingleton(_featureManager);
    }
    
    protected void DisableFeature(string featureName)
    {
        _featureManager.IsEnabledAsync(featureName).Returns(false);
    }
}