namespace AlpaStock.Core.Entities
{
    public class ConfirmEmailToken : BaseEntity
    {
        public int Token { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
