namespace AspNetCoreDI.Services;

public interface IApplicationService
{
    Guid GetApplicationGuid();
}

public class ApplicationService : IApplicationService
{
    private readonly Guid _applicationGuid;

    public ApplicationService()
    {
        _applicationGuid = Guid.NewGuid();
    }

    public Guid GetApplicationGuid() => _applicationGuid;
}
