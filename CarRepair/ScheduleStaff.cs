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
    
    public partial class ScheduleStaff
    {
        public int ID_ScheduleStaff { get; set; }
        public Nullable<int> Order_ID { get; set; }
        public int Staff_ID { get; set; }
    
        public virtual OrderCar OrderCar { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
