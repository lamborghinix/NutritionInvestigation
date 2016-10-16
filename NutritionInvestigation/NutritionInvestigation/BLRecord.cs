using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace NutritionInvestigation
{
    class BLRecord
    {
        /// <summary>
        /// 获取已有记录条数
        /// </summary>
        /// <param name="RecordsCount"></param>
        /// <returns></returns>
        public int GetInvestigationRecordsCount()
        {
            int RecordsCount= 0;
            var p = from u in DALDB.GetInstance().CustomerInvestigationRecords
                    select u;
            if (p != null)
                RecordsCount = p.Count();
            else
            {
                Debug.WriteLine("result of select * from CustomerInvestigationRecords is null.");
            }
            return RecordsCount;
        }
        /// <summary>
        /// 获取所有客户信息。
        /// </summary>
        /// <returns></returns>
        public List<CustomerInfo> GetAllCustomersInfo()
        {
            var p = from u in DALDB.GetInstance().CustomerInfoes
                    select u;
            if (p != null)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("GetAllCustomersInfo return null.");
                return null;
            }
              
        }
        /// <summary>
        /// 获取指定的调查记录。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public CustomerInvestigationRecord GetInvestigationRecord(string queueID)
        {
            var p = from u in DALDB.GetInstance().CustomerInvestigationRecords
                    where u.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                return p.First();
            }
            else
            {
                Debug.WriteLine("GetInvestigationRecord of {0} return null.",queueID);
                return null;
            }
        }
        /// <summary>
        /// 检查指定调查记录是否已经完成全部录入。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public bool CheckInvestigationComplete(string queueID)
        {
            CustomerInvestigationRecord myRecord = GetInvestigationRecord(queueID);
            if(myRecord!=null && myRecord.InvestigationStatus!=null)
            {
                return ((int)myRecord.InvestigationStatus == (int)InvestigateStatus.InvestigateInputOver);
            }
            else
            {
                Debug.WriteLine("GetInvestigationRecord of {0}  investigate Status is null.", queueID);
                return false;
            }
        }
        /// <summary>
        /// 获取调查表的食物类问题列表。
        /// </summary>
        /// <returns></returns>
        public List<FoodClass> GetInvestigationQuestions()
        {
            var p = from u in DALDB.GetInstance().FoodClasses
                    where u.NeedInput == true
                    orderby u.SortID
                    select u;
            if(p!=null && p.Count()>0)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("Get Investigation Questions list");
                return null;
            }
        }
        /// <summary>
        /// 获取指定调查的当前进度。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public int GetInvestigateProgress(string queueID)
        {
            var p = from u in DALDB.GetInstance().CustomerInvestigationRecords
                    where u.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                CustomerInvestigationRecord myInvestigation = p.First();
                if(myInvestigation.FoodIntakeRecords!=null)
                {
                    return myInvestigation.FoodIntakeRecords.Count();
                }
                else
                {
                    Debug.WriteLine("current investigation {0} haven't begin. ",queueID);
                    return 0;
                }
            }
            else
            {
                Debug.WriteLine("can't get the info of Investigation {0}.", queueID);
                return -1;
            }
        }
        /// <summary>
        /// 获取指定的食物类信息（食物类调查问题信息）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public FoodClass GetInvestigationQuestion(int index)
        {
            var p = from u in DALDB.GetInstance().FoodClasses
                    orderby u.SortID
                    select u;
            if (p != null && p.Count() > 0)
            {
                if (p.Count() > index - 1)
                {
                    return p.ElementAt(index - 1);
                }
                else
                {
                    Debug.WriteLine("food class's count {0} is less than the question id {1}", p.Count(), index);
                    return null;
                }
            }
            else
            {
                Debug.WriteLine("can't get the foodClass.");
                return null;
            }
        }
        /// <summary>
        /// 判断用户信息是否存在，如果存在则返回。
        /// </summary>
        /// <param name="healthbookID"></param>
        /// <param name="customerName"></param>
        /// <param name="myCustomerInfo"></param>
        /// <returns></returns>
        public bool CheckCustomerExist(string healthbookID, string customerName, out CustomerInfo myCustomerInfo, out List<CustomerInfo> myCustomerinfoList)
        {
            myCustomerInfo = null;
            myCustomerinfoList = null;
            if (healthbookID==string.Empty&& customerName ==string.Empty)
            {
                Debug.WriteLine("the HealthBookID and the CustomerName are both empty, can't get Customer info.");
                return false;
            }
            if(healthbookID!=string.Empty)
            {
                var p = from u in DALDB.GetInstance().CustomerInfoes
                        where u.HealthBookID == healthbookID
                        select u;
                if(p!=null && p.Count()>0)
                {
                    myCustomerInfo = p.First();
                    return true;
                }
            }
            if(customerName!=string.Empty)
            {
                var p = from u in DALDB.GetInstance().CustomerInfoes
                        where u.Name == customerName
                        select u;
                if(p!=null && p.Count()>0)
                {
                    if(p.Count()==1)
                    {
                        myCustomerInfo = p.First();
                        Debug.WriteLine("find the one customer info on the name {0}. ", customerName);
                        return true;
                    }
                    else
                    {
                        myCustomerinfoList = p.ToList();
                        Debug.WriteLine("find several customer info on the name {0}. ", customerName);
                        return true;
                    }
                }
            }
            Debug.WriteLine("can't find customer info on the name {0}.", customerName);
            return false;
        }
        //新增一个客户信息。
        public bool AddCustomer(CustomerInfo newCustomer)
        {
            CustomerInfo existCustomer;
            List<CustomerInfo> existCustomerList;
            if(CheckCustomerExist(newCustomer.HealthBookID,string.Empty,out existCustomer, out existCustomerList))
            {
                Debug.WriteLine("existCustomer, can't add.");
                return false;
            }
            else
            {
                DALDB.GetInstance().CustomerInfoes.Add(newCustomer);
                try
                {
                    int r = DALDB.GetInstance().SaveChanges();
                    if (r >0)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Add new Customer {0} falure.", newCustomer.Name);
                        Debug.WriteLine("SaveChanges return {0}.", r.ToString());
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Add new Customer {0} error.", newCustomer.Name);
                    Debug.WriteLine("Error message is {0}.", ex.ToString());
                    return false;
                }
                
            }
        }
        /// <summary>
        /// 在一个调查记录中，增加一条饮食记录
        /// </summary>
        /// <param name="queueID"></param>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public bool AddFoodIntakeRecord(string queueID, FoodIntakeRecord newRecord)
        {
            CustomerInvestigationRecord myInvestigationRecord = GetInvestigationRecord(queueID);
            if (myInvestigationRecord!=null)
            {
                myInvestigationRecord.FoodIntakeRecords.Add(newRecord);
                try
                {
                    int r = DALDB.GetInstance().SaveChanges();
                    if (r>0)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Add new Record {0} in Investigation {1} falure.", newRecord.FoodClassID.ToString(), queueID );
                        Debug.WriteLine("SaveChanges return {0}.", r.ToString());
                        return false;
                    }
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine("Add new Record {0} in Investigation {1} falure.", newRecord.FoodClassID.ToString(), queueID);
                    Debug.WriteLine("Error message is {0}.", ex.ToString());
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("Don't Exist Investigation Record {0}, can't add FoodIntakeRecords.",queueID);
                return false;
            }
        }
        /// <summary>
        /// 判断健康调查录入是否完成。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public bool CheckFoodIntakeRecordComplete(string queueID)
        {
            CustomerInvestigationRecord myInvestigationRecord = GetInvestigationRecord(queueID);
            if (myInvestigationRecord != null)
            {
                int FoodIntakeCount = GetInvestigationQuestions().Count();
                int HadInputCount = myInvestigationRecord.FoodIntakeRecords.Count();
                if (FoodIntakeCount == HadInputCount)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine("Have Input {0} records, not yet enough {1}.", HadInputCount, FoodIntakeCount);
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("Can't check because no InvestigationRecord of queueID {0}.", queueID);
                return false;
            }
        }
        /// <summary>
        /// 获取全部调查记录。
        /// </summary>
        /// <returns></returns>
        public List<CustomerInvestigationRecord> GetAllInvestigationRecordList()
        {
            var p = from u in DALDB.GetInstance().CustomerInvestigationRecords
                    select u;
            if(p!= null && p.Count()>0)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("no Customer Investigation Records in DB.");
                return null;
            }
        }
        /// <summary>
        /// 获取一个时间段的调查记录
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<CustomerInvestigationRecord> GetInvestigationRecordList(DateTime beginDate, DateTime endDate)
        {
            var p = from u in DALDB.GetInstance().CustomerInvestigationRecords
                    where u.InvestigationDate >= beginDate && u.InvestigationDate <= endDate
                    select u;
            if(p!=null && p.Count()>0)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("no Customer Investigation Records in DB between date {0} and date{1}. ", 
                    beginDate.ToShortDateString(), endDate.ToShortDateString());
                return null;
            }
        }
        /// <summary>
        /// 获取一个调查的食物统计结果。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public List<FoodStatisticsResult> GetFoodStatisticsResult(string queueID)
        {
            var p = from u in DALDB.GetInstance().FoodStatisticsResults
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("no Food Statistics Result of Investigation {0}.", queueID);
                return null;
            }
        }

        public List<NutritionStatisticResult> GetNutritionStatisticsResult(string queueID)
        {
            var p = from u in DALDB.GetInstance().NutritionStatisticResults
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                return p.ToList();
            }
            else
            {
                Debug.WriteLine("no Nutrition Statistics Result of Investigation {0}.", queueID);
                return null;
            }
        }
        /// <summary>
        /// 生成用户健康调查报告。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public object GenerateCustomerAnalysisReport(string queueID)
        {
            //未完成。
            return null;

        }

        public bool StatisticsFoodIntake(string queueID)
        {
            return false;
        }

        public bool StatisticsNutritionIntake(string queueID)
        {
            return false;
        }


    }
}
