namespace E_TRADING.Common.OrderStatuses
{
    public class StatusTypes
    {
        public static OrderStatus[] ActiveStatuses =
        {
            OrderStatus.InProccess,
            OrderStatus.NeedToPay,
            OrderStatus.Paid,
            OrderStatus.InShipping
        };
        
        public static OrderStatus[] InactiveStatuses =
        {
            OrderStatus.Successful,
            OrderStatus.Failed,
            OrderStatus.CanceledByBuyer,
            OrderStatus.CanceledBySeller
        };
    }
}
