﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class ProductViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Опис")]
        public string Description { get; set; }
        [DisplayName("Картинка")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Кількість")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Ціна")]
        public decimal Price { get; set; }
        [DisplayName("Дата додавання")]
        public string AddedDate { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Категорія")]
        public string Category { get; set; }

        public string SellerId { get; set; }
        public string SellerName { get; set; }
    }
}