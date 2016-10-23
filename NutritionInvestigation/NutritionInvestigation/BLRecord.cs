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
        /// 获取所有需要输入的食品类表
        /// </summary>
        /// <returns></returns>
        internal List<FoodClass> GetIntakeFoodClass()
        {
            var p = from u in DALDB.GetInstance().FoodClasses
                    where u.NeedInput == true
                    select u;
            if (p != null && p.Count() > 0)
                return p.ToList();
            else
                return null;
        }
        /// <summary>
        /// 获取当前调查已经填写的记录
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        internal List<FoodIntakeRecord> GetFoodIntakeList(string queueID)
        {
            var p = from u in DALDB.GetInstance().FoodIntakeRecords
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            if (p != null && p.Count() > 0)
            {
                return p.ToList();
            }
            else
                return null;
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

        internal bool UpdateInvestigationRecord(CustomerInvestigationRecord newInvestigation, string queueID, 
            out CustomerInvestigationRecord myInvestigetionRecord,  out string saveInvestigationMessage)
        {
             myInvestigetionRecord = GetInvestigationRecord(queueID);
            if(myInvestigetionRecord !=null)
            {
                myInvestigetionRecord.CurrentWeight = newInvestigation.CurrentWeight;
                myInvestigetionRecord.GestationalWeek = newInvestigation.GestationalWeek;
                myInvestigetionRecord.InvestigationDate = newInvestigation.InvestigationDate;
                myInvestigetionRecord.InvestigatorName = newInvestigation.InvestigatorName;
                myInvestigetionRecord.CustomerID = newInvestigation.CustomerID;
                myInvestigetionRecord.CustomerName = newInvestigation.CustomerName;
                int result;
                try
                {
                    result = DALDB.GetInstance().SaveChanges();
                }
                catch (Exception ex)
                {
                    saveInvestigationMessage = "update Investigation result error, error message is " + ex.ToString();
                    return false;
                }
                if(result>0)
                {
                    saveInvestigationMessage = "update investigation result successful";
                    return true;
                }
                else
                {
                    saveInvestigationMessage = "update investigation result failure, save changes return " + result.ToString();
                    return false;
                }
            }
            else
            {
                //saveInvestigationMessage = "don't exist Investigation record, add a new one";
                return AddInvestigationBaseInfo(newInvestigation, out saveInvestigationMessage);
            }
        }

        /// <summary>
        /// 更新孕妇基本信息
        /// </summary>
        /// <param name="newCustomerInfo"></param>
        /// <param name="myCustomerInfo"></param>
        /// <param name="saveCustomerMessage"></param>
        /// <returns></returns>
        internal bool UpdateCustomerInfo(CustomerInfo newCustomerInfo, string healthbookID,out CustomerInfo myCustomerInfo, out string saveCustomerMessage)
        {
             myCustomerInfo = GetCustomersInfo(healthbookID);
            if(myCustomerInfo !=null)
            {
                myCustomerInfo.Name = newCustomerInfo.Name;
                myCustomerInfo.Birthday = newCustomerInfo.Birthday;
                myCustomerInfo.BeforeWeight = newCustomerInfo.BeforeWeight;
                myCustomerInfo.Height = newCustomerInfo.Height;
                int result;
                try
                {
                    result = DALDB.GetInstance().SaveChanges();
                }
                catch (Exception ex)
                {
                    saveCustomerMessage = "Update customer info Error, Error Message is " + ex.ToString();
                    return false;
                }
                if(result>0)
                {
                    saveCustomerMessage = "Update customer info Successful.";
                    return true;
                }
                else
                {
                    saveCustomerMessage = "Update customer info failure, save changes return " + result.ToString();
                    return false;
                }
            }
            else
            {
                saveCustomerMessage = "don't exist customer in DB, Add as a new one";
                return AddCustomer(newCustomerInfo);
            }
        }
        /// <summary>
        /// 增加一条营养调查基本信息记录
        /// </summary>
        /// <param name="newInvestigation"></param>
        /// <param name="saveInvestigationMessage"></param>
        /// <returns></returns>
        internal bool AddInvestigationBaseInfo(CustomerInvestigationRecord newInvestigation, out string saveInvestigationMessage)
        {
            CustomerInvestigationRecord existInvestigation = GetInvestigationRecord(newInvestigation.QueueID);
            if (existInvestigation != null)
            {
                saveInvestigationMessage = "Exist Investigation which queueId is " + newInvestigation.QueueID;
                return false;
            }
            newInvestigation.InvestigationStatus = (int)InvestigateStatus.InvestigateInputting;
            DALDB.GetInstance().CustomerInvestigationRecords.Add(newInvestigation);
            int result;
            try
            {
                result = DALDB.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                saveInvestigationMessage = "Add Investigation info to DB error, Error Message is " + ex.ToString();
                return false;
            }
            if(result>0)
            {
                saveInvestigationMessage = "Add Investigation successful";
                return true;
            }
            else
            {
                saveInvestigationMessage = "Add Investigation failure, saveChanges return " + result.ToString();
                return false;
            }
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

        internal CustomerInfo GetCustomersInfo(long customerID)
        {
            CustomerInfo myCustomer;
            var p = from u in DALDB.GetInstance().CustomerInfoes
                    where u.MyID == customerID
                    select u;
            if(p!=null && p.Count()==1)
            {
                myCustomer = p.First();
            }
            else
            {
                myCustomer = null;
            }
            return myCustomer;
        }

        internal CustomerInfo GetCustomersInfo(string healthbookID)
        {
            CustomerInfo myCustomer;
            var p = from u in DALDB.GetInstance().CustomerInfoes
                    where u.HealthBookID == healthbookID
                    select u;
            if (p != null && p.Count() == 1)
            {
                myCustomer = p.First();
            }
            else
            {
                myCustomer = null;
            }
            return myCustomer;
        }

        internal bool CheckInvestigationExist(string queueID,out CustomerInvestigationRecord myInvestigation)
        {
            myInvestigation = GetInvestigationRecord(queueID);
            if (myInvestigation == null)
                return false;
            else
                return true;
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
                var p = from u in DALDB.GetInstance().FoodIntakeRecords
                        where u.CustomerInvestigationRecord.QueueID == queueID
                        select u;
                foreach(var u in p)
                {
                    if(u.FoodClassID == newRecord.FoodClassID)
                    {
                        Debug.WriteLine("Exist Food class {0} in inputRecord, can't add record. ", u.FoodClassID);
                        return false;
                    }
                }
                myInvestigationRecord.FoodIntakeRecords.Add(newRecord);
                int r;
                try
                {
                    r = DALDB.GetInstance().SaveChanges();
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine("Add new Record {0} in Investigation {1} falure.", newRecord.FoodClassID.ToString(), queueID);
                    Debug.WriteLine("Error message is {0}.", ex.ToString());
                    return false;
                }
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine("Add new Record {0} in Investigation {1} falure.", newRecord.FoodClassID.ToString(), queueID);
                    Debug.WriteLine("SaveChanges return {0}.", r.ToString());
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
        public bool CheckAndUpdateFoodIntakeRecordComplete(string queueID)
        {
            CustomerInvestigationRecord myInvestigationRecord = GetInvestigationRecord(queueID);
            if (myInvestigationRecord != null)
            {
                int FoodIntakeCount = GetInvestigationQuestions().Count();
                int HadInputCount = myInvestigationRecord.FoodIntakeRecords.Count();
                if (FoodIntakeCount == HadInputCount)
                {
                    myInvestigationRecord.InvestigationStatus = (int)InvestigateStatus.InvestigateInputOver;
                    int r;
                    try
                    {
                        r = DALDB.GetInstance().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(" Update status of Investigate {0} to InvestigateStatus.InvestigateInputOver error. Error message is {1}.",
                            queueID, ex.ToString());
                        return false;
                    }
                    if(r>0)
                    return true;
                    else
                    {
                        Debug.WriteLine(" Updat status of Investigate {0} to InvestigateStatus.InvestigateInputOver error. ");
                        Debug.WriteLine(" Save changes return {0}.", r);
                        return false;
                    }
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
        /// <summary>
        /// 获取一个调查的营养统计结果。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 统计食物摄入量
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public bool StatisticsFoodIntake(string queueID)
        {
            var p = from u in DALDB.GetInstance().FoodStatistcs
                    group u by u.FoodStatisticsName;
            var q = from u in DALDB.GetInstance().FoodIntakeRecords
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                if(q!=null && q.Count()>0)
                {
                    CustomerInvestigationRecord myInvestigationRecord = q.First().CustomerInvestigationRecord;
                    if(myInvestigationRecord.InvestigationStatus != (int)InvestigateStatus.InvestigateInputOver)
                    {
                        Debug.WriteLine("Investigation {0} havn't input over, can't do Statistics Action.", queueID);
                        return false;
                    }
                    foreach (var gp in p)
                    {
                        FoodStatisticsResult myFoodStatisticsResult = new FoodStatisticsResult();
                        myFoodStatisticsResult.CustomerInputRecordID = myInvestigationRecord.MyID;
                        myFoodStatisticsResult.FoodClassID = gp.First().FoodClassID;
                        myFoodStatisticsResult.FoodClassUnit = gp.First().FoodClassUnit;
                        myFoodStatisticsResult.FoodStaticName = gp.First().FoodStatisticsName;
                        myFoodStatisticsResult.SortID = gp.First().SortID;
                        myFoodStatisticsResult.FoodClassStatisticsValue = 0;
                        foreach (var item in gp)
                        {
                            foreach(var record in q)
                            {
                                if(record.FoodClassID == myFoodStatisticsResult.FoodClassID)
                                {
                                    myFoodStatisticsResult.FoodClassStatisticsValue += record.AverageADay;
                                    break;
                                }
                            }
                        }
                        myInvestigationRecord.FoodStatisticsResults.Add(myFoodStatisticsResult);
                        Debug.WriteLine("Investigation {0}, add food statistics info: food Class {1} have {2}.",
                            queueID, myFoodStatisticsResult.FoodStaticName, myFoodStatisticsResult.FoodClassStatisticsValue);
                    }
                    myInvestigationRecord.InvestigationStatus = (int)InvestigateStatus.StatisticsFoodIntakeOver;
                    int r;
                    try
                    {
                        r = DALDB.GetInstance().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Add new Food Class Statistics to DB error,  error info is {0}", ex.ToString());
                        return false;
                    }
                    if (r > 0)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Add new Food Class Statistics to DB error. ");
                        Debug.WriteLine("Save changes return {0}.", r.ToString());
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("have no food intake records, can't calculate.");
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("have no food statistics info, can't calculate.");
                return false;
            }
            
        }
        /// <summary>
        /// 统计营养摄入量
        /// 方法实现还没写完。
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public bool StatisticsNutritionIntake(string queueID)
        {
            var p = from u in DALDB.GetInstance().FoodIntakeRecords
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            if(p!=null && p.Count()>0)
            {
                CustomerInvestigationRecord myInvestigation = p.First().CustomerInvestigationRecord;
                if(myInvestigation.InvestigationStatus < (int)InvestigateStatus.InvestigateInputOver )
                {
                    Debug.WriteLine("Input of Investigation {0} haven't been complete. can't calculate Nutrition Intake.", queueID);
                    return false;
                }
                if(!CalcTypicalAverageIntake(queueID))
                {
                    Debug.WriteLine("Calculate Typical food average intake error. check Input and retry later.");
                    return false;
                }
                //计算每种食物的营养
                var NutritionElements = from u in DALDB.GetInstance().NuritiveElements
                                        select u;
                var myFoodNutrition = from u in DALDB.GetInstance().FoodNutritions
                                    select u;
                foreach(var u in NutritionElements)
                {
                    NutritionStatisticResult newResult = new NutritionStatisticResult()
                    {
                        CustomerInputRecordID = myInvestigation.MyID,
                        CustomerInvestigationRecord = myInvestigation,
                        ElementName = u.ElementName,
                        ElementUnit = u.ElementUnit,
                        ElementValue = 0,
                        ShowOnReport =u.ShowOnReport,
                         SortID = u.SortID
                    };
                    myInvestigation.NutritionStatisticResults.Add(newResult);
                }
                foreach(var u in myFoodNutrition)
                {
                    var myFoodIntake = from v in p
                                       where v.FoodClassID == u.FoodClassID
                                       select v;
                    if(myFoodIntake!=null && myFoodIntake.Count()==1)
                    {
                        foreach(var w in myInvestigation.NutritionStatisticResults)
                        {
                            if(w.ElementName == u.NuritiveElement.ElementName)
                            {
                                w.ElementValue += myFoodIntake.First().TypicalAverageADay * u.NutritionValue;
                            }
                        }
                    }
                }
                //save changes.
                int r;
                try
                {
                    r = DALDB.GetInstance().SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Save Food Nutrition Statistics info failure. exception info is {0}", ex.ToString());
                    return false;
                }
                if(r>0)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine("Save Food Nutrition Statistics info failure, save changes return {0}.", r);
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("Can't get food intake records of investigation {0}, can't calculate Nutrition Intake.", queueID);
                return false;
            }
            
        }

        /// <summary>
        /// 计算代表性食物平均每天摄入量。
        /// 还没有完成，没有加入计算类的典型食物摄入量。
        /// </summary>
        /// <param name="queueID"></param>
        private bool CalcTypicalAverageIntake(string queueID)
        {
            var p = from u in DALDB.GetInstance().FoodIntakeRecords
                    where u.CustomerInvestigationRecord.QueueID == queueID
                    select u;
            var q = from u in DALDB.GetInstance().FoodClasses
                    select u;
            #region 计算录入食物类的代表性食物类平均每天摄入量
            if (p!=null && p.Count()>0)
            {
                foreach(var u in p)
                {
                    if (u.RecordMode == (int)RecordMode.Input)
                    {
                        var o = (from v in q
                                 where v.MyID == u.FoodClassID
                                 select v).First();
                        u.TypicalAverageADay = u.AverageADay * o.Percent / 100;
                    }
                }
            }
            else
            {
                Debug.WriteLine("no Food Intake Record of Investigation {0}.", queueID);
                return false;
            }
            #endregion
            CustomerInvestigationRecord myInvestigation = p.First().CustomerInvestigationRecord;
            #region 计算代表性食物类平均每天摄入量
            var calcFoodClass = from u in DALDB.GetInstance().FoodClasses
                                where u.NeedInput == false
                                select u;
            if (calcFoodClass != null && calcFoodClass.Count() > 0)
            {
                foreach (var t in calcFoodClass)
                {
                    double foodInputValue = -1;
                    string parentID = t.ParentID;
                    foreach (var t2 in p)
                    {
                        if (t2.FoodClassID == parentID)
                        {
                            foodInputValue = (double)t2.AverageADay;
                            break;
                        }
                    }
                    if (foodInputValue == -1)
                    {
                        Debug.WriteLine("haven't found the parent food class {0} averageDay intake value.", t.ParentName);
                        break;
                    }
                    FoodIntakeRecord newIntake = new FoodIntakeRecord()
                    {
                        AverageADay = 0,
                        CustomerInputRecordID = myInvestigation.MyID,
                        CustomerInvestigationRecord = myInvestigation,
                        FoodClassID = t.MyID,
                        Intake = 0,
                        IntakeFrequency = 0,
                        RecordMode = (int)RecordMode.Calculate,
                        TypicalAverageADay = foodInputValue * t.Percent / 100
                    };
                    myInvestigation.FoodIntakeRecords.Add(newIntake);
                }
            }
            #endregion
            int r;
            try
            {
                r = DALDB.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Save Typical food average intake Error. Error message is {0}.", ex.ToString());
                throw;
            }
            if(r>0)
            {
                return true;
            }
            else
            {
                Debug.WriteLine("Save Typical food average intake error. save changes return {0}.", r.ToString());
                return false;
            }
        }

    }
}
