//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NutritionInvestigation
{
    using System;
    using System.Collections.Generic;
    
    public partial class NuritiveElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NuritiveElement()
        {
            this.FoodNutritions = new HashSet<FoodNutrition>();
        }
    
        public long MyID { get; set; }
        public string ElementName { get; set; }
        public string ElementUnit { get; set; }
        public Nullable<short> ShowOnReport { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodNutrition> FoodNutritions { get; set; }
    }
}
