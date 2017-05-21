using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.OrderStatuses
{
    public enum OrderStatus
    {
        /// <summary>
        /// Успішно доставлено
        /// </summary>
        [Display(Name = "Успішно доставлено")]
        Successful = 100,

        /// <summary>
        /// Помилка замовлення
        /// </summary>
        [Display(Name = "Помилка замовлення")]
        Failed = 101,

        /// <summary>
        /// Відміна замовлення покупцем
        /// </summary>
        [Display(Name = "Відміна замовлення покупцем")]
        CanceledByBuyer = 102,

        /// <summary>
        /// Відміна замовлення продавцем
        /// </summary>
        [Display(Name = "Відміна замовлення продавцем")]
        CanceledBySeller = 103,

        /// <summary>
        /// Замовлення в обробці
        /// </summary>
        [Display(Name = "Замовлення в обробці")]
        InProccess = 104,

        /// <summary>
        /// Замовлення потребує оплати
        /// </summary>
        [Display(Name = "Замовлення потребує оплати")]
        NeedToPay = 105,

        /// <summary>
        /// Оплачено, очікування відправки
        /// </summary>
        [Display(Name = "Оплачено, очікування відправки")]
        Paid = 106,

        /// <summary>
        /// Товар в дорозі
        /// </summary>
        [Display(Name = "Товар в дорозі")]
        InShipping = 107
    }
}
