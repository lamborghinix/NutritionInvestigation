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

        private void button2_Click(object sender, EventArgs e)
        {
            myDBEntities context = new myDBEntities();
            FoodClass myFoodClass;
            var q = from u in context.FoodClasses
                    where u.MyID == 1
                    select u;
            if(q!=null && q.Count()>0)
            {
                myFoodClass = q.First();
                MessageBox.Show(myFoodClass.MyID.ToString());
                context.FoodClasses.Remove(myFoodClass);
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("not exsit.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myDBEntities context = new myDBEntities();
            FoodClass myFoodClass;
            var q = from u in context.FoodClasses
                    //where u.MyID == 1
                    select u;
            if (q != null && q.Count() > 0)
            {
                myFoodClass = q.ToList()[q.Count() - 1];
                MessageBox.Show(myFoodClass.MyID.ToString());
                myFoodClass.ParentName = "no name";
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("not exsit.");
            }
        }
    }
}
