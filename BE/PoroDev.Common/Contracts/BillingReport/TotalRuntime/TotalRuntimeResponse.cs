namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeResponse
    {
        public int RuntimeNumber { get; set; }

        public double RuntimePrice { get; set; }

        public TotalRuntimeResponse()
        {
        }

        public TotalRuntimeResponse(int runtimeNumber, double runtimePrice)
        {
            RuntimeNumber = runtimeNumber;
            RuntimePrice = runtimePrice;
        }
    }
}