using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NutritionInvestigation
{
    public partial class UCWeekly : UserControl
    {
        public FoodClass myFoodClass
        {
            //get { }
            set
            {
                if (value != null)
                {
                    lblClass1.Text = value.ParentName;
                    lblClass2.Text = value.FoodClassName;
                    lblTypicalFood.Text = value.TypicalFood;
                }
            }
        }
        public FoodIntakeRecord myIntakeRecord
        {
            set
            {
                if (value != null)
                {
                    txtTimesPerWeek.Text = value.IntakeFrequency.ToString();
                    txtIntakeValue.Text = value.Intake.ToString();
                }
            }
        }
        public bool HasFilledInfo
        {
            get
            {
                if (txtIntakeValue.Text != string.Empty && txtTimesPerWeek.Text != string.Empty)
                    return true;
                else
                    return false;
            }
        }
        public string FoodIntakeFrequency { get { return txtTimesPerWeek.Text; } set { } }

        public string FoodIntakeValue { get { return txtIntakeValue.Text; } set { } }

        public UCWeekly()
        {
            InitializeComponent();
        }
    }
}
