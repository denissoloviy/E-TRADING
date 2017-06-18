using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.OrderStatuses
{
    public enum OrderStatus
    {
        /// <summary>
        /// Успішно доставлено
        /// </summary>
        [Display(Name = "Succesfully devilred")]
        Successful = 100,

        /// <summary>
        /// Помилка замовлення
        /// </summary>
        [Display(Name = "Order error")]
        Failed = 101,

        /// <summary>
        /// Відміна замовлення покупцем
        /// </summary>
        [Display(Name = "Canceled by buyer")]
        CanceledByBuyer = 102,

        /// <summary>
        /// Відміна замовлення продавцем
        /// </summary>
        [Display(Name = "Canceled by seller")]
        CanceledBySeller = 103,

        /// <summary>
        /// Замовлення в обробці
        /// </summary>
        [Display(Name = "Order pending")]
        InProccess = 104,

        /// <summary>
        /// Підтверджено продавцем, очікування оплати
        /// </summary>
        [Display(Name = "Confirmed by seller, awaiting payment")]
        NeedToPay = 105,

        /// <summary>
        /// Оплачено, очікування відправки
        /// </summary>
        [Display(Name = "Paid in full, awaiting delivery")]
        Paid = 106,

        /// <summary>
        /// Товар в дорозі
        /// </summary>
        [Display(Name = "Product on the way")]
        InShipping = 107
    }
}
