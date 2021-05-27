using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolumeLock
{
    public partial class MainForm : Form
    {
        IAudioEndpointVolume moTargetSpeakerVolume = null;
        float mfTargetVolume;

        public MainForm()
        {
            InitializeComponent();
            InitializeComponentExt();
        }

        private void InitializeComponentExt()
        {
            mroDoLockCbx.Click += OnClick_DoLockCbx;
        }

        private void OnClick_DoLockCbx(object aoS, EventArgs aoE)
        {
            if (!mroDoLockCbx.Checked)
            {
                try { mfTargetVolume = float.Parse(mroTargetVolumeTbx.Text); }
                catch { mfTargetVolume = -0.1f; }
                finally { }

                if (!(0f <= mfTargetVolume && mfTargetVolume <= 1f))
                {
                    mroDeviceNameTbx.Text = "(PLZ USE VALID VALUE)";
                    return;
                }
            }

            mroDoLockCbx.Checked = !mroDoLockCbx.Checked;
        }

        private void OnLoad_MainForm(object aoS, EventArgs aoE)
        {
            mroDeviceNameTbx.Text = "(READY)";
        }

        private void OnCheckedChanged_LockCbx(object aoS, EventArgs aoE)
        {
            if (mroDoLockCbx.Checked)
            {
                try
                {
                    IMMDeviceEnumerator oDeviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();

                    IMMDevice oTargetSpeaker = null;
                    oDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Reader, Role.Multimedia, out oTargetSpeaker);

                    IPropertyStore roPropertyStore = null;
                    oTargetSpeaker.OpenPropertyStore(StorageAccessMode.Read, out roPropertyStore);

                    //PropertyKey roKey = new PropertyKey(new Guid(0x026e516e, 0xb814, 0x414b, 0x83, 0xcd, 0x85, 0x6d, 0x6f, 0xef, 0x48, 0x22), 2);
                    PropertyKey roPropKey = new PropertyKey(new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0), 14);
                    PropertyVariant roPropVar;
                    roPropertyStore.GetValue(ref roPropKey, out roPropVar);

                    mroDeviceNameTbx.Text = (string)roPropVar.Value;

                    Guid uiid = typeof(IAudioEndpointVolume).GUID;
                    object roTemp = null;
                    oTargetSpeaker.Activate(ref uiid, 0, IntPtr.Zero, out roTemp);
                    moTargetSpeakerVolume = (IAudioEndpointVolume)roTemp;

                    mroTargetVolumeTbx.ReadOnly = true;
                    mroDoLockCbx.Text = "Unlock";
                    moTimer.Enabled = true;
                }
                catch
                {
                    mroDeviceNameTbx.Text = "(ERROR)";
                    mroTargetVolumeTbx.Enabled = false;
                    mroDoLockCbx.Enabled = false;
                }
                finally
                {
                }
            }
            else
            {
                mroDeviceNameTbx.Text = "(READY)";
                mroTargetVolumeTbx.ReadOnly = false;
                mroDoLockCbx.Text = "Lock";
                moTimer.Enabled = false;
            }
        }

        private void OnTick_Timer(object sender, EventArgs e)
        {
            moTargetSpeakerVolume.SetMasterVolumeLevelScalar(mfTargetVolume, new Guid());
        }
    }
}
