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
        public Mainform()
        {
            InitializeComponent();
            #region 载入控件预设值
            txtInvestigationDate.Text = DateTime.Now.ToShortDateString();

            int InvestigationCount = new BLRecord().GetInvestigationRecordsCount();
            lblInvestigationCount.Text = InvestigationCount.ToString() + "份";
            #endregion
        }

        private void btnViewRecords_Click(object sender, EventArgs e)
        {
            InvestigationListForm newListForm = new InvestigationListForm();
            newListForm.Show();
        }

        private void btnNewInvestigation_Click(object sender, EventArgs e)
        {
            #region save CumstomerInfo and Investigation base info.
            if(!ExistCustomer)
            {
                //save Customer info

            }
            //save Investigation base info

            #endregion
            #region change to Investigation question forms.
            InvestigationForm newInvestigationForm = new InvestigationForm();
            #endregion
        }
        /// <summary>
        /// 保健手册录入的时候，判断是否已有该用户资料，如果有，则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHealthBookID_TextChanged(object sender, EventArgs e)
        {
            CustomerInfo myCustomerInfo;
            if(ExistCustomerInDB(txtHealthBookID.Text,txtHealthBookID.Name,out myCustomerInfo))
            {
                ExistCustomer = true;
                LoadCustomerInfo(myCustomerInfo);
            }
            else
            {
                ExistCustomer = false;
            }
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
        private void LoadCustomerInfo(CustomerInfo myCustomerInfo)
        {
            if (myCustomerInfo == null)
                return;

            if(txtHealthBookID.Text != myCustomerInfo.HealthBookID)
            {
                txtHealthBookID.Text = myCustomerInfo.HealthBookID;
            }
            if(txtCustomerName.Text != myCustomerInfo.Name)
            {
                txtCustomerName.Text = myCustomerInfo.Name;
            }
            txtCustomerBirthday.Text = ((DateTime)myCustomerInfo.Birthday).ToShortDateString();
            txtHeight.Text = myCustomerInfo.Height.ToString();
            txtWeightBefore.Text = myCustomerInfo.BeforeWeight.ToString();
        }
        /// <summary>
        /// 孕妇姓名录入的时候，判断是否已有该用户资料，如果由，则自动导入。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            CustomerInfo myCustomerInfo;
            if (ExistCustomerInDB(txtCustomerName.Text, txtCustomerName.Name, out myCustomerInfo))
            {
                ExistCustomer = true;
                LoadCustomerInfo(myCustomerInfo);
            }
            else
            {
                ExistCustomer = false;
            }
        }


    }
}
