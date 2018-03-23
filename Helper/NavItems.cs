using System.Collections.Generic;

namespace Helper
{
    public class NavItems
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string DisplayName { get; set; }
        public List<NavItems> ChildItems { get; set; }
        public int ParentId { get; set; }
    }
}
