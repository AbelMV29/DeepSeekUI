using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeepSeekUI.Entities.Common
{
    public class CommonEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        public DateTime CreatedTime { get; private set; }

        public CommonEntity()
        {
            Id = Guid.NewGuid();
            CreatedTime = DateTime.UtcNow;
        }
    }
}
