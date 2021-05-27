
namespace VolumeLock
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
            this.mroDeviceNameLbl = new System.Windows.Forms.Label();
            this.mroTargetVolumeLbl = new System.Windows.Forms.Label();
            this.mroDeviceNameTbx = new System.Windows.Forms.TextBox();
            this.mroTargetVolumeTbx = new System.Windows.Forms.TextBox();
            this.mroDoLockCbx = new System.Windows.Forms.CheckBox();
            this.moTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mroDeviceNameLbl
            // 
            this.mroDeviceNameLbl.AutoSize = true;
            this.mroDeviceNameLbl.Location = new System.Drawing.Point(12, 15);
            this.mroDeviceNameLbl.Name = "mroDeviceNameLbl";
            this.mroDeviceNameLbl.Size = new System.Drawing.Size(122, 15);
            this.mroDeviceNameLbl.TabIndex = 0;
            this.mroDeviceNameLbl.Text = "상태 혹은 장치 이름 :";
            // 
            // mroTargetVolumeLbl
            // 
            this.mroTargetVolumeLbl.AutoSize = true;
            this.mroTargetVolumeLbl.Location = new System.Drawing.Point(12, 44);
            this.mroTargetVolumeLbl.Name = "mroTargetVolumeLbl";
            this.mroTargetVolumeLbl.Size = new System.Drawing.Size(108, 15);
            this.mroTargetVolumeLbl.TabIndex = 1;
            this.mroTargetVolumeLbl.Text = "목표 볼륨 (0 ~ 1) :";
            // 
            // mroDeviceNameTbx
            // 
            this.mroDeviceNameTbx.Location = new System.Drawing.Point(142, 12);
            this.mroDeviceNameTbx.Name = "mroDeviceNameTbx";
            this.mroDeviceNameTbx.ReadOnly = true;
            this.mroDeviceNameTbx.Size = new System.Drawing.Size(170, 23);
            this.mroDeviceNameTbx.TabIndex = 2;
            // 
            // mroTargetVolumeTbx
            // 
            this.mroTargetVolumeTbx.Location = new System.Drawing.Point(142, 41);
            this.mroTargetVolumeTbx.Name = "mroTargetVolumeTbx";
            this.mroTargetVolumeTbx.Size = new System.Drawing.Size(64, 23);
            this.mroTargetVolumeTbx.TabIndex = 3;
            this.mroTargetVolumeTbx.Text = "0.5";
            // 
            // mroDoLockCbx
            // 
            this.mroDoLockCbx.Appearance = System.Windows.Forms.Appearance.Button;
            this.mroDoLockCbx.AutoCheck = false;
            this.mroDoLockCbx.Location = new System.Drawing.Point(212, 41);
            this.mroDoLockCbx.Name = "mroDoLockCbx";
            this.mroDoLockCbx.Size = new System.Drawing.Size(100, 48);
            this.mroDoLockCbx.TabIndex = 5;
            this.mroDoLockCbx.Text = "Lock";
            this.mroDoLockCbx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mroDoLockCbx.UseVisualStyleBackColor = true;
            this.mroDoLockCbx.CheckedChanged += new System.EventHandler(this.OnCheckedChanged_LockCbx);
            // 
            // moTimer
            // 
            this.moTimer.Tick += new System.EventHandler(this.OnTick_Timer);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 101);
            this.Controls.Add(this.mroDoLockCbx);
            this.Controls.Add(this.mroTargetVolumeTbx);
            this.Controls.Add(this.mroDeviceNameTbx);
            this.Controls.Add(this.mroTargetVolumeLbl);
            this.Controls.Add(this.mroDeviceNameLbl);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VolumeLock";
            this.Load += new System.EventHandler(this.OnLoad_MainForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mroDeviceNameLbl;
        private System.Windows.Forms.Label mroTargetVolumeLbl;
        private System.Windows.Forms.TextBox mroDeviceNameTbx;
        private System.Windows.Forms.TextBox mroTargetVolumeTbx;
        private System.Windows.Forms.CheckBox mroDoLockCbx;
        private System.Windows.Forms.Timer moTimer;
    }
}

