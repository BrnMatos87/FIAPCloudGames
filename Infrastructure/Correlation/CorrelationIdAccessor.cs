namespace Infrastructure.Correlation
{
    public class CorrelationIdAccessor : ICorrelationIdAccessor
    {
        private string? _correlationId;

        public string? Get() => _correlationId;

        public void Set(string correlationId) => _correlationId = correlationId;
    }
}
