using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FixDatalog
{
    public partial class FormEdit : Form
    {
        public int ReturnLineNumber = 0;
        public bool RemovingLocation = false;

        private int Linenumber = 0;
        private int LineBefore = 0;
        private int LineAfter = 0;

        const int EM_LINESCROLL = 0x00B6;

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormEdit()
        {
            InitializeComponent();
        }

        public void SetInfos(int TLineNumber, string SearchHEX, string FoundHEX, string ParamFrom, int MatchBefore, int MatchAfter, string[] AllLines)
        {
            Linenumber = TLineNumber;
            LineBefore = MatchBefore;
            LineAfter = MatchAfter;
            lblLine.Text = (Linenumber + 1).ToString();
            lblSearchHEX.Text = SearchHEX.ToString();
            lblFoundHEX.Text = FoundHEX.ToString();
            lblParamFrom.Text = ParamFrom.ToString();

            txtManualLine.Text = (Linenumber + 1).ToString();

            //Set Found Matches
            if (MatchBefore != -99999) lblBefore.Text = (MatchBefore + 1).ToString();
            if (MatchAfter != -99999) lblAfter.Text = (MatchAfter + 1).ToString();

            //Set at -99999
            if (MatchBefore == -99999) lblBefore.Text = MatchBefore.ToString();
            if (MatchAfter == -99999) lblAfter.Text = MatchAfter.ToString();

            if (lblBefore.Text != "-99999") lblBefore.Text += "  (diff: " + (LineBefore - Linenumber) + ")";
            else
            {
                lblBefore.ForeColor = System.Drawing.Color.Red;
                button1.Enabled = false;
                button5.Enabled = false;
            }
            if (lblAfter.Text != "-99999") lblAfter.Text += "  (diff: " + (LineAfter - Linenumber) + ")";
            else
            {
                lblAfter.ForeColor = System.Drawing.Color.Red;
                button2.Enabled = false;
                button6.Enabled = false;
            }

            //Generate Viewer
            txtASM_Full.Text = "";
            for (int i = 0; i < AllLines.Length; i++)
            {
                if (i != Linenumber && i != LineBefore && i != LineAfter) txtASM_Full.AppendText((i + 1) + ". " + AllLines[i] + Environment.NewLine);
                else
                {
                    if (i == Linenumber) txtASM_Full.AppendText((i + 1) + ". " + AllLines[i] + "\t\t**CURRENT LINE**" + Environment.NewLine);
                    if (i == LineBefore) txtASM_Full.AppendText((i + 1) + ". " + AllLines[i] + "\t\t**LINE BEFORE**" + Environment.NewLine);
                    if (i == LineAfter) txtASM_Full.AppendText((i + 1) + ". " + AllLines[i] + "\t\t**LINE AFTER**" + Environment.NewLine);
                }
            }

            MoveCaretToLine(txtASM_Full, Linenumber);
        }

        private void MoveCaretToLine(TextBox txtBox, int lineNumber)
        {
            SetScrollPos(txtBox.Handle, 1, lineNumber - 1, true);
            SendMessage(txtBox.Handle, EM_LINESCROLL, 0, lineNumber - 1);
        }

        /*private void MoveCaretToLine(TextBox txtBox, int lineNumber)
        {
            txtBox.HideSelection = false;
            int position = 0;
            if (lineNumber < txtBox.Lines.Length)
            {
                for (int i = 0; i < lineNumber; i++) position += txtBox.Lines[i].Length;
            }

            txtBox.SelectionStart = position;
            //txtBox.SelectionLength = txtBox.Lines[lineNumber].Length;
            txtBox.ScrollToCaret();
        }*/

        private void Button1_Click(object sender, EventArgs e)
        {
            ReturnLineNumber = LineBefore;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ReturnLineNumber = LineAfter;
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnLineNumber = int.Parse(txtManualLine.Text) - 1;
                this.Close();
            }
            catch { }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            RemovingLocation = true;
            this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MoveCaretToLine(txtASM_Full, LineBefore);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MoveCaretToLine(txtASM_Full, LineAfter);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            MoveCaretToLine(txtASM_Full, Linenumber);
        }
    }
}
