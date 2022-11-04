namespace FixDatalog
{
    partial class FormEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblSearchHEX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFoundHEX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblParamFrom = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBefore = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtManualLine = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.txtASM_Full = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ERROR FINDING COMMAND";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line:";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(107, 9);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(35, 13);
            this.lblLine.TabIndex = 2;
            this.lblLine.Text = "label3";
            // 
            // lblSearchHEX
            // 
            this.lblSearchHEX.AutoSize = true;
            this.lblSearchHEX.Location = new System.Drawing.Point(107, 32);
            this.lblSearchHEX.Name = "lblSearchHEX";
            this.lblSearchHEX.Size = new System.Drawing.Size(35, 13);
            this.lblSearchHEX.TabIndex = 4;
            this.lblSearchHEX.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Search for HEX:";
            // 
            // lblFoundHEX
            // 
            this.lblFoundHEX.AutoSize = true;
            this.lblFoundHEX.Location = new System.Drawing.Point(324, 32);
            this.lblFoundHEX.Name = "lblFoundHEX";
            this.lblFoundHEX.Size = new System.Drawing.Size(35, 13);
            this.lblFoundHEX.TabIndex = 6;
            this.lblFoundHEX.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "HEX Found:";
            // 
            // lblParamFrom
            // 
            this.lblParamFrom.AutoSize = true;
            this.lblParamFrom.Location = new System.Drawing.Point(324, 9);
            this.lblParamFrom.Name = "lblParamFrom";
            this.lblParamFrom.Size = new System.Drawing.Size(35, 13);
            this.lblParamFrom.TabIndex = 8;
            this.lblParamFrom.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Parameter From:";
            // 
            // lblBefore
            // 
            this.lblBefore.AutoSize = true;
            this.lblBefore.Location = new System.Drawing.Point(186, 9);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(41, 13);
            this.lblBefore.TabIndex = 10;
            this.lblBefore.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Matching HEX before at Line:";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.Location = new System.Drawing.Point(186, 31);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(41, 13);
            this.lblAfter.TabIndex = 12;
            this.lblAfter.Text = "label12";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Matching HEX after at Line:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "< Set before location";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(605, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Set after location >";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 112);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Set Manually Line Number:";
            // 
            // txtManualLine
            // 
            this.txtManualLine.Location = new System.Drawing.Point(178, 109);
            this.txtManualLine.Name = "txtManualLine";
            this.txtManualLine.Size = new System.Drawing.Size(87, 20);
            this.txtManualLine.TabIndex = 16;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(271, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Set manual location";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblLine);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblSearchHEX);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblFoundHEX);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblParamFrom);
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 58);
            this.panel1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.lblBefore);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.lblAfter);
            this.panel2.Location = new System.Drawing.Point(534, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 58);
            this.panel2.TabIndex = 19;
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(761, 106);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Remove Location";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // txtASM_Full
            // 
            this.txtASM_Full.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtASM_Full.Location = new System.Drawing.Point(12, 192);
            this.txtASM_Full.Multiline = true;
            this.txtASM_Full.Name = "txtASM_Full";
            this.txtASM_Full.ReadOnly = true;
            this.txtASM_Full.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtASM_Full.Size = new System.Drawing.Size(868, 314);
            this.txtASM_Full.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(369, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "ASM VIEWER";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button5.Location = new System.Drawing.Point(211, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "< Check before location";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button6.Location = new System.Drawing.Point(501, 8);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(150, 23);
            this.button6.TabIndex = 27;
            this.button6.Text = "Check after location >";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button7.Location = new System.Drawing.Point(3, 8);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(128, 23);
            this.button7.TabIndex = 28;
            this.button7.Text = "Check at Line Number";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Location = new System.Drawing.Point(12, 155);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(868, 36);
            this.panel3.TabIndex = 29;
            // 
            // FormEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 518);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.txtASM_Full);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtManualLine);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FormEdit";
            this.ShowIcon = false;
            this.Text = "Error";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblSearchHEX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFoundHEX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblParamFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAfter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtManualLine;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtASM_Full;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel3;
    }
}