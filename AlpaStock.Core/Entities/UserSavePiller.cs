namespace AlpaStock.Core.Entities
{
    public class UserSavePiller : BaseEntity
    {
        public string PillerName { get; set; }
        public string Comparison { get; set; }
        public string Format { get; set; }
        public string Value { get; set; }
        public ApplicationUser User { get; set; }
        public string Userid { get; set; }
    }
}
