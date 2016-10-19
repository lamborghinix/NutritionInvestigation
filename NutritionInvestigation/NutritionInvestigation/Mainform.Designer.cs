namespace NutritionInvestigation
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblInvestigationCount = new System.Windows.Forms.Label();
            this.btnViewRecords = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtQueueID = new System.Windows.Forms.TextBox();
            this.txtHealthBookID = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerBirthday = new System.Windows.Forms.TextBox();
            this.txtWeeks = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWeightBefore = new System.Windows.Forms.TextBox();
            this.txtWeightNow = new System.Windows.Forms.TextBox();
            this.txtInvestigationDate = new System.Windows.Forms.TextBox();
            this.txtInvestigatorName = new System.Windows.Forms.TextBox();
            this.txtAuditor = new System.Windows.Forms.TextBox();
            this.btnNewInvestigation = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::NutritionInvestigation.Properties.Resources._1;
            this.panel2.Controls.Add(this.btnNewInvestigation);
            this.panel2.Controls.Add(this.txtAuditor);
            this.panel2.Controls.Add(this.txtInvestigatorName);
            this.panel2.Controls.Add(this.txtInvestigationDate);
            this.panel2.Controls.Add(this.txtWeightNow);
            this.panel2.Controls.Add(this.txtWeightBefore);
            this.panel2.Controls.Add(this.txtHeight);
            this.panel2.Controls.Add(this.txtWeeks);
            this.panel2.Controls.Add(this.txtCustomerBirthday);
            this.panel2.Controls.Add(this.txtCustomerName);
            this.panel2.Controls.Add(this.txtHealthBookID);
            this.panel2.Controls.Add(this.txtQueueID);
            this.panel2.Location = new System.Drawing.Point(406, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 800);
            this.panel2.TabIndex = 1;
            // 
            // lblInvestigationCount
            // 
            this.lblInvestigationCount.AutoSize = true;
            this.lblInvestigationCount.Location = new System.Drawing.Point(28, 712);
            this.lblInvestigationCount.Name = "lblInvestigationCount";
            this.lblInvestigationCount.Size = new System.Drawing.Size(23, 12);
            this.lblInvestigationCount.TabIndex = 1;
            this.lblInvestigationCount.Text = "0份";
            // 
            // btnViewRecords
            // 
            this.btnViewRecords.Location = new System.Drawing.Point(30, 749);
            this.btnViewRecords.Name = "btnViewRecords";
            this.btnViewRecords.Size = new System.Drawing.Size(119, 23);
            this.btnViewRecords.TabIndex = 2;
            this.btnViewRecords.Text = "点击查看记录";
            this.btnViewRecords.UseVisualStyleBackColor = true;
            this.btnViewRecords.Click += new System.EventHandler(this.btnViewRecords_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::NutritionInvestigation.Properties.Resources._2;
            this.panel1.Controls.Add(this.btnViewRecords);
            this.panel1.Controls.Add(this.lblInvestigationCount);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 800);
            this.panel1.TabIndex = 0;
            // 
            // txtQueueID
            // 
            this.txtQueueID.Location = new System.Drawing.Point(75, 162);
            this.txtQueueID.Name = "txtQueueID";
            this.txtQueueID.Size = new System.Drawing.Size(202, 21);
            this.txtQueueID.TabIndex = 0;
            // 
            // txtHealthBookID
            // 
            this.txtHealthBookID.Location = new System.Drawing.Point(409, 162);
            this.txtHealthBookID.Name = "txtHealthBookID";
            this.txtHealthBookID.Size = new System.Drawing.Size(202, 21);
            this.txtHealthBookID.TabIndex = 1;
            this.txtHealthBookID.TextChanged += new System.EventHandler(this.txtHealthBookID_TextChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(75, 306);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(202, 21);
            this.txtCustomerName.TabIndex = 2;
            this.txtCustomerName.TextChanged += new System.EventHandler(this.txtCustomerName_TextChanged);
            // 
            // txtCustomerBirthday
            // 
            this.txtCustomerBirthday.Location = new System.Drawing.Point(409, 306);
            this.txtCustomerBirthday.Name = "txtCustomerBirthday";
            this.txtCustomerBirthday.Size = new System.Drawing.Size(202, 21);
            this.txtCustomerBirthday.TabIndex = 3;
            // 
            // txtWeeks
            // 
            this.txtWeeks.Location = new System.Drawing.Point(743, 306);
            this.txtWeeks.Name = "txtWeeks";
            this.txtWeeks.Size = new System.Drawing.Size(202, 21);
            this.txtWeeks.TabIndex = 4;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(75, 456);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(202, 21);
            this.txtHeight.TabIndex = 5;
            // 
            // txtWeightBefore
            // 
            this.txtWeightBefore.Location = new System.Drawing.Point(409, 456);
            this.txtWeightBefore.Name = "txtWeightBefore";
            this.txtWeightBefore.Size = new System.Drawing.Size(202, 21);
            this.txtWeightBefore.TabIndex = 6;
            // 
            // txtWeightNow
            // 
            this.txtWeightNow.Location = new System.Drawing.Point(743, 456);
            this.txtWeightNow.Name = "txtWeightNow";
            this.txtWeightNow.Size = new System.Drawing.Size(202, 21);
            this.txtWeightNow.TabIndex = 7;
            // 
            // txtInvestigationDate
            // 
            this.txtInvestigationDate.Location = new System.Drawing.Point(75, 605);
            this.txtInvestigationDate.Name = "txtInvestigationDate";
            this.txtInvestigationDate.Size = new System.Drawing.Size(202, 21);
            this.txtInvestigationDate.TabIndex = 8;
            // 
            // txtInvestigatorName
            // 
            this.txtInvestigatorName.Location = new System.Drawing.Point(409, 605);
            this.txtInvestigatorName.Name = "txtInvestigatorName";
            this.txtInvestigatorName.Size = new System.Drawing.Size(202, 21);
            this.txtInvestigatorName.TabIndex = 9;
            // 
            // txtAuditor
            // 
            this.txtAuditor.Location = new System.Drawing.Point(743, 605);
            this.txtAuditor.Name = "txtAuditor";
            this.txtAuditor.Size = new System.Drawing.Size(202, 21);
            this.txtAuditor.TabIndex = 10;
            // 
            // btnNewInvestigation
            // 
            this.btnNewInvestigation.Location = new System.Drawing.Point(786, 716);
            this.btnNewInvestigation.Name = "btnNewInvestigation";
            this.btnNewInvestigation.Size = new System.Drawing.Size(159, 56);
            this.btnNewInvestigation.TabIndex = 11;
            this.btnNewInvestigation.Text = "开始测试";
            this.btnNewInvestigation.UseVisualStyleBackColor = true;
            this.btnNewInvestigation.Click += new System.EventHandler(this.btnNewInvestigation_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 801);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Mainform";
            this.Text = "孕期食物频率调查系统";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblInvestigationCount;
        private System.Windows.Forms.Button btnViewRecords;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNewInvestigation;
        private System.Windows.Forms.TextBox txtAuditor;
        private System.Windows.Forms.TextBox txtInvestigatorName;
        private System.Windows.Forms.TextBox txtInvestigationDate;
        private System.Windows.Forms.TextBox txtWeightNow;
        private System.Windows.Forms.TextBox txtWeightBefore;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWeeks;
        private System.Windows.Forms.TextBox txtCustomerBirthday;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtHealthBookID;
        private System.Windows.Forms.TextBox txtQueueID;
    }
}