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

        public static OrderStatus[] SuccessedStatuses =
        {
            OrderStatus.Successful,
            OrderStatus.Paid,
            OrderStatus.InShipping
        };

        public static OrderStatus[] WaitingStatuses =
        {
            OrderStatus.InProccess,
            OrderStatus.NeedToPay
        };

        public static OrderStatus[] FailedStatuses =
        {
            OrderStatus.Failed,
            OrderStatus.CanceledByBuyer,
            OrderStatus.CanceledBySeller
        };
    }
}
