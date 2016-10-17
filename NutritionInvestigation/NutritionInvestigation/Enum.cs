using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionInvestigation
{
    enum InvestigateStatus
    {
        InvestigateInputting =1,
        InvestigateInputOver =2,
        StatisticsFoodIntakeOver =3,
        StatisticsNutritionResultOver = 4,
        GenerateReports=5,
        Approved=6,
        ApproveFailure =7
    }
    enum RecordMode
    {
        Input =1,
        Calculate =2
    }
    class Enum
    {
    }
}
