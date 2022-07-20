namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeResponse
    {
        public int RuntimeNumber { get; set; }

        public TotalRuntimeResponse()
        {
        }

        public TotalRuntimeResponse(int runtimeNumber)
        {
            RuntimeNumber = runtimeNumber;
        }
    }
}