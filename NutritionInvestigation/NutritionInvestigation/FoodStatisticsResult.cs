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
    
    public partial class FoodStatisticsResult
    {
        public long MyID { get; set; }
        public Nullable<long> CustomerInputRecordID { get; set; }
        public string FoodStaticName { get; set; }
        public string FoodClassID { get; set; }
        public string FoodClassUnit { get; set; }
        public Nullable<long> SortID { get; set; }
        public Nullable<double> FoodClassStatisticsValue { get; set; }
    
        public virtual CustomerInvestigationRecord CustomerInvestigationRecord { get; set; }
    }
}
