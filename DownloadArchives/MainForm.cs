﻿using System;
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

        private List<string> mroArchivePathList = null;

        private Dictionary<string, SortedSet<uint>> mroCurrArchives = null;

        private bool misArchivesModified = false;
        private void SetAsArchivesModified()
        {
            if (!misArchivesModified)
                this.Text += " (변경됨)";

            misArchivesModified = true;
        }

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

        private List<string> AcquireArchivesPathListFromConfigFile()
        {
            List<string> roArchivesPathListRet = new List<string>();

            StreamReader rSR = null;
            try
            {
                try { rSR = File.OpenText(DEF_CONFIG_PATH); }
                catch { throw new Exception("CONFIG 파일(" + DEF_CONFIG_PATH + ")을 열지 못했습니다."); }

                string sLastTrimmedReadLine = null;
                while (rSR.Peek() >= 0)
                {
                    sLastTrimmedReadLine = rSR.ReadLine().Trim();
                    if ("" == sLastTrimmedReadLine)
                        break;

                    roArchivesPathListRet.Add(sLastTrimmedReadLine);
                }

                if (0 == roArchivesPathListRet.Count)
                    throw new Exception("CONFIG 파일(" + DEF_CONFIG_PATH + ")에는 유의미한 내용이 없습니다.");
            }
            catch (Exception roE) { PopUpFatalError(roE.Message); }
            finally { rSR?.Close(); rSR = null; }

            return roArchivesPathListRet;
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

        private bool AcquireNumberForFastLookupFromUI(out uint uiNumber)
        {
            uiNumber = 0;

            string sTrimmedRawNumbers = mroNumberSetInputTBX.Text.Trim();
            string[] sNumberTerms = sTrimmedRawNumbers.Split(new char[] { ' ', '\t', ',' });

            if (1 != sNumberTerms.Length)
                return false;

            try { uiNumber = uint.Parse(sNumberTerms[0]); }
            catch { return false; }

            return true;
        }

        private SortedSet<uint> AcquireNumberSetFromUI(StringBuilder aroWarningSB = null)
        {
            SortedSet<uint> roNumberSetRet = new SortedSet<uint>();

            string sTrimmedRawNumbers = mroNumberSetInputTBX.Text.Trim();
            string[] sNumberTerms = sTrimmedRawNumbers.Split(new char[] { ' ', '\t', ',' });

            foreach (string sCurrNumberTerm in sNumberTerms)
            {
                if (sCurrNumberTerm.Contains("-") || sCurrNumberTerm.Contains("~"))
                {
                    string[] sNumberRangeTerms = sCurrNumberTerm.Split(new char[] { '-', '~' });
                    if (2 != sNumberRangeTerms.Length)
                    {
                        aroWarningSB?.AppendLine("W: 범위 형식이 잘못된 데이터가 있습니다. (" + sCurrNumberTerm + ")");
                        continue;
                    }

                    try
                    {
                        uint uiNumberBeg = uint.Parse(sNumberRangeTerms[0]);
                        uint uiNumberEnd = uint.Parse(sNumberRangeTerms[1]);
                        if (uiNumberBeg < uiNumberEnd)
                        {
                            for (uint n = uiNumberBeg; n <= uiNumberEnd; ++n)
                                roNumberSetRet.Add(n);
                        }
                        else
                            aroWarningSB?.AppendLine("W: 범위의 끝값이 시작값보다 작습니다. (" + sCurrNumberTerm + ")");
                    }
                    catch { aroWarningSB?.AppendLine("W: 숫자로 변환할 수 없는 데이터가 있습니다. (" + sCurrNumberTerm + ")"); }
                }
                else
                {
                    try { roNumberSetRet.Add(uint.Parse(sCurrNumberTerm)); }
                    catch { aroWarningSB?.AppendLine("W: 숫자로 변환할 수 없는 데이터가 있습니다. (" + sCurrNumberTerm + ")"); }
                }
            }

            return roNumberSetRet;
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
                string sCurrCode = roCurrPair.Key;
                SortedSet<uint> roCurrNumberSet = roCurrPair.Value;

                roSB.AppendLine("@" + sCurrCode);

                bool isFirstTime = true;
                bool isNumberRangeSet = false;
                uint uiNumberBeg = 0, uiNumberEnd = 0;
                foreach (uint uiCurrNumber in roCurrNumberSet)
                {
                    if (!isNumberRangeSet)
                    {
                        uiNumberEnd = uiNumberBeg = uiCurrNumber;
                        isNumberRangeSet = true;
                    }
                    else
                    {
                        if (uiNumberEnd + 1 == uiCurrNumber)
                            uiNumberEnd = uiCurrNumber;
                        else
                        {
                            if (isFirstTime)
                                isFirstTime = false;
                            else
                                roSB.Append(" ");

                            if (uiNumberBeg == uiNumberEnd)
                                roSB.Append(uiNumberBeg);
                            else if (uiNumberBeg + 1 == uiNumberEnd)
                                roSB.Append(uiNumberBeg + " " + uiNumberEnd);
                            else
                                roSB.Append(uiNumberBeg + "-" + uiNumberEnd);

                            uiNumberEnd = uiNumberBeg = uiCurrNumber;
                        }
                    }
                }

                if (isNumberRangeSet)
                {
                    if (!isFirstTime)
                        roSB.Append(" ");

                    if (uiNumberBeg == uiNumberEnd)
                        roSB.Append(uiNumberBeg);
                    else if (uiNumberBeg + 1 == uiNumberEnd)
                        roSB.Append(uiNumberBeg + " " + uiNumberEnd);
                    else
                        roSB.Append(uiNumberBeg + "-" + uiNumberEnd);
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
                SetAsArchivesModified();
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
                SetAsArchivesModified();
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
            if (null != (mroArchivePathList = AcquireArchivesPathListFromConfigFile()) && 0 < mroArchivePathList.Count)
            {
                if (null != (mroCurrArchives = AcquireArchivesFromFile(mroArchivePathList[0])))
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
                if (misArchivesModified)
                {
                    foreach (string sArchivesPath in mroArchivePathList)
                        FlushArchivesToFile(sArchivesPath);
                }
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
            uint uiNumber;
            if (AcquireNumberForFastLookupFromUI(out uiNumber))
                ProcessFastLookup(sCode, uiNumber);
            else
                mroFastLookupOutputLBL.Text = "";
        }

        private void OnKeyUp_NumberSetInputTBX(object aroS, KeyEventArgs aroE)
        {
            if (aroE.KeyCode == Keys.Enter)
            {
                StringBuilder roWarningSB = new StringBuilder();

                string sCode = AcquireCodeFromUI();
                SortedSet<uint> roNumbers = AcquireNumberSetFromUI(roWarningSB);
                if (roWarningSB.Length > 0)
                {
                    if (DialogResult.Cancel == MessageBox.Show(roWarningSB.ToString(), "입력 데이터에 이싱한 점이 있습니다. 그래도 계속 진행하시겠습니까?", MessageBoxButtons.OKCancel))
                        return;
                }

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
