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
    
    public partial class FoodIntakeRecord
    {
        public long MyID { get; set; }
        public Nullable<long> CustomerInputRecordID { get; set; }
        public long FoodClassID { get; set; }
        public Nullable<long> IntakeFrequency { get; set; }
        public Nullable<double> Intake { get; set; }
        public Nullable<double> AverageADay { get; set; }
        public Nullable<double> TypicalAverageADay { get; set; }
        public Nullable<int> RecordMode { get; set; }
    
        public virtual CustomerInvestigationRecord CustomerInvestigationRecord { get; set; }
    }
}
