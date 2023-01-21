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

namespace DownloadArchives
{
    public partial class MainForm : Form
    {
        private static string DEF_CONFIG_PATH = "./DownloadArchives.config.txt";

        private string msArchivesPath = null;

        private bool misArchivesChanged = false;
        private Dictionary<string, SortedSet<uint>> mroCurrArchives = null;

        private bool misErrorOccured = false;

        private void ReportResult(string asMsg)
        {
            mroResultOutputTBX.Text = asMsg;
        }

        private void PopUpFatalError(string asMsg)
        {
            misErrorOccured = true;
            mroFunctionSelectCBX.Enabled = false;
            mroCodeInputCBX.Enabled = false;
            mroNumberSetInputTBX.Enabled = false;

            MessageBox.Show("E: FATAL: " + asMsg);
        }

        private string AcquireArchivesPathFromConfigFile()
        {
            string sArchivesPathRet = null;

            StreamReader rSR = null;
            try
            {
                try { rSR = File.OpenText(DEF_CONFIG_PATH); }
                catch { throw new Exception("CONFIG 파일(" + DEF_CONFIG_PATH + ")을 열지 못했습니다.");  }

                string sLastTrimmedReadLine = null;
                while (rSR.Peek() >= 0)
                {
                    sLastTrimmedReadLine = rSR.ReadLine().Trim();
                    if ("" != sLastTrimmedReadLine)
                        break;
                }

                if (null == sLastTrimmedReadLine || "" == sLastTrimmedReadLine)
                    throw new Exception("CONFIG 파일(" + DEF_CONFIG_PATH + ")에는 유의미한 내용이 없습니다.");

                sArchivesPathRet = sLastTrimmedReadLine;
            }
            catch (Exception roE) { PopUpFatalError(roE.Message); }
            finally { rSR?.Close(); rSR = null; }

            return sArchivesPathRet;
        }

        private Dictionary<string, SortedSet<uint>> AcquireArchivesFromFile(string asArchivesPath)
        {
            Dictionary<string, SortedSet<uint>> roArchivesRet = new Dictionary<string, SortedSet<uint>>();

            StreamReader rSR = null;
            try
            {
                try { rSR = File.OpenText(asArchivesPath); }
                catch { throw new Exception("ARCHIVES 파일(" + asArchivesPath + ")을 열지 못했습니다."); }

                SortedSet<uint> roCurrNumberSet = null;
                while (rSR.Peek() >= 0)
                {
                    string sTrimmedLine = rSR.ReadLine().Trim();
                    if ("" == sTrimmedLine)
                        continue;

                    if (sTrimmedLine.StartsWith("@"))
                    {
                        string sCode = sTrimmedLine.Substring(1);
                        if ("" != sCode)
                        {
                            if (roArchivesRet.Keys.Contains(sCode))
                                throw new Exception("코드 섹션이 중복(" + sCode + ")됩니다.");

                            roCurrNumberSet = roArchivesRet[sCode] = new SortedSet<uint>();
                        }
                        else
                            throw new Exception("코드 섹션의 이름이 없습니다.");
                    }
                    else
                    {
                        string[] sNumberTerms = sTrimmedLine.Split(new char[] { ' ', '\t' });
                        foreach (string sCurrNumber in sNumberTerms)
                        {
                            uint uiNumber = 0;
                            try { uiNumber = uint.Parse(sCurrNumber); }
                            catch { throw new Exception("번호로 변환이 안되는 번호 데이터(" + sCurrNumber + ")가 있습니다."); }

                            if (null == roCurrNumberSet)
                                throw new Exception("코드 섹션 없이 번호 데이터가 먼저 나왔습니다.");

                            roCurrNumberSet.Add(uiNumber);
                        }
                    }
                }
            }
            catch (Exception roE) { PopUpFatalError(roE.Message); }
            finally { rSR?.Close(); rSR = null; }

            return roArchivesRet;
        }

        private string AcquireCodeFromUI()
        {
            return mroCodeInputCBX.Text.Trim();
        }

        private SortedSet<uint> AcquireNumberSetFromUI()
        {
            SortedSet<uint> roNumberSet = new SortedSet<uint>();

            string sTrimmedRawNumbers = mroNumberSetInputTBX.Text.Trim();
            string[] sNumberTerms = sTrimmedRawNumbers.Split(new char[] { ' ', '\t', ',' });

            foreach (string sCurrNumberTerm in sNumberTerms)
            {
                try { roNumberSet.Add(uint.Parse(sCurrNumberTerm)); }
                catch { /*IGNORE*/ }
            }

            return roNumberSet;
        }

