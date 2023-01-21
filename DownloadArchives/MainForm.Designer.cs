namespace DownloadArchives
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
            this.mroCodeInputCBX = new System.Windows.Forms.ComboBox();
            this.mroNumberSetInputTBX = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mroFunctionSelectCBX = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mroFastLookupOutputLBL = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mroResultOutputTBX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mroCurrArchivesOutputTBX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mroCodeInputCBX
            // 
            this.mroCodeInputCBX.Location = new System.Drawing.Point(47, 48);
            this.mroCodeInputCBX.Name = "mroCodeInputCBX";
            this.mroCodeInputCBX.Size = new System.Drawing.Size(121, 23);
            this.mroCodeInputCBX.TabIndex = 4;
            // 
            // mroNumberSetInputTBX
            // 
            this.mroNumberSetInputTBX.Enabled = false;
            this.mroNumberSetInputTBX.Location = new System.Drawing.Point(218, 48);
            this.mroNumberSetInputTBX.Name = "mroNumberSetInputTBX";
            this.mroNumberSetInputTBX.Size = new System.Drawing.Size(236, 23);
            this.mroNumberSetInputTBX.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.mroFunctionSelectCBX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.mroFastLookupOutputLBL);
            this.groupBox1.Controls.Add(this.mroNumberSetInputTBX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mroCodeInputCBX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mroResultOutputTBX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "컨트롤";
            // 
            // mroFunctionSelectCBX
            // 
            this.mroFunctionSelectCBX.Appearance = System.Windows.Forms.Appearance.Button;
            this.mroFunctionSelectCBX.Checked = true;
            this.mroFunctionSelectCBX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mroFunctionSelectCBX.Location = new System.Drawing.Point(47, 19);
            this.mroFunctionSelectCBX.Name = "mroFunctionSelectCBX";
            this.mroFunctionSelectCBX.Size = new System.Drawing.Size(121, 23);
            this.mroFunctionSelectCBX.TabIndex = 1;
            this.mroFunctionSelectCBX.Text = "추가";
            this.mroFunctionSelectCBX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mroFunctionSelectCBX.UseVisualStyleBackColor = true;
            this.mroFunctionSelectCBX.CheckedChanged += new System.EventHandler(this.OnCheckedChanged_FunctionSelectCBX);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "기능 :";
            // 
            // mroFastLookupOutputLBL
            // 
            this.mroFastLookupOutputLBL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mroFastLookupOutputLBL.Location = new System.Drawing.Point(414, 19);
            this.mroFastLookupOutputLBL.Name = "mroFastLookupOutputLBL";
            this.mroFastLookupOutputLBL.Size = new System.Drawing.Size(40, 23);
            this.mroFastLookupOutputLBL.TabIndex = 2;
            this.mroFastLookupOutputLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "번호 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "코드 :";
            // 
            // mroResultOutputTBX
            // 
            this.mroResultOutputTBX.Location = new System.Drawing.Point(47, 77);
            this.mroResultOutputTBX.Multiline = true;
            this.mroResultOutputTBX.Name = "mroResultOutputTBX";
            this.mroResultOutputTBX.ReadOnly = true;
            this.mroResultOutputTBX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mroResultOutputTBX.Size = new System.Drawing.Size(407, 69);
            this.mroResultOutputTBX.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "결과 :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mroCurrArchivesOutputTBX);
            this.groupBox2.Location = new System.Drawing.Point(12, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 379);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "현재 데이터";
            // 
            // mroCurrArchivesOutputTBX
            // 
            this.mroCurrArchivesOutputTBX.Location = new System.Drawing.Point(6, 22);
            this.mroCurrArchivesOutputTBX.Multiline = true;
            this.mroCurrArchivesOutputTBX.Name = "mroCurrArchivesOutputTBX";
            this.mroCurrArchivesOutputTBX.ReadOnly = true;
            this.mroCurrArchivesOutputTBX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mroCurrArchivesOutputTBX.Size = new System.Drawing.Size(445, 351);
            this.mroCurrArchivesOutputTBX.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "빠른 룩업 :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "DownloadArchives";
            this.Load += new System.EventHandler(this.OnLoad_MainForm);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox mroCodeInputCBX;
        private System.Windows.Forms.TextBox mroNumberSetInputTBX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mroCurrArchivesOutputTBX;
        private System.Windows.Forms.Label mroFastLookupOutputLBL;
        private System.Windows.Forms.TextBox mroResultOutputTBX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox mroFunctionSelectCBX;
        private System.Windows.Forms.Label label5;
    }
}

