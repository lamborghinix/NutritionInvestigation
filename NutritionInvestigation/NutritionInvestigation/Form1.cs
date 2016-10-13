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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myDBEntities context = new myDBEntities();
            FoodClass newFoodClass = new FoodClass();
            newFoodClass.FoodClassName = "粮谷类";
            newFoodClass.FoodIndex = "F01";
            newFoodClass.ParentID = 0;
            newFoodClass.ParentName = string.Empty;

            context.FoodClasses.Add(newFoodClass);
            context.SaveChanges();

            MessageBox.Show("add data OK.");
        }
    }
}
