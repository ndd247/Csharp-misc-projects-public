
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
            this.moDeviceNameLbl = new System.Windows.Forms.Label();
            this.moTargetVolumeLbl = new System.Windows.Forms.Label();
            this.moDeviceNameTbx = new System.Windows.Forms.TextBox();
            this.moDoLockCbx = new System.Windows.Forms.CheckBox();
            this.moTimer = new System.Windows.Forms.Timer(this.components);
            this.moTargetVolumeNud = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.moTargetVolumeNud)).BeginInit();
            this.SuspendLayout();
            // 
            // moDeviceNameLbl
            // 
            this.moDeviceNameLbl.AutoSize = true;
            this.moDeviceNameLbl.Location = new System.Drawing.Point(12, 15);
            this.moDeviceNameLbl.Name = "moDeviceNameLbl";
            this.moDeviceNameLbl.Size = new System.Drawing.Size(122, 15);
            this.moDeviceNameLbl.TabIndex = 0;
            this.moDeviceNameLbl.Text = "상태 혹은 장치 이름 :";
            // 
            // moTargetVolumeLbl
            // 
            this.moTargetVolumeLbl.AutoSize = true;
            this.moTargetVolumeLbl.Location = new System.Drawing.Point(12, 44);
            this.moTargetVolumeLbl.Name = "moTargetVolumeLbl";
            this.moTargetVolumeLbl.Size = new System.Drawing.Size(122, 15);
            this.moTargetVolumeLbl.TabIndex = 2;
            this.moTargetVolumeLbl.Text = "목표 볼륨 (0 ~ 100) :";
            // 
            // moDeviceNameTbx
            // 
            this.moDeviceNameTbx.Location = new System.Drawing.Point(142, 12);
            this.moDeviceNameTbx.Name = "moDeviceNameTbx";
            this.moDeviceNameTbx.ReadOnly = true;
            this.moDeviceNameTbx.Size = new System.Drawing.Size(170, 23);
            this.moDeviceNameTbx.TabIndex = 1;
            // 
            // moDoLockCbx
            // 
            this.moDoLockCbx.Appearance = System.Windows.Forms.Appearance.Button;
            this.moDoLockCbx.Location = new System.Drawing.Point(212, 41);
            this.moDoLockCbx.Name = "moDoLockCbx";
            this.moDoLockCbx.Size = new System.Drawing.Size(100, 48);
            this.moDoLockCbx.TabIndex = 4;
            this.moDoLockCbx.Text = "Lock";
            this.moDoLockCbx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.moDoLockCbx.UseVisualStyleBackColor = true;
            this.moDoLockCbx.CheckedChanged += new System.EventHandler(this.OnCheckedChanged_LockCbx);
            // 
            // moTimer
            // 
            this.moTimer.Tick += new System.EventHandler(this.OnTick_Timer);
            // 
            // moTargetVolumeNud
            // 
            this.moTargetVolumeNud.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.moTargetVolumeNud.Location = new System.Drawing.Point(142, 41);
            this.moTargetVolumeNud.Name = "moTargetVolumeNud";
            this.moTargetVolumeNud.Size = new System.Drawing.Size(64, 23);
            this.moTargetVolumeNud.TabIndex = 3;
            this.moTargetVolumeNud.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 101);
            this.Controls.Add(this.moTargetVolumeNud);
            this.Controls.Add(this.moDoLockCbx);
            this.Controls.Add(this.moDeviceNameTbx);
            this.Controls.Add(this.moTargetVolumeLbl);
            this.Controls.Add(this.moDeviceNameLbl);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VolumeLock";
            this.Load += new System.EventHandler(this.OnLoad_MainForm);
            ((System.ComponentModel.ISupportInitialize)(this.moTargetVolumeNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label moDeviceNameLbl;
        private System.Windows.Forms.Label moTargetVolumeLbl;
        private System.Windows.Forms.TextBox moDeviceNameTbx;
        private System.Windows.Forms.CheckBox moDoLockCbx;
        private System.Windows.Forms.Timer moTimer;
        private System.Windows.Forms.NumericUpDown moTargetVolumeNud;
    }
}

