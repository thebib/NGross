namespace NGross.Core.Builders.Director;

public class TestBuildDirector
{
    private ITestBuilder builder;

    public TestBuildDirector(ITestBuilder builder)
    {
        this.builder = builder;
    }
}