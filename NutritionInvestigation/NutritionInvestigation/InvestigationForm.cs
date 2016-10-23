using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NutritionInvestigation
{
    public partial class InvestigationForm : Form
    {
        CustomerInvestigationRecord myInvestigation;
        List<FoodIntakeRecord> myFoodIntakeList;
        List<FoodClass> allIntakeFoodClassList;
        List<FoodClass> haveInputFoodClassList;
        FoodClass currentInputFoodClass;
        FoodIntakeRecord currentFoodIntakeRecord;
        int FoodClassIndex;
        UCWeekly myUCWeekly;
        UCDaily myUCDaily;
        UCMonthly myUCMonthly;

        public InvestigationForm(CustomerInvestigationRecord myInvestigationRecord)
        {
            myInvestigation = myInvestigationRecord;
            InitializeComponent();
            if(myInvestigationRecord.InvestigationStatus>(int)InvestigateStatus.InvestigateInputting)
            {
                MessageBox.Show("当前营养调查表已经填写完毕。无需再填写。");
                this.Close();
            }
            InitializeFoodInputList(myInvestigationRecord.QueueID);
            /*
            FoodClass haventInput = GetHaventInputFoodClass();
            if(haventInput!=null)
            {
                LoadInputUC(haventInput);
            }*/
            if(allIntakeFoodClassList.Count()>0)
            {
                FoodClassIndex = 0;
                ShowInputQuestion(FoodClassIndex);
                
            }
        }

        private void ShowInputQuestion(int foodClassIndex)
        {
            currentInputFoodClass = allIntakeFoodClassList[FoodClassIndex];

            currentFoodIntakeRecord = GetFoodInputRecord(currentInputFoodClass);
            LoadInputUC(currentInputFoodClass, currentFoodIntakeRecord);

        }

        /// <summary>
        /// 载入当前食物类型的输入界面，以及已有输入记录。
        /// </summary>
        /// <param name="currentInputFoodClass"></param>
        /// <param name="currentFoodIntakeRecord"></param>
        private void LoadInputUC(FoodClass currentInputFoodClass, FoodIntakeRecord currentFoodIntakeRecord)
        {
            myUCDaily = null;
            myUCWeekly = null;
            myUCMonthly = null;
            panel2.Controls.Clear();
       
            if(currentInputFoodClass.RecordMode ==1)
            {
                myUCWeekly = new UCWeekly();
                panel2.Controls.Add(myUCWeekly);
                //传递参数。
                myUCWeekly.myFoodClass = currentInputFoodClass;
                myUCWeekly.myIntakeRecord = currentFoodIntakeRecord;
                myUCWeekly.Show();
            }
            else if(currentInputFoodClass.RecordMode ==2)
            {
                myUCDaily = new UCDaily();
                panel2.Controls.Add(myUCDaily);

                myUCDaily.myFoodClass = currentInputFoodClass;
                myUCDaily.myIntakeRecord = currentFoodIntakeRecord;
                myUCDaily.Show();
            }
            else
            {
                myUCMonthly = new UCMonthly();
                panel2.Controls.Add(myUCMonthly);

                myUCMonthly.Show();
            }
        }
        /// <summary>
        /// 获取当前食物类型已输入的值
        /// </summary>
        /// <param name="currentInputFoodClass"></param>
        /// <returns></returns>
        private FoodIntakeRecord GetFoodInputRecord(FoodClass currentInputFoodClass)
        {
            foreach (var u in myFoodIntakeList)
            {
                if (u.FoodClassID == currentInputFoodClass.MyID)
                    return u;
            }
            return null;
        }

        /// <summary>
        /// 初始化食品摄入列表。。
        ///1,获取所有需要摄入的食品列表，2，获取已经填写了的食品摄入列表。 
        /// </summary>
        private void InitializeFoodInputList(string queueID)
        {
            allIntakeFoodClassList = new BLRecord().GetIntakeFoodClass();
            myFoodIntakeList = new BLRecord().GetFoodIntakeList(queueID);
            if(allIntakeFoodClassList == null)
            {
                MessageBox.Show("出现错误，没有食物类型可用输入。");
                this.Close();
            }
            if (myFoodIntakeList == null)
            {
                myFoodIntakeList = new List<FoodIntakeRecord>();
            }
            else
            {
                if (myFoodIntakeList.Count() >= allIntakeFoodClassList.Count())
                {
                    MessageBox.Show("出现错误，已完成所有食物类型输入。");
                    this.Close();
                }
            }
            haveInputFoodClassList = new List<FoodClass>();
            foreach (var u in allIntakeFoodClassList)
            {
                bool haveInput = false;
                foreach (var v in myFoodIntakeList)
                {
                    if (v.FoodClassID == u.MyID)
                    {
                        haveInput = true;
                    }
                    if (haveInput)
                    {
                        haveInputFoodClassList.Add(u);
                        continue;
                    }
                }
            }
            int percent = haveInputFoodClassList.Count() * 100 / allIntakeFoodClassList.Count();
            lblPercent.Text = percent.ToString();
        }

        //private FoodClass GetHaventInputFoodClass()
        //{
        //    FoodClass currentInputFoodClass = null;
        //    foreach (var u in allIntakeFoodClassList)
        //    {
        //        bool haveInput = false;
        //        foreach (var v in myFoodIntakeList)
        //        {
        //            if (v.FoodClassID == u.MyID)
        //            {
        //                haveInput = true;
        //                break;
        //            }
        //        }
        //        if (!haveInput)
        //        {
        //            currentInputFoodClass = u;
        //            //停止遍历
        //            break;
        //        }
        //    }
        //    return currentInputFoodClass;
        //    //if(currentInputFoodClass!=null)
        //    //{
        //    //    return currentInputFoodClass;
        //    //}
        //}

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(currentFoodIntakeRecord ==null)
            {
                currentFoodIntakeRecord = new FoodIntakeRecord();
                currentFoodIntakeRecord.FoodClassID = currentInputFoodClass.MyID;
                currentFoodIntakeRecord.RecordMode = currentInputFoodClass.RecordMode;
                myInvestigation.FoodIntakeRecords.Add(currentFoodIntakeRecord);
                    }
            if(myUCWeekly!=null)
            {
                if( myUCWeekly.HasFilledInfo)
                {
                    try
                    {
                        currentFoodIntakeRecord.IntakeFrequency = Convert.ToInt32(myUCWeekly.FoodIntakeFrequency);
                        currentFoodIntakeRecord.Intake = Convert.ToDouble(myUCWeekly.FoodIntakeValue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("非法输入： " + ex.ToString());
                        return;
                    }
                    if (new BLRecord().AddFoodIntakeRecord(myInvestigation.QueueID, currentFoodIntakeRecord))
                    {
                        //LoadQuestion();
                        FoodClassIndex++;
                        ShowInputQuestion(FoodClassIndex);
                    }
                    else
                    {
                        MessageBox.Show("记录数据到数据库失败。");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("输入不完整，不能进入下一题");
                    return;
                }

            }

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            currentFoodIntakeRecord.Intake = 0;
            currentFoodIntakeRecord.IntakeFrequency = 0;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}
