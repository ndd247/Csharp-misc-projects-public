using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeKeeper
{
    public partial class MainForm : Form
    {
        public static string TodayToString(DateTime adsToday)
        {
            return adsToday.Year + " 년 " + adsToday.Month.ToString("D2") + " 월 " + adsToday.Day.ToString("D2") + " 일";
        }

        public static string TodayToLogFilePath(DateTime adsToday)
        {
            return "./logs/" + adsToday.Year + "-" + adsToday.Month.ToString("D2") + "-" + adsToday.Day.ToString("D2") + ".log.txt";
        }

        public static string TotalTimeToString(TimeSpan adsTotalTime)
        {
            if (0 == adsTotalTime.Hours)
            {
                if (0 == adsTotalTime.Minutes)
                    return adsTotalTime.Seconds + " 초";
                else
                    return adsTotalTime.Minutes + " 분 " + adsTotalTime.Seconds + " 초";
            }
            else
                return adsTotalTime.Hours + " 시간 " + adsTotalTime.Minutes + " 분";
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeComponentEx();
        }

        class TaskNameEx : Tuple<string, uint> // Combobox List 에 작업 이름과 함께 ID 를 저장하기 위한 자료구조
        {
            public TaskNameEx(string arsName, uint auiID) : base(arsName, auiID)
            {
            }

            public string Name
            {
                get { return this.Item1; }
            }

            public uint ID
            {
                get { return this.Item2; }
            }

            override public string ToString()
            {
                return this.Item1;
            }
        }

        const string CONFIG_FILE_PATH = "./TimeKeeper.config.txt";
        const uint DEFAULT_TASK_ID = 0;
        const string DEFAULT_TASK_NAME = "(기본 작업)";

        DateTime mdsToday = new DateTime();
        Dictionary<uint, TimeSpan> mroTaskTimeStatus = new Dictionary<uint, TimeSpan>();

        uint muiCurrTaskID;
        TimeSpan mdsCurrTaskTotalTime;
        DateTime mdsCurrTaskLastTick;

        private void InitializeTaskTimeStatus() // Combobox List 와 기준 날짜 로그 파일으로부터 작업 시간 데이터를 생성한다.
        {
            mroTaskTimeStatus = new Dictionary<uint, TimeSpan>();

            // STEP-0: Combobox List 에서 작업 데이터를 추가
            {
                foreach (object roItem in mroTaskItemsCBL.Items)
                {
                    TaskNameEx roTaskNameEx = (TaskNameEx)roItem;
                    mroTaskTimeStatus.Add(roTaskNameEx.ID, new TimeSpan());
                }
            }

            // STEP-1: 기준 날짜 로그 파일에서 작업 데이터를 추가
            {
                StreamReader roSR = null;
                try
                {
                    roSR = File.OpenText(TodayToLogFilePath(mdsToday));
                    while (true)
                    {
                        string rsLine = roSR.ReadLine();
                        if (null == rsLine)
                            break;

                        string rsTrimmedLine = rsLine.Trim();
                        string[] rsTerms = rsLine.Split(new char[] { ' ', '\t' }, 2);
                        if (rsTerms.Length < 2)
                            continue;

                        uint uiTaskID = 0;
                        try { uiTaskID = uint.Parse(rsTerms[0]); }
                        catch { }
                        finally { }
                        if (0 == uiTaskID)
                            continue;

                        TimeSpan dsTotalTime = new TimeSpan();
                        bool isTotalTimeParsingOK = false;
                        try
                        {
                            dsTotalTime = TimeSpan.Parse(rsTerms[1]);
                            isTotalTimeParsingOK = true;
                        }
                        catch { }
                        finally { }
                        if (!isTotalTimeParsingOK)
                            continue;

                        mroTaskTimeStatus[uiTaskID] = dsTotalTime;
                    }
                }
                catch { }
                finally
                {
                    if (null != roSR)
                        roSR.Close();
                }
            }
        }

        private void InitializeComponentEx()
        {
            mroTodayLBL.DoubleClick += OnDoubleClick_TodayLBL;

            mdsToday = DateTime.Now;
            mroTodayLBL.Text = TodayToString(mdsToday);

            // STEP 0: 작업 설정 파일 읽어오기
            {
                StreamReader roSR = null;
                try
                {
                    roSR = File.OpenText(CONFIG_FILE_PATH);
                    while (true)
                    {
                        string rsLine = roSR.ReadLine();
                        if (null == rsLine)
                            break;

                        string rsTrimmedLine = rsLine.Trim();
                        string[] rsTerms = rsLine.Split(new char[] { ' ', '\t' }, 2);
                        if (rsTerms.Length < 2)
                            continue;

                        uint uiTaskID = 0;
                        try { uiTaskID = uint.Parse(rsTerms[0]); }
                        catch { }
                        finally { }
                        if (0 == uiTaskID)
                            continue;

                        mroTaskItemsCBL.Items.Add(new TaskNameEx(rsTerms[1], uiTaskID));
                    }
                }
                catch { }
                finally
                {
                    if (null != roSR)
                        roSR.Close();
                }

                if (0 == mroTaskItemsCBL.Items.Count)
                    mroTaskItemsCBL.Items.Add(new TaskNameEx(DEFAULT_TASK_NAME, DEFAULT_TASK_ID));
            }

            // STEP 1: 작업 시간 데이터 설정하기
            InitializeTaskTimeStatus();

            mroTaskItemsCBL.SelectedIndex = 0;
        }

        private void OnDoubleClick_TodayLBL(object aroS, EventArgs aroE)
        {
            if (!mroKeepCBTN.Checked)
            {
                CalendarForm roCF = new CalendarForm();
                if (DialogResult.OK == roCF.ShowDialog())
                {
                    mdsToday = roCF.mdsNewToday;
                    mroTodayLBL.Text = TodayToString(mdsToday);

                    InitializeTaskTimeStatus();

                    mroTaskItemsCBL.SelectedIndex = -1;
                    mroTaskItemsCBL.SelectedIndex = 0;
                    //mroTaskItemsCBL.SelectedIndexChanged
                }
            }
        }

        private void OnCheckedChanged_KeepBTN(object aroS, EventArgs aroE)
        {
            if (mroKeepCBTN.Checked)
            {
                mroTaskItemsCBL.Enabled = false;
                mroKeepCBTN.Text = "Stop Keeping";
                mroTimer.Enabled = true;

                muiCurrTaskID = ((TaskNameEx)mroTaskItemsCBL.SelectedItem).ID;
                mdsCurrTaskTotalTime = mroTaskTimeStatus[muiCurrTaskID];
                mdsCurrTaskLastTick = DateTime.Now;
            }
            else
            {
                mroTaskItemsCBL.Enabled = true;
                mroKeepCBTN.Text = "Start Keeping";
                mroTimer.Enabled = false;

                // STEP 0: 현재 작업에 대해 누적 시간 업데이트
                mroTaskTimeStatus[muiCurrTaskID] += DateTime.Now - mdsCurrTaskLastTick;

                // STEP 1: 현재 작업 시간 로그 덮어쓰기
                {
                    StreamWriter roSW = null;
                    try
                    {
                        roSW = new StreamWriter(TodayToLogFilePath(mdsToday)); // 기준 날짜로 저장

                        foreach (KeyValuePair<uint, TimeSpan> roPair in mroTaskTimeStatus)
                            roSW.WriteLine(roPair.Key + " " + roPair.Value.ToString());
                    }
                    catch (Exception roE) { MessageBox.Show(roE.ToString()); }
                    finally
                    {
                        if (null != roSW)
                            roSW.Close();
                    }
                }
            }
        }

        private void OnTick_Timer(object aroS, EventArgs aroE)
        {
            if (mroKeepCBTN.Checked) // unnecessary check, but for safety
            {
                // 현재 작업에 대해 누적 시간 업데이트
                mroTotalTimeLBL.Text = TotalTimeToString(mdsCurrTaskTotalTime + (DateTime.Now - mdsCurrTaskLastTick));
            }
        }

        private void OnSelectedIndexChanged_TaskItemsCBL(object aroS, EventArgs aroE)
        {
            if (-1 == mroTaskItemsCBL.SelectedIndex)
                return;

            mroTotalTimeLBL.Text = TotalTimeToString(mroTaskTimeStatus[((TaskNameEx)mroTaskItemsCBL.SelectedItem).ID]);
        }
    }
}
