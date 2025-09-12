
namespace Core.Entities.Abstract
{
    public abstract class Vehicle : BaseEntity
    {
        public string Nickname { get; set; } = string.Empty;
        public int KmPassed { get; set; }
        public DateTime FabricationDate { get; set; }
    }
}
