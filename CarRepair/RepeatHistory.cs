//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRepair
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepeatHistory
    {
        public int ID_RepairHistory { get; set; }
        public int Orders_ID { get; set; }
        public string ListOfRepair { get; set; }
        public Nullable<int> Staff_ID { get; set; }
    
        public virtual OrderCar OrderCar { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
