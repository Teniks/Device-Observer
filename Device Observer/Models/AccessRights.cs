//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Device_Observer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccessRights
    {
        public int IdRight { get; set; }
        public int IdUser { get; set; }
        public int IdResource { get; set; }
        public string Permission { get; set; }
    
        public virtual Resources Resources { get; set; }
        public virtual Users Users { get; set; }
    }
}
