using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Order : BaseEntity
    {
        [DisplayName("Статус замовлення")]
        public OrderStatus Status { get; set; }

        [DisplayName("Кількість")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Загальна ціна")]
        public decimal FullPrice { get; set; }

        [DisplayName("Адреса доставки")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Товар")]
        public string ProductId { get; set; }
        [DisplayName("Товар")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Покупець")]
        public string BuyerId { get; set; }
        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }
    }

    public enum OrderStatus // todo Переглянути
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
        /// Відміна замовлення
        /// </summary>
        [Display(Name = "Відміна замовлення покупцем")]
        CanceledByBuyer = 102,

        /// <summary>
        /// Відміна замовлення
        /// </summary>
        [Display(Name = "Відміна замовлення продавцем")]
        CanceledBySeller = 103,

        /// <summary>
        /// Замовлення в обробці
        /// </summary>
        [Display(Name = "Замовлення в обробці")]
        InProccess = 104,

        /// <summary>
        /// Товар в дорозі
        /// </summary>
        [Display(Name = "Товар в дорозі")]
        InShipping = 105
    }
}
