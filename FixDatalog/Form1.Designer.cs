namespace FixDatalog
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chk_Reloc = new System.Windows.Forms.CheckBox();
            this.chk_FixDTL = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLastTable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_GenerateDTInfos = new System.Windows.Forms.CheckBox();
            this.txtAutoCopyVersion = new System.Windows.Forms.TextBox();
            this.chk_AutoCopy = new System.Windows.Forms.CheckBox();
            this.chk_OverLong = new System.Windows.Forms.CheckBox();
            this.chk_Quick = new System.Windows.Forms.CheckBox();
            this.chk_Generate = new System.Windows.Forms.CheckBox();
            this.chk_RelocUnused = new System.Windows.Forms.CheckBox();
            this.chk_Array = new System.Windows.Forms.CheckBox();
            this.chk_Word = new System.Windows.Forms.CheckBox();
            this.chk_SingleByte = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_BMTuneLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chk_Quit = new System.Windows.Forms.CheckBox();
            this.chk_FreeSpace = new System.Windows.Forms.CheckBox();
            this.chk_ASM = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Logs = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.chk_Do111 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Decrypt DP Jump";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(319, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(39, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(451, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Binary|*.bin|Assembly|*.asm";
            this.openFileDialog1.Title = "Open file";
            // 
            // chk_Reloc
            // 
            this.chk_Reloc.AutoSize = true;
            this.chk_Reloc.Checked = true;
            this.chk_Reloc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Reloc.Location = new System.Drawing.Point(10, 58);
            this.chk_Reloc.Name = "chk_Reloc";
            this.chk_Reloc.Size = new System.Drawing.Size(179, 17);
            this.chk_Reloc.TabIndex = 3;
            this.chk_Reloc.Text = "Parameters Relocation(V1.14++)";
            this.chk_Reloc.UseVisualStyleBackColor = true;
            // 
            // chk_FixDTL
            // 
            this.chk_FixDTL.AutoSize = true;
            this.chk_FixDTL.Checked = true;
            this.chk_FixDTL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_FixDTL.Location = new System.Drawing.Point(10, 26);
            this.chk_FixDTL.Name = "chk_FixDTL";
            this.chk_FixDTL.Size = new System.Drawing.Size(129, 17);
            this.chk_FixDTL.TabIndex = 4;
            this.chk_FixDTL.Text = "Fix Datalog (BMTune)";
            this.chk_FixDTL.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Last Table At:";
            // 
            // txtLastTable
            // 
            this.txtLastTable.Location = new System.Drawing.Point(83, 5);
            this.txtLastTable.Name = "txtLastTable";
            this.txtLastTable.Size = new System.Drawing.Size(62, 20);
            this.txtLastTable.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "File:";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 467);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(825, 10);
            this.progressBar1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.chk_GenerateDTInfos);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.chk_FixDTL);
            this.panel1.Location = new System.Drawing.Point(319, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 68);
            this.panel1.TabIndex = 9;
            // 
            // chk_GenerateDTInfos
            // 
            this.chk_GenerateDTInfos.AutoSize = true;
            this.chk_GenerateDTInfos.Checked = true;
            this.chk_GenerateDTInfos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_GenerateDTInfos.Location = new System.Drawing.Point(10, 46);
            this.chk_GenerateDTInfos.Name = "chk_GenerateDTInfos";
            this.chk_GenerateDTInfos.Size = new System.Drawing.Size(186, 17);
            this.chk_GenerateDTInfos.TabIndex = 6;
            this.chk_GenerateDTInfos.Text = "Generate Datalog Infos (BMTune)";
            this.chk_GenerateDTInfos.UseVisualStyleBackColor = true;
            // 
            // txtAutoCopyVersion
            // 
            this.txtAutoCopyVersion.Location = new System.Drawing.Point(208, 136);
            this.txtAutoCopyVersion.Name = "txtAutoCopyVersion";
            this.txtAutoCopyVersion.Size = new System.Drawing.Size(31, 20);
            this.txtAutoCopyVersion.TabIndex = 17;
            this.txtAutoCopyVersion.Text = "114";
            // 
            // chk_AutoCopy
            // 
            this.chk_AutoCopy.AutoSize = true;
            this.chk_AutoCopy.Checked = true;
            this.chk_AutoCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_AutoCopy.Location = new System.Drawing.Point(30, 138);
            this.chk_AutoCopy.Name = "chk_AutoCopy";
            this.chk_AutoCopy.Size = new System.Drawing.Size(181, 17);
            this.chk_AutoCopy.TabIndex = 9;
            this.chk_AutoCopy.Text = "AutoCopy \'Class32_Locations\' V:";
            this.chk_AutoCopy.UseVisualStyleBackColor = true;
            // 
            // chk_OverLong
            // 
            this.chk_OverLong.AutoSize = true;
            this.chk_OverLong.Checked = true;
            this.chk_OverLong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_OverLong.Location = new System.Drawing.Point(30, 118);
            this.chk_OverLong.Name = "chk_OverLong";
            this.chk_OverLong.Size = new System.Drawing.Size(163, 17);
            this.chk_OverLong.TabIndex = 8;
            this.chk_OverLong.Text = "Over Check Long Reference";
            this.chk_OverLong.UseVisualStyleBackColor = true;
            // 
            // chk_Quick
            // 
            this.chk_Quick.AutoSize = true;
            this.chk_Quick.Checked = true;
            this.chk_Quick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Quick.Location = new System.Drawing.Point(30, 98);
            this.chk_Quick.Name = "chk_Quick";
            this.chk_Quick.Size = new System.Drawing.Size(109, 17);
            this.chk_Quick.TabIndex = 7;
            this.chk_Quick.Text = "Quick Generation";
            this.chk_Quick.UseVisualStyleBackColor = true;
            // 
            // chk_Generate
            // 
            this.chk_Generate.AutoSize = true;
            this.chk_Generate.Checked = true;
            this.chk_Generate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Generate.Location = new System.Drawing.Point(10, 78);
            this.chk_Generate.Name = "chk_Generate";
            this.chk_Generate.Size = new System.Drawing.Size(175, 17);
            this.chk_Generate.TabIndex = 5;
            this.chk_Generate.Text = "Generate Relocations(V1.14++)";
            this.chk_Generate.UseVisualStyleBackColor = true;
            // 
            // chk_RelocUnused
            // 
            this.chk_RelocUnused.AutoSize = true;
            this.chk_RelocUnused.Checked = true;
            this.chk_RelocUnused.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_RelocUnused.Location = new System.Drawing.Point(13, 88);
            this.chk_RelocUnused.Name = "chk_RelocUnused";
            this.chk_RelocUnused.Size = new System.Drawing.Size(175, 17);
            this.chk_RelocUnused.TabIndex = 8;
            this.chk_RelocUnused.Text = "Relocate Unused Bytes/Tables";
            this.chk_RelocUnused.UseVisualStyleBackColor = true;
            // 
            // chk_Array
            // 
            this.chk_Array.AutoSize = true;
            this.chk_Array.Checked = true;
            this.chk_Array.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Array.Location = new System.Drawing.Point(13, 68);
            this.chk_Array.Name = "chk_Array";
            this.chk_Array.Size = new System.Drawing.Size(152, 17);
            this.chk_Array.TabIndex = 7;
            this.chk_Array.Text = "Remove Bytes Array Table";
            this.chk_Array.UseVisualStyleBackColor = true;
            // 
            // chk_Word
            // 
            this.chk_Word.AutoSize = true;
            this.chk_Word.Checked = true;
            this.chk_Word.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Word.Location = new System.Drawing.Point(13, 48);
            this.chk_Word.Name = "chk_Word";
            this.chk_Word.Size = new System.Drawing.Size(125, 17);
            this.chk_Word.TabIndex = 6;
            this.chk_Word.Text = "Remove Word Table";
            this.chk_Word.UseVisualStyleBackColor = true;
            this.chk_Word.CheckedChanged += new System.EventHandler(this.chk_Word_CheckedChanged);
            // 
            // chk_SingleByte
            // 
            this.chk_SingleByte.AutoSize = true;
            this.chk_SingleByte.Checked = true;
            this.chk_SingleByte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_SingleByte.Location = new System.Drawing.Point(13, 28);
            this.chk_SingleByte.Name = "chk_SingleByte";
            this.chk_SingleByte.Size = new System.Drawing.Size(152, 17);
            this.chk_SingleByte.TabIndex = 5;
            this.chk_SingleByte.Text = "Remove Single Byte Table";
            this.chk_SingleByte.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.txt_BMTuneLocation);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(319, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 54);
            this.panel3.TabIndex = 11;
            // 
            // txt_BMTuneLocation
            // 
            this.txt_BMTuneLocation.Location = new System.Drawing.Point(144, 28);
            this.txt_BMTuneLocation.Name = "txt_BMTuneLocation";
            this.txt_BMTuneLocation.Size = new System.Drawing.Size(346, 20);
            this.txt_BMTuneLocation.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "BMTune Project Location:";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(193, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(46, 21);
            this.button3.TabIndex = 16;
            this.button3.Text = "tbl_5f2f";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(148, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 21);
            this.button2.TabIndex = 15;
            this.button2.Text = "tbl_5f03";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // chk_Quit
            // 
            this.chk_Quit.AutoSize = true;
            this.chk_Quit.Checked = true;
            this.chk_Quit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Quit.Location = new System.Drawing.Point(386, 235);
            this.chk_Quit.Name = "chk_Quit";
            this.chk_Quit.Size = new System.Drawing.Size(101, 17);
            this.chk_Quit.TabIndex = 9;
            this.chk_Quit.Text = "Quit when done";
            this.chk_Quit.UseVisualStyleBackColor = true;
            // 
            // chk_FreeSpace
            // 
            this.chk_FreeSpace.AutoSize = true;
            this.chk_FreeSpace.Checked = true;
            this.chk_FreeSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_FreeSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_FreeSpace.ForeColor = System.Drawing.Color.Blue;
            this.chk_FreeSpace.Location = new System.Drawing.Point(46, 169);
            this.chk_FreeSpace.Name = "chk_FreeSpace";
            this.chk_FreeSpace.Size = new System.Drawing.Size(154, 17);
            this.chk_FreeSpace.TabIndex = 8;
            this.chk_FreeSpace.Text = "ENABLE FREE SPACE";
            this.chk_FreeSpace.UseVisualStyleBackColor = true;
            this.chk_FreeSpace.CheckedChanged += new System.EventHandler(this.Chk_FreeSpace_CheckedChanged);
            // 
            // chk_ASM
            // 
            this.chk_ASM.AutoSize = true;
            this.chk_ASM.Checked = true;
            this.chk_ASM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ASM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ASM.ForeColor = System.Drawing.Color.Green;
            this.chk_ASM.Location = new System.Drawing.Point(334, 129);
            this.chk_ASM.Name = "chk_ASM";
            this.chk_ASM.Size = new System.Drawing.Size(213, 17);
            this.chk_ASM.TabIndex = 5;
            this.chk_ASM.Text = "ASSEMBLE/DISASSEMBLE FILE";
            this.chk_ASM.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(507, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Main Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(363, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "ASM/DASM Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(33, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Free Space Settings";
            // 
            // txt_Logs
            // 
            this.txt_Logs.Location = new System.Drawing.Point(11, 9);
            this.txt_Logs.Multiline = true;
            this.txt_Logs.Name = "txt_Logs";
            this.txt_Logs.ReadOnly = true;
            this.txt_Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Logs.Size = new System.Drawing.Size(302, 455);
            this.txt_Logs.TabIndex = 14;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(319, 438);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(245, 26);
            this.button4.TabIndex = 15;
            this.button4.Text = "Reload BACKUP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.chk_FreeSpace);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.txtAutoCopyVersion);
            this.panel4.Controls.Add(this.chk_Reloc);
            this.panel4.Controls.Add(this.chk_AutoCopy);
            this.panel4.Controls.Add(this.txtLastTable);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.chk_Generate);
            this.panel4.Controls.Add(this.chk_OverLong);
            this.panel4.Controls.Add(this.chk_Quick);
            this.panel4.Location = new System.Drawing.Point(570, 157);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(245, 307);
            this.panel4.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.chk_SingleByte);
            this.panel2.Controls.Add(this.chk_Word);
            this.panel2.Controls.Add(this.chk_RelocUnused);
            this.panel2.Controls.Add(this.chk_Array);
            this.panel2.Location = new System.Drawing.Point(16, 191);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 110);
            this.panel2.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(23, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "ASM/DASM V1.14++ Settings";
            // 
            // chk_Do111
            // 
            this.chk_Do111.AutoSize = true;
            this.chk_Do111.Checked = true;
            this.chk_Do111.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Do111.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Do111.ForeColor = System.Drawing.Color.Green;
            this.chk_Do111.Location = new System.Drawing.Point(609, 129);
            this.chk_Do111.Name = "chk_Do111";
            this.chk_Do111.Size = new System.Drawing.Size(177, 17);
            this.chk_Do111.TabIndex = 7;
            this.chk_Do111.Text = "ACTIVATE V1.14++ MODS";
            this.chk_Do111.UseVisualStyleBackColor = true;
            this.chk_Do111.CheckedChanged += new System.EventHandler(this.Chk_Do111_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(635, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "V1.14++ Settings";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button5.Location = new System.Drawing.Point(319, 411);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(245, 26);
            this.button5.TabIndex = 18;
            this.button5.Text = "Generate Infos Only";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 477);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chk_Do111);
            this.Controls.Add(this.chk_Quit);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.chk_ASM);
            this.Controls.Add(this.txt_Logs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Honda Binary Decryptor/Encryptor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chk_Reloc;
        private System.Windows.Forms.CheckBox chk_FixDTL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLastTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chk_RelocUnused;
        private System.Windows.Forms.CheckBox chk_Array;
        private System.Windows.Forms.CheckBox chk_Word;
        private System.Windows.Forms.CheckBox chk_SingleByte;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chk_Quit;
        private System.Windows.Forms.CheckBox chk_FreeSpace;
        private System.Windows.Forms.CheckBox chk_ASM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Logs;
        private System.Windows.Forms.CheckBox chk_Generate;
        private System.Windows.Forms.TextBox txt_BMTuneLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chk_GenerateDTInfos;
        private System.Windows.Forms.CheckBox chk_Quick;
        private System.Windows.Forms.CheckBox chk_OverLong;
        private System.Windows.Forms.CheckBox chk_AutoCopy;
        private System.Windows.Forms.TextBox txtAutoCopyVersion;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chk_Do111;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button5;
    }
}

