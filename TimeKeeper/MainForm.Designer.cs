namespace TimeKeeper
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mroTaskItemsCBL = new System.Windows.Forms.ComboBox();
            this.mroKeepCBTN = new System.Windows.Forms.CheckBox();
            this.mroTotalTimeLBL = new System.Windows.Forms.Label();
            this.mroTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mroTodayLBL = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moCopyright2Lbl = new System.Windows.Forms.Label();
            this.moCopyrightLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mroTaskItemsCBL
            // 
            this.mroTaskItemsCBL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mroTaskItemsCBL.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mroTaskItemsCBL.FormattingEnabled = true;
            this.mroTaskItemsCBL.Location = new System.Drawing.Point(68, 83);
            this.mroTaskItemsCBL.Name = "mroTaskItemsCBL";
            this.mroTaskItemsCBL.Size = new System.Drawing.Size(134, 23);
            this.mroTaskItemsCBL.TabIndex = 0;
            this.mroTaskItemsCBL.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged_TaskItemsCBL);
            // 
            // mroKeepCBTN
            // 
            this.mroKeepCBTN.Appearance = System.Windows.Forms.Appearance.Button;
            this.mroKeepCBTN.Location = new System.Drawing.Point(208, 86);
            this.mroKeepCBTN.Name = "mroKeepCBTN";
            this.mroKeepCBTN.Size = new System.Drawing.Size(100, 43);
            this.mroKeepCBTN.TabIndex = 3;
            this.mroKeepCBTN.Text = "Start Keeping";
            this.mroKeepCBTN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mroKeepCBTN.UseVisualStyleBackColor = true;
            this.mroKeepCBTN.CheckedChanged += new System.EventHandler(this.OnCheckedChanged_KeepBTN);
            // 
            // mroTotalTimeLBL
            // 
            this.mroTotalTimeLBL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mroTotalTimeLBL.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mroTotalTimeLBL.Location = new System.Drawing.Point(68, 109);
            this.mroTotalTimeLBL.Name = "mroTotalTimeLBL";
            this.mroTotalTimeLBL.Size = new System.Drawing.Size(134, 23);
            this.mroTotalTimeLBL.TabIndex = 4;
            this.mroTotalTimeLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mroTimer
            // 
            this.mroTimer.Interval = 500;
            this.mroTimer.Tick += new System.EventHandler(this.OnTick_Timer);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "작업 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "총 시간 :";
            // 
            // mroTodayLBL
            // 
            this.mroTodayLBL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mroTodayLBL.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mroTodayLBL.Location = new System.Drawing.Point(12, 9);
            this.mroTodayLBL.Name = "mroTodayLBL";
            this.mroTodayLBL.Size = new System.Drawing.Size(160, 23);
            this.mroTodayLBL.TabIndex = 7;
            this.mroTodayLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = ": 기준 날짜 (더블클릭)";
            // 
            // moCopyright2Lbl
            // 
            this.moCopyright2Lbl.AutoSize = true;
            this.moCopyright2Lbl.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.moCopyright2Lbl.Location = new System.Drawing.Point(12, 53);
            this.moCopyright2Lbl.Name = "moCopyright2Lbl";
            this.moCopyright2Lbl.Size = new System.Drawing.Size(306, 12);
            this.moCopyright2Lbl.TabIndex = 10;
            this.moCopyright2Lbl.Text = "  (ndd247blog.wordpress.com/2022/03/14/윈도-나의-작업-시간-관리하기)";
            // 
            // moCopyrightLbl
            // 
            this.moCopyrightLbl.AutoSize = true;
            this.moCopyrightLbl.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.moCopyrightLbl.Location = new System.Drawing.Point(12, 41);
            this.moCopyrightLbl.Name = "moCopyrightLbl";
            this.moCopyrightLbl.Size = new System.Drawing.Size(67, 12);
            this.moCopyrightLbl.TabIndex = 9;
            this.moCopyrightLbl.Text = "제작자: ndd247";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 141);
            this.Controls.Add(this.moCopyright2Lbl);
            this.Controls.Add(this.moCopyrightLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mroTodayLBL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mroTotalTimeLBL);
            this.Controls.Add(this.mroKeepCBTN);
            this.Controls.Add(this.mroTaskItemsCBL);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TimeKeeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox mroTaskItemsCBL;
        private System.Windows.Forms.CheckBox mroKeepCBTN;
        private System.Windows.Forms.Label mroTotalTimeLBL;
        private System.Windows.Forms.Timer mroTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label mroTodayLBL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label moCopyright2Lbl;
        private System.Windows.Forms.Label moCopyrightLbl;
    }
}

