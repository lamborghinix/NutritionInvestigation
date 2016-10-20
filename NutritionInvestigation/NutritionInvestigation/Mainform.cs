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
        bool ExistCustomer;
        bool ExistInvestigation;
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
        /// 孕妇姓名录入的时候，判断是否已有该用户资料，如果由，则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            //CustomerInfo myCustomerInfo;
            if (ExistCustomerInDB(txtCustomerName.Text, txtCustomerName.Name, out myCustomerInfo))
            {
                ExistCustomer = true;
                LoadCustomerInfo();
            }
            else
            {
                ExistCustomer = false;
            }
        }
        /// <summary>
        /// 判断是否已存在该记录，如果存在则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueueID_TextChanged(object sender, EventArgs e)
        {
            if (ExistInvestigationInDB(txtQueueID.Text, out myInvestigation))
            {
                ExistInvestigation = true;
                LoadInvestigation();
                myCustomerInfo = new BLRecord().GetCustomersInfo(myInvestigation.CustomerID);
                if (myCustomerInfo != null)
                {
                    ExistCustomer = true;
                    LoadCustomerInfo();
                }
                else
                {
                    ExistCustomer = false;
                }
            }
            else
            {
                ExistInvestigation = false;
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
                ExistCustomer = true;
                LoadCustomerInfo();
            }
            else
            {
                ExistCustomer = false;
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
            bool saveInvestigationResult;
            string saveCustomerMessage, saveInvestigationMessage;
            #region save CumstomerInfo and Investigation base info.
            if (ExistInvestigation && myInvestigation.InvestigationStatus < (int)InvestigateStatus.StatisticsFoodIntakeOver)
            {
                //saveInvestigationResult = UpdateInvestigationBaseinfo(out saveInvestigationMessage);
            }
            else
            {
                //saveInvestigationResult = AddNewInvestigationBaseInfo(out saveInvestigationMessage);
            }
            if (ExistCustomer)
            {
                //saveCustomerResult = UpdateCustomerInfo(out saveCustomerMessage);
            }
            else
            {
                //saveCustomerResult = AddNewCustomerInfo(out saveCustomerMessage);
            }


            #endregion
            #region change to Investigation question forms.
            /*
            if (saveCustomerResult && saveInvestigationResult)
            {
                InvestigationForm newInvestigationForm = new InvestigationForm();
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
            */
            #endregion
        }
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
                QueueID = txtQueueID.Text
            };
            eMessage = string.Empty;
            return true;
        }

        #endregion
        #region 私有方法
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
                ExistInvestigation = true;
                return true; }
            else
            {
                ExistInvestigation = false;
                return false;
            }
        }
        /// <summary>
        /// 检查必要输入是否完整。
        /// </summary>
        /// <param name="eMessage"></param>
        /// <returns></returns>
        private bool CheckInput(out string eMessage)
        {
            if (txtQueueID.Text == string.Empty)
            {
                eMessage = "Queue ID haven't input. ";
                return false;
            }
            if (txtCustomerName.Text == string.Empty)
            {
                eMessage = "Customer name haven't input. ";
                return false;
            }
            eMessage = string.Empty;
            return true;
        }
        /// <summary>
        ///判断DB中是否有用户
        /// </summary>
        /// <param name="text"></param>
        /// <param name="name"></param>
        /// <param name="myCustomerInfo"></param>
        /// <returns></returns>
        private bool ExistCustomerInDB(string text, string name, out CustomerInfo myCustomerInfo)
        {
            List<CustomerInfo> users;
            if (name == txtHealthBookID.Name)
            {

                return new BLRecord().CheckCustomerExist(text, string.Empty, out myCustomerInfo, out users);
            }
            else if (name == txtCustomerName.Name)
            {
                bool exist = new BLRecord().CheckCustomerExist(string.Empty, text, out myCustomerInfo, out users);
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
        #endregion
    }
}
