namespace E_TRADING.Data.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
