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
    
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.OrderCars = new HashSet<OrderCar>();
        }
    
        public int ID_Car { get; set; }
        public int Client_ID { get; set; }
        public int ModelCar_ID { get; set; }
        public int Mark_ID { get; set; }
        public string NumberCar { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Mark Mark { get; set; }
        public virtual ModelCar ModelCar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderCar> OrderCars { get; set; }
    }
}
