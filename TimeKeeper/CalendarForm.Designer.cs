namespace TimeKeeper
{
    partial class CalendarForm
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
            this.mroCalendarCLD = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // mroCalendarCLD
            // 
            this.mroCalendarCLD.Location = new System.Drawing.Point(18, 18);
            this.mroCalendarCLD.MaxSelectionCount = 1;
            this.mroCalendarCLD.Name = "mroCalendarCLD";
            this.mroCalendarCLD.TabIndex = 0;
            this.mroCalendarCLD.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnDateChanged_CalendarCLD);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 198);
            this.Controls.Add(this.mroCalendarCLD);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CalendarForm";
            this.Text = "날짜 선택";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mroCalendarCLD;
    }
}