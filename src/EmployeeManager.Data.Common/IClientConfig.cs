namespace EmployeeManager.Data
{
    public interface IClientConfig
    {
        string BaseUrl { get; }
        string APIToken { get; }
    }
}
