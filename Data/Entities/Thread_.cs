
namespace Data.Entities
{
    public class Thread_ : BaseEntity
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }    
        public int CensorShipId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Vievs { get; set; } 
        public string ThreadTitle { get; set; } = "";
        public string ThreadBody { get; set; } = "";

        public virtual User Owner { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual CensorShip CensorShip { get; set; }
        public virtual ICollection<Comment> Coments { get; set; }
        
    }
}
