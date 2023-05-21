using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models;

[Table("Users")]
public class User_SQL
{
    [Required]
    public long Id { get; set; }
    [MaxLength(255)]
    public string Forename { get; set; } = default!;
    [MaxLength(255)]
    public string Surname { get; set; } = default!;
    [MaxLength(255)]
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
}
