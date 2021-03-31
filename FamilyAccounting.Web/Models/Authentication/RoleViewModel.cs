using System.Collections.Generic;

namespace FamilyAccounting.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
