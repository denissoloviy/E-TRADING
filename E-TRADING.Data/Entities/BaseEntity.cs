using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Data.Entities
{
    public abstract class BaseEntity : IEntity, IDate
    {
        private DateTime _addedDate;

        [Key]
        public virtual string Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            AddedDate = DateTime.UtcNow;
        }

        [DisplayName("Creation date")]
        public DateTime AddedDate
        {
            get { return DateTime.SpecifyKind(_addedDate, DateTimeKind.Utc); }
            private set { _addedDate = value; }
        }
    }

    public interface IDate
    {
        DateTime AddedDate { get; }
    }

    public interface IEntity
    {
        string Id { get; set; }
    }
}
