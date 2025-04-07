
namespace Core.Entities.Abstract
{
    public abstract class Vehicle : BaseEntity
    {
        public string Nickname { get; set; }
        public int KmPassed { get; set; }
        public DateOnly FabricationDate { get; set; }
    }
}
