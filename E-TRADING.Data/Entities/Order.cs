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

    public enum OrderStatus // todo Дописати ще статуси транзакцій
    {
        /// <summary>
        /// Транзакція успішно проведена
        /// </summary>
        [Display(Name = "Транзакція успішно проведена")]
        Successful = 100,

        /// <summary>
        /// Помилка транзакії
        /// </summary>
        [Display(Name = "Помилка транзакії")]
        Failed = 100,

        /// <summary>
        /// Транзакція в обробці
        /// </summary>
        [Display(Name = "Транзакція в обробці")]
        InProccess = 100,
    }
}
