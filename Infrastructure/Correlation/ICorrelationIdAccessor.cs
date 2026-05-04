namespace Infrastructure.Correlation
{
    public interface ICorrelationIdAccessor
    {
        string? Get();
        void Set(string correlationId);
    }
}
