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
    
    public partial class CustomerInvestigationRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerInvestigationRecord()
        {
            this.FoodIntakeRecords = new HashSet<FoodIntakeRecord>();
            this.FoodStatisticsResults = new HashSet<FoodStatisticsResult>();
            this.NutritionStatisticResults = new HashSet<NutritionStatisticResult>();
        }
    
        public long MyID { get; set; }
        public long CustomerID { get; set; }
        public string QueueID { get; set; }
        public Nullable<System.DateTime> InvestigationDate { get; set; }
        public Nullable<double> CurrentWeight { get; set; }
        public Nullable<long> InvestigatorID { get; set; }
        public string InvestigatorName { get; set; }
        public Nullable<long> AuditorID { get; set; }
        public string AuditorName { get; set; }
        public Nullable<int> InvestigationStatus { get; set; }
        public Nullable<int> GestationalWeek { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodIntakeRecord> FoodIntakeRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodStatisticsResult> FoodStatisticsResults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutritionStatisticResult> NutritionStatisticResults { get; set; }
    }
}
