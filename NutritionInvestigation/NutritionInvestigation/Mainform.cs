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
    public partial class Mainform : Form
    {
        CustomerInfo myCustomerInfo;
        CustomerInvestigationRecord myInvestigation;
        public Mainform()
        {
            InitializeComponent();
            #region 载入控件预设值
            txtInvestigationDate.Text = DateTime.Now.ToShortDateString();

            int InvestigationCount = new BLRecord().GetInvestigationRecordsCount();
            lblInvestigationCount.Text = InvestigationCount.ToString() + "份";
            txtInvestigationDate.Text = DateTime.Now.ToShortDateString();
            #endregion
        }
        #region 控件事件
        /// <summary>
        /// 判断是否已存在该记录，如果存在则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueueID_TextChanged(object sender, EventArgs e)
        {
            if (ExistInvestigationInDB(txtQueueID.Text, out myInvestigation))
            {
                LoadInvestigation();
                myCustomerInfo = new BLRecord().GetCustomersInfo(myInvestigation.HealthBookID);
                if (myCustomerInfo != null)
                {
                    LoadCustomerInfo();
                }
            }
        }
        /// <summary>
        /// 保健手册录入的时候，判断是否已有该用户资料，如果有，则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHealthBookID_TextChanged(object sender, EventArgs e)
        {
            if (ExistCustomerInDB(txtHealthBookID.Text, txtHealthBookID.Name, out myCustomerInfo))
            {
                LoadCustomerInfo();
            }
        }
        private void btnViewRecords_Click(object sender, EventArgs e)
        {
            InvestigationListForm newListForm = new InvestigationListForm();
            newListForm.Show();
        }
        /// <summary>
        /// 开始新的营养调查。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewInvestigation_Click(object sender, EventArgs e)
        {
            string EMessage;
            CustomerInfo newCustomerInfo;
            CustomerInvestigationRecord newInvestigation;
            if (!CheckInput(out newCustomerInfo, out newInvestigation, out EMessage))
            {
                MessageBox.Show(EMessage);
                return;
            }
            bool saveCustomerResult;
            bool saveInvestigationResult = false;
            string saveCustomerMessage, saveInvestigationMessage;
            #region save CumstomerInfo and Investigation base info.
            CustomerInfo savedCustomerInfo;
            CustomerInvestigationRecord savedInvestigation;
            saveCustomerResult = UpdateCustomerInfo(newCustomerInfo, myCustomerInfo, out savedCustomerInfo, out saveCustomerMessage);
            if (saveCustomerResult)
            {
                newInvestigation.CustomerID = savedCustomerInfo.MyID;
                newInvestigation.CustomerName = savedCustomerInfo.Name;
                saveInvestigationResult = UpdateInvestigationBaseInfo(newInvestigation, myInvestigation,out savedInvestigation, out saveInvestigationMessage);
            }
            else
            {
                savedInvestigation = null;
                saveInvestigationMessage = "Customer info error, can't create Investigation.";
            }
            #endregion
            #region change to Investigation question forms.
            if (saveInvestigationResult)
            {
                myInvestigation = savedInvestigation;
                myCustomerInfo = savedCustomerInfo;
                InvestigationForm newInvestigationForm = new InvestigationForm();
                newInvestigationForm.Show();
            }
            else
            {
                if (!saveCustomerResult)
                {
                    MessageBox.Show(saveCustomerMessage);
                }
                if (!saveInvestigationResult)
                {
                    MessageBox.Show(saveInvestigationMessage);
                }
            }
            #endregion
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 检查输入是否合法，并将输入的值记录到对象中。
        /// </summary>
        /// <param name="myCustomerInfo"></param>
        /// <param name="myInvestigation"></param>
        /// <param name="eMessage"></param>
        /// <returns></returns>
        private bool CheckInput(out CustomerInfo myCustomerInfo, out CustomerInvestigationRecord myInvestigation, out string eMessage)
        {
            double beforeWeight, currentWeight, height;
            int weeks;
            DateTime customerBirthday, investigateDate;

            myCustomerInfo = null;
            myInvestigation = null;
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    if (ctl.Text == string.Empty)
                    {
                        eMessage = "Control " + ctl.Name + "'s test is empty.";
                        return false;
                    }
                }
            }
            try
            {
                beforeWeight = Convert.ToDouble(txtWeightBefore.Text);
                height = Convert.ToDouble(txtHeight.Text);
                customerBirthday = Convert.ToDateTime(txtCustomerBirthday.Text);
                weeks = Convert.ToInt32(txtWeeks.Text);
                currentWeight = Convert.ToDouble(txtWeightNow.Text);
                investigateDate = Convert.ToDateTime(txtInvestigationDate.Text);
            }
            catch (Exception ex)
            {
                eMessage = "非法输入：" + ex.ToString();
                return false;
            }
            myCustomerInfo = new CustomerInfo()
            {
                BeforeWeight = beforeWeight,
                Birthday = customerBirthday,
                HealthBookID = txtHealthBookID.Text,
                Height = height,
                Name = txtCustomerName.Text
            };
            myInvestigation = new CustomerInvestigationRecord()
            {
                AuditorName = txtAuditor.Text,
                CurrentWeight = currentWeight,
                GestationalWeek = weeks,
                InvestigationDate = investigateDate,
                InvestigatorName = txtInvestigatorName.Text,
                QueueID = txtQueueID.Text,
                HealthBookID = txtHealthBookID.Text
            };
            eMessage = string.Empty;
            return true;
        }
        /// <summary>
        /// 载入调查记录信息。
        /// </summary>
        private void LoadInvestigation()
        {
            txtAuditor.Text = myInvestigation.AuditorName;
            txtInvestigationDate.Text = ((DateTime)myInvestigation.InvestigationDate).ToShortDateString();
            txtInvestigatorName.Text = myInvestigation.InvestigatorName;
            txtWeeks.Text = myInvestigation.GestationalWeek.ToString();
            txtWeightNow.Text = myInvestigation.CurrentWeight.ToString();
        }
        /// <summary>
        /// 判断是否已保存有这个调查的记录。
        /// </summary>
        /// <param name="queueID"></param>
        /// <param name="myInvestigation"></param>
        /// <returns></returns>
        private bool ExistInvestigationInDB(string queueID, out CustomerInvestigationRecord myInvestigation)
        {
            if(new BLRecord().CheckInvestigationExist(queueID,out myInvestigation))
            {
                return true; }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///判断DB中是否有用户
        /// </summary>
        /// <param name="textValue"></param>
        /// <param name="controlName"></param>
        /// <param name="myCustomerInfo"></param>
        /// <returns></returns>
        private bool ExistCustomerInDB(string textValue, string controlName, out CustomerInfo myCustomerInfo)
        {
            List<CustomerInfo> users;
            if (controlName == txtHealthBookID.Name)
            {

                return new BLRecord().CheckCustomerExist(textValue, string.Empty, out myCustomerInfo, out users);
            }
            else if (controlName == txtCustomerName.Name)
            {
                bool exist = new BLRecord().CheckCustomerExist(string.Empty, textValue, out myCustomerInfo, out users);
                if (exist && users != null && users.Count() == 1)
                {
                    myCustomerInfo = users.First();
                    return true;
                }
                else
                {
                    myCustomerInfo = null;
                    return false;
                }
            }
            myCustomerInfo = null;
            return false;
        }
        /// <summary>
        /// 载入孕妇信息。
        /// </summary>
        private void LoadCustomerInfo()
        {
            if (myCustomerInfo == null)
                return;

            if (txtHealthBookID.Text != myCustomerInfo.HealthBookID)
            {
                txtHealthBookID.Text = myCustomerInfo.HealthBookID;
            }
            if (txtCustomerName.Text != myCustomerInfo.Name)
            {
                txtCustomerName.Text = myCustomerInfo.Name;
            }
            txtCustomerBirthday.Text = ((DateTime)myCustomerInfo.Birthday).ToShortDateString();
            txtHeight.Text = myCustomerInfo.Height.ToString();
            txtWeightBefore.Text = myCustomerInfo.BeforeWeight.ToString();
        }
        /*
        private bool AddNewCustomerInfo(CustomerInfo newCustomerInfo, out string saveCustomerMessage)
        {
            bool result = new BLRecord().AddCustomer(newCustomerInfo);
            if (result)
            {
                saveCustomerMessage = "Add Customer successful.";
            }
            else
            {
                saveCustomerMessage = "Add Customer Failure.";
            }
            return result;
        }

        private bool AddNewInvestigationBaseInfo(CustomerInvestigationRecord newInvestigation, out string saveInvestigationMessage)
        {
            return new BLRecord().AddInvestigationBaseInfo(newInvestigation, out saveInvestigationMessage);
        }
        */
        private bool UpdateCustomerInfo(CustomerInfo newCustomerInfo, CustomerInfo myCustomerInfo,out CustomerInfo savedCustomerInfo, out string saveCustomerMessage)
        {
            //创建新的孕妇信息记录
            if(myCustomerInfo==null || newCustomerInfo.HealthBookID != myCustomerInfo.HealthBookID)
            {
                return new BLRecord().UpdateCustomerInfo(newCustomerInfo, newCustomerInfo.HealthBookID, out savedCustomerInfo, out saveCustomerMessage);
            }
            //判断信息是否更新。
            if (newCustomerInfo.Name == myCustomerInfo.Name
                && newCustomerInfo.Birthday == myCustomerInfo.Birthday
                && newCustomerInfo.Height == myCustomerInfo.Height
                && newCustomerInfo.BeforeWeight == myCustomerInfo.BeforeWeight)
            {
                saveCustomerMessage = "customer info is the same, not need to update.";
                savedCustomerInfo = myCustomerInfo;
                return true;
            }
            else
            {
                return new BLRecord().UpdateCustomerInfo(newCustomerInfo, myCustomerInfo.HealthBookID,out savedCustomerInfo, out saveCustomerMessage);
            }
        }

        private bool UpdateInvestigationBaseInfo(CustomerInvestigationRecord newInvestigation, CustomerInvestigationRecord myInvestigation,
            out CustomerInvestigationRecord savedInvestigation, out string saveInvestigationMessage)
        {
            if(myInvestigation==null || newInvestigation.QueueID != myInvestigation.QueueID)
            {
                return new BLRecord().UpdateInvestigationRecord(newInvestigation, newInvestigation.QueueID, out savedInvestigation, out saveInvestigationMessage);
            }
            if(newInvestigation.InvestigationDate == myInvestigation.InvestigationDate
                && newInvestigation.AuditorName == myInvestigation.AuditorName
                && newInvestigation.CurrentWeight == myInvestigation.CurrentWeight
                && newInvestigation.GestationalWeek == myInvestigation.GestationalWeek
                && newInvestigation.InvestigatorName == myInvestigation.InvestigatorName
                && newInvestigation.CustomerID == myInvestigation.CustomerID
                    && newInvestigation.CustomerName == myInvestigation.CustomerName)
            {
                saveInvestigationMessage = "Investigation info is the same, no need to update.";
                savedInvestigation = myInvestigation;
                return true;
            }
            return new BLRecord().UpdateInvestigationRecord(newInvestigation, myInvestigation.QueueID, out savedInvestigation, out saveInvestigationMessage);
        }
        #endregion
    }
}