        private void ShowCodesToUI()
        {
            mroCodeInputCBX.Items.Clear();
            foreach (string sCurrCode in mroCurrArchives.Keys)
                mroCodeInputCBX.Items.Add(sCurrCode);
        }

        private void ShowCurrArchivesToUI()
        {
            StringBuilder roSB = new StringBuilder();
            foreach (KeyValuePair<string, SortedSet<uint>> roCurrPair in mroCurrArchives)
            {
                roSB.AppendLine("@" + roCurrPair.Key);

                bool isFirstTime = true;
                foreach (uint uiCurrNumber in roCurrPair.Value)
                {
                    if (isFirstTime)
                        isFirstTime = false;
                    else
                        roSB.Append(" ");

                    roSB.Append(uiCurrNumber);
                }
                roSB.AppendLine();
                roSB.AppendLine();
            }

            mroCurrArchivesOutputTBX.Text = roSB.ToString();
        }

        private void FlushArchivesToFile(string asArchivesPath)
        {
            StreamWriter roSW = null;
            try
            {
                try { roSW = new StreamWriter(asArchivesPath); }
                catch { throw new Exception("FATAL: ARCHIVES 파일(\" + asArchivesPath + \")을 열지 못했습니다.");  }

                try
                {
                    foreach (KeyValuePair<string, SortedSet<uint>> roCurrPair in mroCurrArchives)
                    {
                        roSW.WriteLine("@" + roCurrPair.Key);

                        SortedSet<uint> roNumberSet = roCurrPair.Value;

                        bool isFirstTime = true;
                        foreach (uint uiNumber in roNumberSet)
                        {
                            if (isFirstTime)
                                isFirstTime = false;
                            else       
                                roSW.Write(" ");

                            roSW.Write(uiNumber);
                        }

                        roSW.WriteLine();
                        roSW.WriteLine();
                    }
                }
                catch { throw new Exception("FATAL: ARCHIVES 파일(\" + asArchivesPath + \")을 쓰는 도중에 예상치 못한 오류가 발생했습니다.");  }
            }
            catch (Exception roE) { PopUpFatalError(roE.Message); }
            finally { roSW?.Close(); roSW = null; }
        }

        private void AddToArchives(string asCode, SortedSet<uint> aroNumberSet)
        {
            bool isNeedToUpdateCodesToUI = false;
            bool isArchivesChanged = false;

            StringBuilder roSB = new StringBuilder();

            if (!mroCurrArchives.Keys.Contains(asCode))
            {
                mroCurrArchives[asCode] = aroNumberSet;
                isNeedToUpdateCodesToUI = true;
                isArchivesChanged = true;

                roSB.AppendLine("I: 코드 섹션 \'" + asCode + "\' 을/를 새로 만들었습니다.");
                roSB.Append("I: 코드 섹션 \'" + asCode + "\' 에 ");

                bool isFirstTime = true;
                foreach (uint uiNumber in aroNumberSet)
                {
                    if (isFirstTime)
                        isFirstTime = false;
                    else
                        roSB.Append(", ");

                    roSB.Append(uiNumber);
                }

                roSB.AppendLine(" 을/를 추가했습니다.");
            }
            else
            {
                SortedSet<uint> roCurrNumberSet = mroCurrArchives[asCode];

                roSB.Append("I: 코드 섹션 " + asCode + " 에 ");

                bool isFirstTime = true;
                foreach (uint uiNumber in aroNumberSet)
                {
                    if (isFirstTime)
                        isFirstTime = false;
                    else
                        roSB.Append(", ");

                    if (!roCurrNumberSet.Contains(uiNumber))
                    {
                        roCurrNumberSet.Add(uiNumber);
                        isArchivesChanged = true;
                        roSB.Append(uiNumber);
                    }
                    else
                        roSB.Append("(" + uiNumber + ")");
                }

                roSB.AppendLine(" 을/를 추가했습니다.");
            }

            ReportResult(roSB.ToString());

            if (isNeedToUpdateCodesToUI)
                ShowCodesToUI();

            if (isArchivesChanged)
            {
                misArchivesChanged = true;
                ShowCurrArchivesToUI();
            }
        }

