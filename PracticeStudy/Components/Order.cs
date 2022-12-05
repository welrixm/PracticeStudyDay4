//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticeStudy.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.ProductOrder = new HashSet<ProductOrder>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> DateOrder { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public int ExecutionStageId { get; set; }
        public int UserId { get; set; }
    
        public virtual ExecutionStage ExecutionStage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual User User { get; set; }
    }
}
