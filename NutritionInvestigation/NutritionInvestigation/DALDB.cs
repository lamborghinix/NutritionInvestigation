using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionInvestigation
{
    public static class DALDB
    {
        static myDBEntities myDB;

        public static myDBEntities GetInstance()
        {
            if(myDB==null)
            {
                myDB = new myDBEntities();
            }
            return myDB;
        }
    }
}