        private void RemoveFromArchives(string asCode, SortedSet<uint> aroNumberSet)
        {
            bool isNeedToUpdateCodesToUI = false;
            bool isArchivesChanged = false;

            StringBuilder roSB = new StringBuilder();

            if (mroCurrArchives.Keys.Contains(asCode))
            {
                SortedSet<uint> roCurrNumberSet = mroCurrArchives[asCode];

                roSB.Append("I: 코드 섹션 \'" + asCode + "\' 에서 ");

                bool isFirstTime = true;
                foreach (uint uiNumber in aroNumberSet)
                {
                    if (isFirstTime)
                        isFirstTime = false;
                    else
                        roSB.Append(", ");

                    if (roCurrNumberSet.Contains(uiNumber))
                    {
                        roCurrNumberSet.Remove(uiNumber);
                        isArchivesChanged = true;
                        roSB.Append(uiNumber);
                    }
                    else
                        roSB.Append("(" + uiNumber + ")");
                }

                roSB.AppendLine(" 을/를 제거했습니다.");

                if (0 == roCurrNumberSet.Count)
                {
                    mroCurrArchives.Remove(asCode);
                    isNeedToUpdateCodesToUI = true;

                    roSB.AppendLine("I: 코드 섹션 \'" + asCode + "\' 을/를 제거했습니다.");
                }
            }
            else
                roSB.AppendLine("W: 코드 섹션 \'" + asCode +"\' 은/는 존재하지 않습니다. 따라서 아무런 처리도 이루어지지 않았습니다.");

            ReportResult(roSB.ToString());

            if (isNeedToUpdateCodesToUI)
                ShowCodesToUI();

            if (isArchivesChanged)
            {
                misArchivesChanged = true;
                ShowCurrArchivesToUI();
            }
        }

        private void ProcessFastLookup(string asCode, uint auiNumber)
        {
            if (mroCurrArchives.Keys.Contains(asCode))
            {
                if (mroCurrArchives[asCode].Contains(auiNumber))
                    mroFastLookupOutputLBL.Text = "O";
                else
                    mroFastLookupOutputLBL.Text = "X";
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeComponentMy();
        }

        void InitializeComponentMy()
        {
            this.FormClosing += OnFormClosing_MainForm;
            mroCodeInputCBX.TextChanged += OnTextChanged_CodeInputCBX;
            mroNumberSetInputTBX.TextChanged += OnTextChanged_NumberSetInputTBX;
            mroNumberSetInputTBX.KeyUp += OnKeyUp_NumberSetInputTBX;
        }

        private void OnLoad_MainForm(object aroS, EventArgs aroE)
        {
            if (null != (msArchivesPath = AcquireArchivesPathFromConfigFile()))
            {
                if (null != (mroCurrArchives = AcquireArchivesFromFile(msArchivesPath)))
                {
                    ShowCodesToUI();
                    ShowCurrArchivesToUI();
                }
            }
        }

        private void OnFormClosing_MainForm(object aroS, FormClosingEventArgs aroE)
        {
            if (!misErrorOccured)
            {
                if (misArchivesChanged)
                    FlushArchivesToFile(msArchivesPath);
            }
        }

        private void OnCheckedChanged_FunctionSelectCBX(object aroS, EventArgs aroE)
        {
            if (mroFunctionSelectCBX.Checked)
                mroFunctionSelectCBX.Text = "추가";
            else
                mroFunctionSelectCBX.Text = "제거";
        }

        private void OnTextChanged_CodeInputCBX(object aroS, EventArgs aroE)
        {
            string sCode = AcquireCodeFromUI();
            mroNumberSetInputTBX.Enabled = (sCode != "");
        }

        private void OnTextChanged_NumberSetInputTBX(object aroS, EventArgs aroE)
        {
            string sCode = AcquireCodeFromUI();
            SortedSet<uint> roNumbers = AcquireNumberSetFromUI();
            if (1 == roNumbers.Count)
            {
                ProcessFastLookup(sCode, roNumbers.ElementAt(0));
                return;
            }

            mroFastLookupOutputLBL.Text = "";
        }

        private void OnKeyUp_NumberSetInputTBX(object aroS, KeyEventArgs aroE)
        {
            if (aroE.KeyCode == Keys.Enter)
            {
                string sCode = AcquireCodeFromUI();
                SortedSet<uint> roNumbers = AcquireNumberSetFromUI();

                if (roNumbers.Count > 0)
                {
                    if (mroFunctionSelectCBX.Checked)
                        AddToArchives(sCode, roNumbers);
                    else
                        RemoveFromArchives(sCode, roNumbers);

                    mroNumberSetInputTBX.Text = "";
                }
            }
        }
    }
}
