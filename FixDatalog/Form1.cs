using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FixDatalog
{
    public partial class Form1 : Form
    {
        private bool DecryptDPJump = false;
        private bool EncrytBMTune_DPJump = false;
        private DASM_Bytes DASM_Bytes_0;
        private ASM_Bytes ASM_Bytes_0;

        private string[] AllLines;
        private byte[] AllFileBytes;
        private int ByteDiff = 0;
        private bool AlreadyFixed = false;


        private int LastTableFoundCount = 3;

        private string FilePath = "";
        private List<string> RelocationsList = new List<string>();
        private List<string> RelocationsListDesc = new List<string>();      //list of 'PARAM FROM' within main program region
        private List<int> RelocationsListLines = new List<int>();

        private List<string> ListDesc = new List<string>();      //list of 'ORIGINAL TABLE' within parameters region
        private List<int> ListLines = new List<int>();

        private int CurrOffset = 0;
        private string SettingPath = Application.StartupPath + @"\BINSettings.txt";
        private string DescPath = Application.StartupPath + @"\BINDesc.txt";

        private string RAMDescPath = @"C:\Users\boule\Desktop\RAM_Addresse_Infos.txt";

        //##############################################
        //VARIABLE OF FREE UP SPACE
        public int FreeBytesAmount = 0;
        public string Method = "";

        public string LastTableBCK = "tbl_5f05";

        //##############################################

        public Form1()
        {
            InitializeComponent();

            txtLastTable.Text = LastTableBCK;
            //txtLastTable.Text = "tbl_5f2f";

            DASM_Bytes_0 = new DASM_Bytes();
            ASM_Bytes_0 = new ASM_Bytes();

            textBox1.Text = @"C:\Users\boule\Desktop\BM Devs\asm-dasm662\BMTune Update\DONE\1.09\BMTune_V1.1.4.asm";
            txt_BMTuneLocation.Text = @"C:\Users\boule\Documents\Visual Studio 2019\Projects\BMTune2";

            LoadSettings();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1) textBox1.Text = args[1];
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingPath))
            {
                string[] AllSettingLines = File.ReadAllLines(SettingPath);
                if (AllSettingLines.Length > 0)
                {
                    for (int i = 0; i < AllSettingLines.Length; i++)
                    {
                        if (AllSettingLines[i].Contains("="))
                        {
                            string[] SettingSplit = AllSettingLines[i].Split('=');
                            if (SettingSplit[0] == "DecryptDP") checkBox1.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "ParamRelococation") chk_Reloc.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "FixDTL") chk_FixDTL.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "GenerateRelocations") chk_Generate.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "GenerateDatalogInfos") chk_GenerateDTInfos.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "QuickGeneration") chk_Quick.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "CheckOverLong") chk_OverLong.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "AutoCopyToClass18") chk_AutoCopy.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "AutoCopyVersion") txtAutoCopyVersion.Text = SettingSplit[1];
                            if (SettingSplit[0] == "Activate111") chk_Do111.Checked = bool.Parse(SettingSplit[1]);

                            if (SettingSplit[0] == "ASM") chk_ASM.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "FreeUpSpace") chk_FreeSpace.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "QuitOnExit") chk_Quit.Checked = bool.Parse(SettingSplit[1]);

                            if (SettingSplit[0] == "DoSingleByte") chk_SingleByte.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "DoWord") chk_Word.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "DoArray") chk_Array.Checked = bool.Parse(SettingSplit[1]);
                            if (SettingSplit[0] == "RelocateUnused") chk_RelocUnused.Checked = bool.Parse(SettingSplit[1]);


                            if (SettingSplit[0] == "LastFile") textBox1.Text = SettingSplit[1];
                            if (SettingSplit[0] == "LastTable") txtLastTable.Text = SettingSplit[1];
                            if (SettingSplit[0] == "BMTuneLocation") txt_BMTuneLocation.Text = SettingSplit[1];
                        }
                    }

                    panel4.Enabled = chk_Do111.Checked;
                    panel2.Enabled = chk_FreeSpace.Checked;
                }
            }
            else SaveSettings();
        }

        private void SaveSettings()
        {
            string SaveString = "";
            SaveString += "DecryptDP=" + checkBox1.Checked.ToString() + Environment.NewLine;
            SaveString += "ParamRelococation=" + chk_Reloc.Checked.ToString() + Environment.NewLine;
            SaveString += "FixDTL=" + chk_FixDTL.Checked.ToString() + Environment.NewLine;
            SaveString += "GenerateRelocations=" + chk_Generate.Checked.ToString() + Environment.NewLine;
            SaveString += "GenerateDatalogInfos=" + chk_GenerateDTInfos.Checked.ToString() + Environment.NewLine;
            SaveString += "QuickGeneration=" + chk_Quick.Checked.ToString() + Environment.NewLine;
            SaveString += "CheckOverLong=" + chk_OverLong.Checked.ToString() + Environment.NewLine;
            SaveString += "AutoCopyToClass18=" + chk_AutoCopy.Checked.ToString() + Environment.NewLine;
            SaveString += "AutoCopyVersion=" + txtAutoCopyVersion.Text + Environment.NewLine;
            SaveString += "Activate111=" + chk_Do111.Checked.ToString() + Environment.NewLine;

            SaveString += "ASM=" + chk_ASM.Checked.ToString() + Environment.NewLine;
            SaveString += "FreeUpSpace=" + chk_FreeSpace.Checked.ToString() + Environment.NewLine;
            SaveString += "QuitOnExit=" + chk_Quit.Checked.ToString() + Environment.NewLine;

            SaveString += "DoSingleByte=" + chk_SingleByte.Checked.ToString() + Environment.NewLine;
            SaveString += "DoWord=" + chk_Word.Checked.ToString() + Environment.NewLine;
            SaveString += "DoArray=" + chk_Array.Checked.ToString() + Environment.NewLine;
            SaveString += "RelocateUnused=" + chk_RelocUnused.Checked.ToString() + Environment.NewLine;

            SaveString += "LastFile=" + textBox1.Text + Environment.NewLine;
            SaveString += "LastTable=" + txtLastTable.Text + Environment.NewLine;
            SaveString += "BMTuneLocation=" + txt_BMTuneLocation.Text + Environment.NewLine;

            File.Create(SettingPath).Dispose();
            File.WriteAllText(SettingPath, SaveString);
        }


        /*private int GetAddress(int Line)
        {
            string TLine = AllLines[Line];
            TLine = TLine.Replace(" ", "");
            string[] Split = TLine.Split(';');

            return int.Parse(Split[1].Substring(0, 4), NumberStyles.HexNumber);
        }*/

        private void LogThis(string ThisTxt)
        {
            txt_Logs.AppendText(ThisTxt + Environment.NewLine);
        }

        private void FIXDatalog_BIN()
        {
            AllFileBytes = File.ReadAllBytes(Application.StartupPath + @"\FileName.bin");

            LogThis("-----------------------------------------------------");
            LogThis("FIXING DATALOG...");
            //Get Bytes Diff
            for (int i = 0; i < AllFileBytes.Length; i++)
            {
                if (AllFileBytes[i] == 0x8D && AllFileBytes[i + 1] == 0x7E && AllFileBytes[i + 2] == 0x03)
                {
                    ByteDiff = (i + 3) - 0x4FDC;
                    i = AllFileBytes.Length;
                }
            }

            for (int i = 0; i < AllFileBytes.Length; i++)
            {
                //Re-encrypt DP Jump
                if (AllFileBytes[i] == 0x42 && AllFileBytes[i + 1] == 0x55 && AllFileBytes[i + 2] == 0x34)
                {
                    AllFileBytes[i + 3] = 0xc9;
                    AllFileBytes[i + 4] = 0x03;
                    AllFileBytes[i + 5] = 0x52;
                    AllFileBytes[i + 6] = 0x92;
                    AllFileBytes[i + 7] = 0x22;
                }

                if (AllFileBytes[i] == 0xFA && AllFileBytes[i + 1] == 0xCB && AllFileBytes[i + 2] == 0x8A)
                {
                    //0x18F3
                    SetAtLocation(i + 21, 0x4f54 + ByteDiff);
                    SetAtLocation(i + 35, 0x4f48 + ByteDiff);
                }

                if (AllFileBytes[i] == 0xD6 && AllFileBytes[i + 1] == 0x08 && AllFileBytes[i + 2] == 0x18)
                {
                    //0x386A
                    SetAtLocation(i + 24, 0x4f54 + ByteDiff);
                }

                if (AllFileBytes[i] == 0xD3 && AllFileBytes[i + 1] == 0x06 && AllFileBytes[i + 2] == 0xF5)
                {
                    //0x399A
                    SetAtLocation(i + 5, 0x4f60 + ByteDiff);
                }

                if (AllFileBytes[i] == 0x8D && AllFileBytes[i + 1] == 0x7E && AllFileBytes[i + 2] == 0x03)
                {
                    //0x4FDC
                    SetAtLocation(i - 47, 0x5066 + ByteDiff);

                    SetAtLocation(i + 3, 0x5064 + ByteDiff);
                    SetAtLocation(i + 7, 0x5060 + ByteDiff);
                    SetAtLocation(i + 22, 0x4FF3 + ByteDiff);
                    SetAtLocation(i + 28, 0x4FC4 + ByteDiff);
                    SetAtLocation(i + 38, 0x5060 + ByteDiff);
                }

                if (AllFileBytes[i] == 0xD6 && AllFileBytes[i + 1] == 0xF0 && AllFileBytes[i + 2] == 0x00)
                {
                    //0x5029
                    SetAtLocation(i + 7, 0x511e + ByteDiff);
                }

                if (AllFileBytes[i] == 0xA6 && AllFileBytes[i + 1] == 0xC0 && AllFileBytes[i + 2] == 0x62)
                {
                    //0x5071
                    SetAtLocation(i + 3, 0x518c + ByteDiff);    //shortdtl
                    SetAtLocation(i + 39, 0x4ffd + ByteDiff);
                }

                if (AllFileBytes[i] == 0xB3 && AllFileBytes[i + 1] == 0x45 && AllFileBytes[i + 2] == 0x98)
                {
                    //0x50AF
                    SetAtLocation(i + 3, 0x518C + ByteDiff);    //shortdtl
                    SetAtLocation(i + 7, 0x4FC4 + ByteDiff);
                    SetAtLocation(i + 17, 0x5162 + ByteDiff);   //shortdtl
                    SetAtLocation(i + 44, 0x4FB7 + ByteDiff);
                    SetAtLocation(i + 47, 0x5064 + ByteDiff);
                    SetAtLocation(i + 59, 0x4FAC + ByteDiff);
                    SetAtLocation(i + 62, 0x5064 + ByteDiff);
                    SetAtLocation(i + 76, 0x5060 + ByteDiff);
                }

                if (AllFileBytes[i] == 0x77 && AllFileBytes[i + 1] == 0xDD && AllFileBytes[i + 2] == 0x03)
                {
                    //0x511C  (DECIMAL = 20764)
                    SetAtLocation(i + 3, 0x5060 + ByteDiff);       //5060
                    SetAtLocation(i + 5, 0x50FA + ByteDiff);       //50FA
                    SetAtLocation(i + 7, 0x509D + ByteDiff);      //509D
                    SetAtLocation(i + 9, 0x50AD + ByteDiff);      //50AD
                    SetAtLocation(i + 11, 0x50BB + ByteDiff);      //50BB
                    SetAtLocation(i + 13, 0x50FA + ByteDiff);      //50FA
                    SetAtLocation(i + 15, 0x50A1 + ByteDiff);     //50A1
                    SetAtLocation(i + 17, 0x50C1 + ByteDiff);      //50C1
                    SetAtLocation(i + 19, 0x50C7 + ByteDiff);      //50C7
                    SetAtLocation(i + 21, 0x50CC + ByteDiff);      //50CC
                    SetAtLocation(i + 23, 0x50DD + ByteDiff);      //50DD
                    SetAtLocation(i + 25, 0x50EC + ByteDiff);      //50EC
                    SetAtLocation(i + 27, 0x5119 + ByteDiff);      //5119
                    SetAtLocation(i + 29, 0x5119 + ByteDiff);      //5119
                    SetAtLocation(i + 31, 0x5119 + ByteDiff);      //5119
                    SetAtLocation(i + 33, 0x5119 + ByteDiff);      //5119
                    SetAtLocation(i + 35, 0x5119 + ByteDiff);      //5119
                    SetAtLocation(i + 37, 0x5144 + ByteDiff);       //5144  shortdtl
                    SetAtLocation(i + 39, 0x5162 + ByteDiff);       //5162  shortdtl
                    SetAtLocation(i + 41, 0x518A + ByteDiff);      //518A   shortdtl

                    /*
                    0FAh,050h		50FA    20730
                    09Dh,050h		509D    20637
                    0ADh,050h		50AD    20653
                    0BBh,050h		50BB    20667
                    0FAh,050h		50FA    20730
                    0A1h,050h		50A1    20641
                    0C1h,050h		50C1    20673
                    0C7h,050h		50C7    20679
                    0CCh,050h		50CC    684
                    0DDh,050h		50DD    701
                    0ECh,050h		50EC    716
                    019h,051h		5119    761
                    019h,051h		5119
                    019h,051h		5119
                    019h,051h		5119
                    019h,051h		5119
                    044h,051h		5144    804
                    062h,051h		5162    834
                    08Ah,051h		518A    874
                    */
                }
            }


            File.Create(Application.StartupPath + @"\FileName.bin").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\FileName.bin", AllFileBytes);

            AlreadyFixed = true;
        }

        private void SetAtLocation(int AtIndex, int Location)
        {
            int High = (Location / 256);
            int Low = Location - (High * 256);
            AllFileBytes[AtIndex] = (byte)Low;
            AllFileBytes[AtIndex + 1] = (byte)High;
        }


        private void DASM_Bin()
        {
            LogThis("-----------------------------------------------------");
            LogThis("DISSASEMBLING .BIN...");
            progressBar1.Value = 0;
            //Copy Binary
            File.Create(Application.StartupPath + @"\FileName.bin").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\FileName.bin", File.ReadAllBytes(FilePath));
            //Decrypt DP
            if (DecryptDPJump)
            {
                AllFileBytes = File.ReadAllBytes(Application.StartupPath + @"\FileName.bin");
                for (int i = 0; i < AllFileBytes.Length; i++)
                {
                    if (AllFileBytes[i] == 0x92 && AllFileBytes[i + 1] == 0x22 && AllFileBytes[i + 2] == 0x03)
                    {
                        AllFileBytes[i - 3] = 0x03;
                        AllFileBytes[i] = 0xff;
                        AllFileBytes[i + 1] = 0xff;
                    }
                }

                File.WriteAllBytes(Application.StartupPath + @"\FileName.bin", AllFileBytes);
            }

            progressBar1.Value = 15;
            if (!AlreadyFixed && chk_FixDTL.Checked) FIXDatalog_BIN();
            progressBar1.Value = 30;


            //Copy BIN (CREATE BACKUP)
            File.Create(Application.StartupPath + @"\LastBIN_BACKUP.bin").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\LastBIN_BACKUP.bin", File.ReadAllBytes(FilePath));

            //Create DASM.exe
            File.Create(Application.StartupPath + @"\dasm662.exe").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\dasm662.exe", DASM_Bytes_0.ThisBytes);
            //Create DASM.bat
            string BatTxt = "dasm662 FileName.bin FileName.asm";
            File.Create(Application.StartupPath + @"\DASM.bat").Dispose();
            File.WriteAllText(Application.StartupPath + @"\DASM.bat", BatTxt);

            progressBar1.Value = 45;
            //Load DASM.bat
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = Application.StartupPath + @"\DASM.bat";
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.Verb = "runas";
            p.Start();

            progressBar1.Value = 60;
            Thread.Sleep(500);
            Process[] pname = Process.GetProcessesByName("DASM.bat");
            while (pname.Length > 1)
            {
                Thread.Sleep(1);
                pname = Process.GetProcessesByName("DASM.bat");
            }
            progressBar1.Value = 75;
            Thread.Sleep(1000);

            //Copy ASM File
            string ThisPath = Path.GetDirectoryName(FilePath);
            string ThisFN = Path.GetFileNameWithoutExtension(FilePath);
            string ThisNewPath = ThisPath + @"\" + ThisFN + ".asm";
            File.Create(ThisNewPath).Dispose();
            File.WriteAllText(ThisNewPath, File.ReadAllText(Application.StartupPath + @"\FileName.asm"));

            progressBar1.Value = 90;
            //Remove DASM
            File.Delete(Application.StartupPath + @"\FileName.bin");
            File.Delete(Application.StartupPath + @"\FileName.asm");
            File.Delete(Application.StartupPath + @"\dasm662.exe");
            File.Delete(Application.StartupPath + @"\DASM.bat");

            progressBar1.Value = 0;
        }


        private void ASM_Bin(bool DASM)
        {
            LogThis("-----------------------------------------------------");
            LogThis("ASSEMBLING .ASM...");
            progressBar1.Value = 0;
            string BuffPath = FilePath;

            bool IsFreeSpaceModdedBuffer = true;
            if (chk_Do111.Checked)
            {
                if (DASM)
                {
                    IsFreeSpaceModdedBuffer = IsFreeSpaceModded();
                    if (chk_Reloc.Checked) if (IsFreeSpaceModdedBuffer) GetRelocationsList();
                }
            }

            progressBar1.Value = 10;
            //Copy ASM
            File.Create(Application.StartupPath + @"\FileName.asm").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\FileName.asm", File.ReadAllBytes(FilePath));
            //Create ASM.exe
            File.Create(Application.StartupPath + @"\asm662.exe").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\asm662.exe", ASM_Bytes_0.ThisBytes);
            //Create ASM.bat
            string BatTxt = "asm662 FileName.asm FileName.bin";
            File.Create(Application.StartupPath + @"\ASM.bat").Dispose();
            File.WriteAllText(Application.StartupPath + @"\ASM.bat", BatTxt);

            //Copy ASM (CREATE BACKUP)
            File.Create(Application.StartupPath + @"\LastASM_BACKUP.asm").Dispose();
            File.WriteAllBytes(Application.StartupPath + @"\LastASM_BACKUP.asm", File.ReadAllBytes(FilePath));

            progressBar1.Value = 20;
            //Load ASM.bat
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = Application.StartupPath + @"\ASM.bat";
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.Verb = "runas";
            p.Start();

            progressBar1.Value = 35;
            Thread.Sleep(500);
            Process[] pname = Process.GetProcessesByName("ASM.bat");
            while (pname.Length > 1)
            {
                Thread.Sleep(1);
                pname = Process.GetProcessesByName("ASM.bat");
            }
            progressBar1.Value = 50;
            Thread.Sleep(1000);


            progressBar1.Value = 55;
            if (!AlreadyFixed && chk_FixDTL.Checked) FIXDatalog_BIN();
            progressBar1.Value = 60;

            //Copy ASM File
            string ThisPath = Path.GetDirectoryName(FilePath);
            string ThisFN = Path.GetFileNameWithoutExtension(FilePath);
            string ThisNewPath = ThisPath + @"\" + ThisFN + ".bin";
            File.Create(ThisNewPath).Dispose();
            File.WriteAllBytes(ThisNewPath, File.ReadAllBytes(Application.StartupPath + @"\FileName.bin"));

            //Check For Binary Lenght, if its not 32768 bytes then its not a correct bin, we detect the difference and try to apply it
            byte[] BuffBytess = File.ReadAllBytes(ThisNewPath);
            /*if (BuffBytess.Length != -1 && BuffBytess.Length != 32768)
            {
                //Missing bytes, adding them
                string Messssd = "BIN AREN'T CORRECT IN LENGHT\nHaving " + (BuffBytess.Length - 32768) + " less bytes\nDo you want to automatically add the bytes?";
                if (MessageBox.Show(Messssd, "Honda Binary", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                {
                    FreeBytesAmount = Math.Abs((BuffBytess.Length - 32768));
                    AddFreeBytes(true, false);
                }
            }*/
            //42 4D 54 75 6E 65 (BMTune)
            if (BuffBytess[0x7ffa] != 0x42
                && BuffBytess[0x7ffb] != 0x4D
                && BuffBytess[0x7ffc] != 0x54
                && BuffBytess[0x7ffd] != 0x75
                && BuffBytess[0x7ffe] != 0x6E
                && BuffBytess[0x7fff] != 0x65)
            {
                if (BuffBytess[0x7fff] == 0xff)
                {
                    int CheckAT = 0x7fff;
                    int Removebytes = 0;
                    while (BuffBytess[CheckAT] != 0x65 && BuffBytess[CheckAT] == 0xff)
                    {
                        CheckAT--;
                        Removebytes++;
                    }

                    string Messssd = "BIN AREN'T CORRECT IN LENGHT\nShould Add " + Removebytes + " bytes\nDo you want to automatically add the bytes?";
                    if (MessageBox.Show(Messssd, "Honda Binary", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    {
                        FreeBytesAmount = Removebytes;
                        AddFreeBytes(false, false);
                        //AddFreeBytes(true, false);
                    }
                }
                else
                {
                    //05Bh,057h,053h,04Fh,04Bh ; 7E05 (tbl_7e0f) 08Eh,08Eh
                    int CheckAT = 32271;
                    int Removebytes = 1;
                    while (BuffBytess[CheckAT - 5] != 0x5B
                        && BuffBytess[CheckAT - 4] != 0x57
                        && BuffBytess[CheckAT - 3] != 0x53
                        && BuffBytess[CheckAT - 2] != 0x4F
                        && BuffBytess[CheckAT - 1] != 0x4B
                        && BuffBytess[CheckAT] != 0x8E
                        && BuffBytess[CheckAT + 1] != 0x8E)
                    {
                        CheckAT++;
                        Removebytes++;
                    }

                    string Messssd = "BIN AREN'T CORRECT IN LENGHT\nShould Remove " + Removebytes + " bytes\nDo you want to automatically remove the bytes?";
                    if (MessageBox.Show(Messssd, "Honda Binary", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    {
                        FreeBytesAmount = Removebytes;
                        AddFreeBytes(false, true);
                    }
                }

                //Console.WriteLine(CheckAT.ToString("x4"));
            }

            progressBar1.Value = 65;
            //Remove DASM
            File.Delete(Application.StartupPath + @"\FileName.bin");
            File.Delete(Application.StartupPath + @"\FileName.asm");
            File.Delete(Application.StartupPath + @"\asm662.exe");
            File.Delete(Application.StartupPath + @"\ASM.bat");

            progressBar1.Value = 80;
            //DASM Again
            FilePath = ThisNewPath;
            if (DASM) DASM_Bin();
            progressBar1.Value = 90;

            FilePath = BuffPath;
            if (chk_Do111.Checked)
            {
                if (DASM)
                {
                    if (chk_Reloc.Checked) if (IsFreeSpaceModdedBuffer) SetRelocationsList();
                    progressBar1.Value = 94;
                    SetTableLocations();
                    if (chk_Generate.Checked) if (IsFreeSpaceModdedBuffer) GenerateRelocations();
                    progressBar1.Value = 97;
                    if (chk_AutoCopy.Checked) CopyToClass18();
                }
            }
            if (chk_GenerateDTInfos.Checked) SetExtrasDatalogInfosInASM();

            progressBar1.Value = 0;
        }

        private int GetIntFromHEXString(string hexString)
        {
            return Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }

        private void GetRelocationsList()
        {
            LogThis("-----------------------------------------------------");
            LogThis("GETTING NEW PARAMETERS LOCATIONS...");
            RelocationsList = new List<string>();
            RelocationsListDesc = new List<string>();
            RelocationsListLines = new List<int>();
            AllLines = File.ReadAllLines(FilePath); //reading .asm only

            ListDesc = new List<string>();
            ListLines = new List<int>();

            bool InParametersRegion = false;
            int CurrentFound = 0;       //CurrentFound != (LastTableFoundCount - 1)

            ResetLastTable();

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains(txtLastTable.Text))
                {
                    if (CurrentFound == LastTableFoundCount - 1) InParametersRegion = true;
                    else CurrentFound++;
                }

                if (!InParametersRegion)
                {
                    //PARAM FROM=tbl_5f44
                    //NEW ADDED=long_485
                    if (AllLines[i].Contains("PARAM FROM=") || AllLines[i].Contains("NEW ADDED="))
                    {
                        //add hex cmd (77FF)
                        RelocationsList.Add(GetHEXCMD(AllLines[i]));

                        //Console.WriteLine("Added:" + GetHEXCMD(AllLines[i]));

                        //add desc (PARAM FROM=)
                        string[] ParamSplit = AllLines[i].Split('=');
                        for (int i2 = 0; i2 < ParamSplit.Length; i2++)
                        {
                            if (ParamSplit[i2].Contains("PARAM FROM") || ParamSplit[i2].Contains("NEW ADDED"))
                            {
                                if (ParamSplit[i2].Contains("PARAM FROM")) RelocationsListDesc.Add("PARAM FROM=" + ParamSplit[i2 = 1].Substring(0, 8));
                                if (ParamSplit[i2].Contains("NEW ADDED")) RelocationsListDesc.Add("NEW ADDED=" + ParamSplit[i2 = 1].Substring(0, 8));
                            }
                        }

                        //Add Line number
                        RelocationsListLines.Add(i);
                        //Console.WriteLine("(Added) HEX=" + RelocationsList[RelocationsList.Count - 1] + "      Desc=" + RelocationsListDesc[RelocationsListDesc.Count - 1] + "     Line=" + RelocationsListLines[RelocationsListLines.Count - 1]);
                    }
                }
                else
                {
                    //; 5F5E		ORIGINAL TABLE=tbl_5f5e
                    if (AllLines[i].Contains(";"))
                    {
                        //TOTAL FREE BYTES=
                        //ORIGINAL TABLE=tbl_5f30
                        //**SINGLE WORD BUT USED MORE THAN ONCE**

                        string[] ParamSplit = AllLines[i].Split(';');
                        if (ParamSplit[1].Contains("ORIGINAL TABLE="))
                        {
                            string[] THsssplit = ParamSplit[1].Split('=');
                            ListLines.Add(i);
                            ListDesc.Add("\tORIGINAL TABLE=" + THsssplit[1].Substring(0, 8));
                        }
                    }
                }
            }
        }

        private void SetRelocationsList()
        {
            LogThis("-----------------------------------------------------");
            LogThis("SETTING NEW PARAMETERS LOCATIONS...");
            CurrOffset = 0;
            AllLines = File.ReadAllLines(FilePath); //reading .asm only
            for (int i = 0; i < RelocationsListLines.Count; i++)
            {
                int LineNumber = RelocationsListLines[i] + CurrOffset;

                //Console.WriteLine("Line=" + LineNumber + "\tFOUND HEX=" + GetHEXBytes(AllLines[LineNumber]) + "\tSEARCH HEX=" + RelocationsList[i]);

                bool ShouldDo = true;
                if (GetHEXBytes(AllLines[LineNumber]) != RelocationsList[i])
                {
                    //Console.WriteLine("NOT SAME HEX AT #" + LineNumber + "\tLine HEX=" + GetHEXBytes(AllLines[LineNumber]) + "\tSEARCH HEX=" + RelocationsList[i] + "\tLine=" + AllLines[LineNumber]);
                    
                    //not same line, proceed into manual search
                    int PossibleBefore = GetLineFromThisHEXCmd(LineNumber, RelocationsList[i], false);
                    int PossibleAfter = GetLineFromThisHEXCmd(LineNumber, RelocationsList[i], true);

                    FormEdit FormEdit_0 = new FormEdit();
                    FormEdit_0.SetInfos(LineNumber, RelocationsList[i], GetHEXBytes(AllLines[LineNumber]), RelocationsListDesc[i], PossibleBefore, PossibleAfter, AllLines);
                    FormEdit_0.ShowDialog();

                    //Only do if the location are still used (not removed from .bin)
                    if (!FormEdit_0.RemovingLocation)
                    {
                        //Reset LineNumber
                        CurrOffset += (FormEdit_0.ReturnLineNumber - LineNumber);
                        LineNumber = RelocationsListLines[i] + CurrOffset;
                    }
                    else ShouldDo = false;
                }

                //add infos
                //Console.WriteLine(RelocationsListDesc[i]);
                //if (ShouldDo) AllLines[LineNumber] += "\t\tPARAM FROM=" + RelocationsListDesc[i];
                if (ShouldDo) AllLines[LineNumber] += "\t\t" + RelocationsListDesc[i];
            }

            //Add all description that was within the parameters region
            int OffsetedTable = 0;
            for (int i = 0; i < ListLines.Count; i++)
            {
                //Console.WriteLine(ListLines[i]);
                //Console.WriteLine(AllLines[ListLines[i] + OffsetedTable]);
                bool ShouldDo = true;
                //while (!AllLines[ListLines[i] + OffsetedTable].Contains("tbl_")) OffsetedTable++;
                if (!AllLines[ListLines[i] + OffsetedTable].Contains("tbl_"))
                {
                    int PossibleBefore = GetLineMatchingThisString(ListLines[i] + OffsetedTable, "tbl_", false);
                    int PossibleAfter = GetLineMatchingThisString(ListLines[i] + OffsetedTable, "tbl_", true);

                    FormEdit FormEdit_0 = new FormEdit();
                    FormEdit_0.SetInfos(ListLines[i] + OffsetedTable, "tbl_", "NULL", ListDesc[i], PossibleBefore, PossibleAfter, AllLines);
                    FormEdit_0.ShowDialog();

                    //Only do if the location are still used (not removed from .bin)
                    if (!FormEdit_0.RemovingLocation)
                    {
                        //Reset LineNumber
                        OffsetedTable += (FormEdit_0.ReturnLineNumber - (ListLines[i] + OffsetedTable));
                    }
                    else ShouldDo = false;
                }

                if (ShouldDo) AllLines[ListLines[i] + OffsetedTable] += ListDesc[i];
            }

            //Reset Last Table Location
            ResetLastTable();

            //Save Changes
            File.Create(FilePath).Dispose();
            File.WriteAllLines(FilePath, AllLines);
        }

        private void ResetLastTable()
        {
            //AllLines = File.ReadAllLines(FilePath); //reading .asm only

            //Reset Last Table Location
            int LastTableCount = 0;
            for (int i = 0; i < AllLines.Length; i++) if (AllLines[i].Contains(txtLastTable.Text)) LastTableCount++;

            //Last table are no more used twice, reset with new location
            if (LastTableCount < 2)
            {
                for (int i = AllLines.Length - 1; i > 0; i--)
                {
                    //if (AllLines[i].Contains(txtLastTable.Text))
                    if (chk_Do111.Checked)
                    {
                        if (AllLines[i].Contains(LastTableBCK))
                        {
                            txtLastTable.Text = "tbl_" + GetAppliedLocation(AllLines[i]).ToLower();
                            LogThis("*** LAST TABLE CHANGED TO: " + txtLastTable.Text + " ***");
                            i = 0;
                        }
                    }
                    else
                    {
                        if (AllLines[i].Substring(0, 4) == "tbl_")
                        {
                            //txtLastTable.Text = "tbl_" + GetAppliedLocation(AllLines[i]).ToLower();
                            txtLastTable.Text = AllLines[i].Substring(0, 8);
                        }
                        if (i < AllLines.Length - (AllLines.Length / 10)) i = 0;
                    }
                }

                //if (!chk_Do111.Checked) LogThis("*** LAST TABLE CHANGED TO: " + txtLastTable.Text + " ***");
                LogThis("*** LAST TABLE CHANGED TO: " + txtLastTable.Text + " ***");
            }
        }

        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################

         //THIS MOSTLY REFER TO BINDesc.txt File

        private void GenerateRelocations()
        {
            LogThis("-----------------------------------------------------");
            LogThis("GENERATING RELOCATIONS INFOS...");
            AllLines = File.ReadAllLines(FilePath); //reading .asm only

            List<string> AllDescLiness = new List<string>();
            List<string> AllDescLinessSame = new List<string>();


            List<string> AllLongDescLiness = new List<string>();
            List<string> AllLongDescLinessUnchange = new List<string>();

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains("PARAM FROM=")
                    || AllLines[i].Contains("ORIGINAL TABLE=")
                    || AllLines[i].Contains("NEW ADDED="))
                {
                    string SearchingLocation = "";
                    string FoundLong = "";

                    string[] ParamSplit = AllLines[i].Split('=');

                    bool DoingTableInformation = false;
                    for (int i2 = 0; i2 < ParamSplit.Length; i2++)
                    {
                        //this is a table reference (tbl_5f45)
                        if (ParamSplit[i2].Contains("PARAM FROM") || ParamSplit[i2].Contains("ORIGINAL TABLE")) SearchingLocation = ParamSplit[i2 + 1];
                        //this is a long reference (long_485)
                        if (ParamSplit[i2].Contains("NEW ADDED")) FoundLong = ParamSplit[i2 + 1];
                        //we are doing table information (making this bool tru will copy all the table information in the main program region)
                        if (ParamSplit[i2].Contains("ORIGINAL TABLE")) DoingTableInformation = true;
                    }

                    if (SearchingLocation.Length >= 8 || FoundLong != "")
                    {
                        //Get the long reference in BMTune (long_485) from the table (tbl_5f45)
                        if (FoundLong == "")
                        {
                            SearchingLocation = SearchingLocation.Substring(4, 4);
                            FoundLong = GetLongVariableInBMTune(SearchingLocation);
                        }

                        //We found the long reference so we add informations
                        if (FoundLong != "")
                        {
                            string VariableAndFoundInDescStr = "";
                            VariableAndFoundInDescStr += "\t###\tVARIABLE=" + FoundLong;


                            string ThisssNewLocaation = GetAppliedLocation(AllLines[i]).ToLower();
                            int ThisssNewLocaationINT = GetIntFromHEXString(ThisssNewLocaation);
                            int LastTableLoc = GetIntFromHEXString(txtLastTable.Text.Substring(4, 4));

                            //every relocated parameters are now 1byte offset in within main program region
                            if (ThisssNewLocaationINT < LastTableLoc) ThisssNewLocaationINT++;

                            string ResesSTR = "";
                            ResesSTR += FoundLong + "=";
                            ResesSTR += "0x" + ThisssNewLocaationINT.ToString("x4") + "L";
                            ResesSTR += "       (Previous=tbl_" + SearchingLocation + " | 0x" + SearchingLocation + "L)";

                            if (!chk_Quick.Checked)
                            {
                                string FoundClassInBMTune = GetLongLocationInBMTune(FoundLong);
                                if (FoundClassInBMTune != "")
                                {
                                    VariableAndFoundInDescStr += FoundClassInBMTune;
                                    ResesSTR += "       " + FoundClassInBMTune;
                                }
                                else VariableAndFoundInDescStr += "\t**LONG REFERENCE CREATED IN BMTUNE BUT NEVER USED OR DETECTED**";
                            }

                            //Activate this line if you need to extract all location
                            //AllLongDescLiness.Add(FoundLong + " = 0x" + ThisssNewLocaationINT.ToString("x4") + "L;");

                            //first we add all line that been changed into descrition file
                            if (SearchingLocation != ThisssNewLocaation)
                            {
                                if (!chk_Quick.Checked) AllDescLiness.Add(ResesSTR);

                                if (ThisssNewLocaationINT < LastTableLoc)
                                {
                                    if (FoundLong != "//long_471"
                                        && FoundLong != "long_494")
                                    {
                                        AllLongDescLiness.Add(FoundLong + " = 0x" + ThisssNewLocaationINT.ToString("x4") + "L;     //Previous Location: 0x" + SearchingLocation + "L");

                                    }
                                    else if(FoundLong == "long_494")
                                    {
                                        AllLongDescLinessUnchange.Add(FoundLong + " = 0x" + ThisssNewLocaationINT.ToString("x4") + "L;     //Previous Location: 0x" + SearchingLocation + "L");
                                    }
                                }
                                else
                                {
                                    AllLongDescLinessUnchange.Add(FoundLong + " = 0x" + ThisssNewLocaationINT.ToString("x4") + "L;     //Previous Location: 0x" + SearchingLocation + "L");
                                }
                            }
                            else
                            {
                                //then we add all the line that use still the same table location
                                if (!chk_Quick.Checked) AllDescLinessSame.Add(ResesSTR);
                            }

                            AllLines[i] += VariableAndFoundInDescStr;

                            //add the table (parameters) information within main program region
                            if (DoingTableInformation)
                            {
                                //Reset Last Table Location
                                ResetLastTable();

                                int CurrentFound = 0;       //CurrentFound != (LastTableFoundCount - 1)
                                string SearchthisTable = "tbl_" + SearchingLocation;
                                //reloop in all lines but avoid parameters region
                                for (int i2 = 0; i2 < AllLines.Length; i2++)
                                {
                                    if (AllLines[i2].Contains(txtLastTable.Text))
                                    {
                                        if (CurrentFound == LastTableFoundCount - 1) i2 = AllLines.Length;
                                        else CurrentFound++;
                                    }

                                    if (i2 != AllLines.Length)
                                    {
                                        if (AllLines[i2].Contains(SearchthisTable))
                                        {
                                            if (AllLines[i2].Substring(0, 4) != "tbl_")
                                            {
                                                AllLines[i2] += "\tORIGINAL TABLE=" + SearchthisTable + VariableAndFoundInDescStr;
                                            }
                                        }
                                    }
                                }
                            }

                            //LogThis("GENERATED INFOS: '" + FoundLong + "(tbl_" + SearchingLocation + "/0x" + SearchingLocation + "L)'\tAT LINE: " + i);
                        }
                        else
                        {
                            AllLines[i] += "\t**NOT FOUND IN BMTUNE BUT USED IN BIN**";
                        }
                    }
                }
            }

            //Save Changes
            File.Create(FilePath).Dispose();
            File.WriteAllLines(FilePath, AllLines);

            //#######################################################
            //Descrition BIN
            //AllDescLiness = AllDescLiness.OrderBy(x => x).ToList();
            //AllDescLinessSame = AllDescLinessSame.OrderBy(x => x).ToList();
            if (!chk_Quick.Checked) AllDescLiness = ReOrderList(AllDescLiness, "long_", "=");
            if (!chk_Quick.Checked) AllDescLinessSame = ReOrderList(AllDescLinessSame, "long_", "=");
            AllLongDescLiness = ReOrderList(AllLongDescLiness, "long_", " = ");
            AllLongDescLinessUnchange = ReOrderList(AllLongDescLinessUnchange, "long_", " = ");

            if (!chk_Quick.Checked)
            {
                AllDescLiness.Add("");
                AllDescLiness.Add("");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("");
                AllDescLiness.Add("ALL THESE LOCATION HASNT BEEN CHANGED");
                AllDescLiness.Add("");

                for (int i = 0; i < AllDescLinessSame.Count; i++) AllDescLiness.Add(AllDescLinessSame[i]);

                AllDescLiness.Add("");
                AllDescLiness.Add("");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("");
                AllDescLiness.Add("POSSIBLE CRITICAL ERROR OR UNUSED VARIABLE REPORT");
                AllDescLiness.Add("");

                //get possible issue or good informations to know to add to the desc file
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Contains("**")) AllDescLiness.Add("Check line: " + i + "      " + AllLines[i].Substring(AllLines[i].IndexOf("**")));
                }

                AllDescLiness.Add("");
                AllDescLiness.Add("");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("##############################################################################################");
                AllDescLiness.Add("");
                AllDescLiness.Add("COPY THESE LONG ADRESSES LOCATION TO BMTUNE");
                AllDescLiness.Add("");
            }

            for (int i = 0; i < AllLongDescLiness.Count; i++) AllDescLiness.Add("        this.class18_0.class13_0." + AllLongDescLiness[i]);
            AllDescLiness.Add("");
            AllDescLiness.Add("#############################################");
            AllDescLiness.Add("");
            for (int i = 0; i < AllLongDescLinessUnchange.Count; i++) AllDescLiness.Add("        this.class18_0.class13_0." + AllLongDescLinessUnchange[i]);

            string[] RemaddeegLines = new string[AllDescLiness.Count];
            for (int i = 0; i < AllDescLiness.Count; i++) RemaddeegLines[i] = AllDescLiness[i];

            //Save
            File.Create(DescPath).Dispose();
            File.WriteAllLines(DescPath, RemaddeegLines);
        }

        private List<string> ReOrderList(List<string> ThisList, string StartString, string MidString)
        {
            //StartString = "long_"
            //StartString = "        class13_0.long_"
            //MidString = "="
            //MidString = " = "
            List<string> ReOrderedList = new List<string>();
            for (int i = 0; i < 999; i++)
            {
                for (int i2 = 0; i2 < ThisList.Count; i2++) if (ThisList[i2].Contains(StartString + i + MidString)) ReOrderedList.Add(ThisList[i2]);
            }

            if (ReOrderedList.Count != ThisList.Count)
            {
                for (int i2 = 0; i2 < ThisList.Count; i2++)
                {
                    bool HaveThisLine = false;
                    for (int i = 0; i < ReOrderedList.Count; i++) if (ThisList[i2] == ReOrderedList[i]) HaveThisLine = true;

                    if (!HaveThisLine) ReOrderedList.Add(ThisList[i2]);
                }
            }

            return ReOrderedList;
        }

        private void CopyToClass18()
        {
            LogThis("-----------------------------------------------------");
            LogThis("AUTO COPY LOCATION TO Class18...");
            string[] AllDescLines = File.ReadAllLines(DescPath);


            List<string> UnchangeList = new List<string>();
            List<string> ChangeList = new List<string>();

            //Load All Changed and Unchanged Locations Desc Lines
            if (!chk_Quick.Checked)
            {
                bool DoingPart2 = false;
                bool WithinLocationLines = false;
                for (int i = 0; i < AllDescLines.Length; i++)
                {
                    if (WithinLocationLines)
                    {
                        if (AllDescLines[i].Contains("long_"))
                        {
                            if (!DoingPart2) ChangeList.Add(AllDescLines[i]);
                            else UnchangeList.Add(AllDescLines[i]);
                        }
                        else
                        {
                            DoingPart2 = true;
                        }
                    }
                    else
                    {
                        if (AllDescLines[i].Contains("COPY THESE LONG ADRESSES"))
                        {
                            WithinLocationLines = true;
                            i++;
                        }
                    }
                }
            }
            else
            {
                bool DoingPart2 = false;
                for (int i = 0; i < AllDescLines.Length; i++)
                {
                    if (AllDescLines[i].Contains("long_"))
                    {
                        if (!DoingPart2) ChangeList.Add(AllDescLines[i]);
                        else UnchangeList.Add(AllDescLines[i]);
                    }
                    else
                    {
                        DoingPart2 = true;
                    }
                }
            }


            List<string> Class18Remade = new List<string>();
            AllDescLines = File.ReadAllLines(txt_BMTuneLocation.Text + @"\Class32_Locations.cs");

            bool DoingUnchangeAutoCopy = false;
            bool DoingChangeAutoCopy = false;
            bool UnchangeAdded = false;
            bool ChangeAdded = false;
            for (int i = 0; i < AllDescLines.Length; i++)
            {
                if (DoingUnchangeAutoCopy || DoingChangeAutoCopy)
                {
                    if (DoingUnchangeAutoCopy)
                    {
                        if (AllDescLines[i].Contains("Auto Copy end Line here ALL"))
                        {
                            DoingUnchangeAutoCopy = false;
                            Class18Remade.Add(AllDescLines[i]);
                        }
                        else
                        {
                            if (!UnchangeAdded)
                            {
                                UnchangeAdded = true;
                                for (int i2 = 0; i2 < UnchangeList.Count; i2++) Class18Remade.Add(UnchangeList[i2]);
                            }
                        }
                    }
                    if (DoingChangeAutoCopy)
                    {
                        if (AllDescLines[i].Contains("Auto Copy end Line here " + txtAutoCopyVersion.Text))
                        {
                            DoingChangeAutoCopy = false;
                            Class18Remade.Add(AllDescLines[i]);
                        }
                        else
                        {
                            if (!ChangeAdded)
                            {
                                ChangeAdded = true;
                                for (int i2 = 0; i2 < ChangeList.Count; i2++) Class18Remade.Add(ChangeList[i2]);
                            }
                        }
                    }
                }
                else
                {
                    Class18Remade.Add(AllDescLines[i]);
                    if (AllDescLines[i].Contains("Auto Copy Line here ALL")) DoingUnchangeAutoCopy = true;
                    if (AllDescLines[i].Contains("Auto Copy Line here " + txtAutoCopyVersion.Text)) DoingChangeAutoCopy = true;
                }
            }

            //Remake
            string[] RemaddeegLines = new string[Class18Remade.Count];
            for (int i = 0; i < Class18Remade.Count; i++) RemaddeegLines[i] = Class18Remade[i];

            //Save
            File.Create(txt_BMTuneLocation.Text + @"\Class32_Locations.cs").Dispose();
            File.WriteAllLines(txt_BMTuneLocation.Text + @"\Class32_Locations.cs", RemaddeegLines);
        }

        private string GetLongLocationInBMTune(string ThisLong)
        {
            string FoundLong = "\tFOUND IN FILE=";

            string pattern = @"\.cs$";
            var matches = Directory.GetFiles(txt_BMTuneLocation.Text).Where(path => Regex.Match(path, pattern).Success);

            //check in all '*.cs' files
            foreach (string file in matches)
            {
                //dont check in class13 since this is where we declare long variable
                if (Path.GetFileName(file) != "Class13_u.cs"
                    && Path.GetFileName(file) != "Class32_Locations.cs")
                {
                    string[] ClassLines = File.ReadAllLines(file);

                    if (ClassLines.Length > 0)
                    {
                        bool StartOfClass18 = true;
                        for (int i = 0; i < ClassLines.Length; i++)
                        {
                            //in class18, check lines only under 'method_146'
                            if (Path.GetFileName(file) == "Class18.cs")
                            {
                                if (StartOfClass18) while(!ClassLines[i].Contains("method_146")) i++;
                                StartOfClass18 = false;
                            }

                            //check if the long are the exact one (not only matching)
                            //long_321
                            if (ClassLines[i].Contains(ThisLong))
                            {
                                //if it has a space after the same 'long_xxx' name then its the exact one
                                //there was an issue where by exempe 'long_40' was finding result of 'long_405' exemple
                                if (ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "0"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "1"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "2"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "3"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "4"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "5"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "6"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "7"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "8"
                                    && ClassLines[i].Substring(ClassLines[i].IndexOf(ThisLong) + ThisLong.Length, 1) != "9")
                                {
                                    if (FoundLong != "\tFOUND IN FILE=") FoundLong += "\t###\t";
                                    FoundLong += Path.GetFileName(file);
                                    if (ClassLines[i].Contains(".Text")
                                        || ClassLines[i].Contains(".Value")
                                        || ClassLines[i].Contains(".Checked")
                                        || ClassLines[i].Contains(".Enabled")
                                        || ClassLines[i].Contains(".Visible")
                                        || ClassLines[i].Contains(".ForeColor")
                                        || ClassLines[i].Contains(".Color")) {
                                        string[] CurrentClassCmd = ClassLines[i].Split('.');
                                        for (int i2 = 1; i2 < CurrentClassCmd.Length; i2++)
                                        {
                                            if (CurrentClassCmd[i2].Contains("Text") || CurrentClassCmd[i2].Contains("Value")) FoundLong += " (" + CurrentClassCmd[i2 - 1] + ")";
                                        }
                                    }
                                    else if (ClassLines[i].Contains("method_")) {
                                        int DoingInnt = ClassLines[i].IndexOf("method_");
                                        string Linstes = ClassLines[i];
                                        FoundLong += " (";
                                        while (Linstes[DoingInnt] != '(')
                                        {
                                            FoundLong += Linstes[DoingInnt];
                                            DoingInnt++;
                                        }
                                        FoundLong += ")";
                                        //FoundLong += ClassLines[i].Substring(ClassLines[i].IndexOf("method_"), ClassLines[i].IndexOf("(") - ClassLines[i].IndexOf("method_"));
                                    }
                                    else
                                    {
                                        if (ClassLines[i - 1].Contains(".Text")
                                            || ClassLines[i - 1].Contains(".Value")
                                            || ClassLines[i - 1].Contains(".Checked")
                                            || ClassLines[i - 1].Contains(".Enabled")
                                            || ClassLines[i - 1].Contains(".Visible")
                                            || ClassLines[i - 1].Contains(".ForeColor")
                                            || ClassLines[i - 1].Contains(".Color")) {
                                            string[] CurrentClassCmd = ClassLines[i - 1].Split('.');
                                            for (int i2 = 1; i2 < CurrentClassCmd.Length; i2++)
                                            {
                                                if (CurrentClassCmd[i2].Contains("Text") || CurrentClassCmd[i2].Contains("Value")) FoundLong += " (" + CurrentClassCmd[i2 - 1] + ")";
                                            }
                                        }
                                        else if (ClassLines[i - 1].Contains("method_"))
                                        {
                                            int DoingInnt = ClassLines[i - 1].IndexOf("method_");
                                            string Linstes = ClassLines[i - 1];
                                            FoundLong += " (";
                                            while (Linstes[DoingInnt] != '(')
                                            {
                                                FoundLong += Linstes[DoingInnt];
                                                DoingInnt++;
                                            }
                                            FoundLong += ")";
                                            //FoundLong += ClassLines[i - 1].Substring(ClassLines[i - 1].IndexOf("method_"), ClassLines[i - 1].IndexOf("(") - ClassLines[i - 1].IndexOf("method_"));
                                        }
                                        else
                                        {
                                            //remove whitespace, tabs
                                            string CurrentClassLine = ClassLines[i];
                                            CurrentClassLine = CurrentClassLine.Replace("\t", "");
                                            if (CurrentClassLine.Contains("//")) CurrentClassLine = CurrentClassLine.Substring(0, CurrentClassLine.IndexOf("//"));
                                            while (CurrentClassLine[0] == ' ') CurrentClassLine = CurrentClassLine.Substring(1);

                                            FoundLong += " (" + CurrentClassLine + ")";
                                        }
                                    }

                                    //Dont search for other results when we already found a match
                                    i = ClassLines.Length;
                                }
                            }
                        }
                    }
                }
            }
            if (FoundLong == "\tFOUND IN FILE=") FoundLong = "";
            return FoundLong;
        } 

        private string GetLongVariableInBMTune(string ThisLocation)
        {
            //long_65 = 0x61f8L,
            //This command find the matching 'long_122' variable from location'5f41' in 'Class18.cs'

            string FoundLong = "";
            ThisLocation = "0x" + ThisLocation + "L";

            if (File.Exists(txt_BMTuneLocation.Text + @"\Class18.cs"))
            {
                string[] ClassLines = File.ReadAllLines(txt_BMTuneLocation.Text + @"\Class18.cs");

                for (int i =0; i < ClassLines.Length; i++)
                {
                    if (ClassLines[i].Contains(ThisLocation))
                    {
                        string CurrentClassLine = ClassLines[i];
                        CurrentClassLine = CurrentClassLine.Replace(" ", "");
                        CurrentClassLine = CurrentClassLine.Replace("\t", "");
                        if (CurrentClassLine.Contains("=")) {
                            string[] CurrentClassCmd = CurrentClassLine.Split('=');
                            if (CurrentClassCmd[0].Contains("long_")) FoundLong = CurrentClassCmd[0];
                            //if (CurrentClassCmd[0].Contains("long_") && !CurrentClassCmd[0].Contains("//long_")) FoundLong = CurrentClassCmd[0];
                        }
                    }
                }
            }

            if (FoundLong == "" && chk_OverLong.Checked)
            {
                string pattern = @"\.cs$";
                var matches = Directory.GetFiles(txt_BMTuneLocation.Text).Where(path => Regex.Match(path, pattern).Success);

                //check in all '*.cs' files
                foreach (string file in matches)
                {
                    //dont check in class13 since this is where we declare long variable
                    if (Path.GetFileName(file) != "Class13_u.cs"
                        && Path.GetFileName(file) != "Class18.cs"
                        && Path.GetFileName(file) != "Class32_Locations.cs")
                    {
                        string[] ClassLines = File.ReadAllLines(file);

                        if (ClassLines.Length > 0)
                        {
                            for (int i = 0; i < ClassLines.Length; i++)
                            {
                                if (ClassLines[i].Contains(ThisLocation))
                                {
                                    string CurrentClassLine = ClassLines[i];
                                    CurrentClassLine = CurrentClassLine.Replace(" ", "");
                                    CurrentClassLine = CurrentClassLine.Replace("\t", "");
                                    if (CurrentClassLine.Contains("="))
                                    {
                                        string[] CurrentClassCmd = CurrentClassLine.Split('=');
                                        if (CurrentClassCmd[0].Contains("long_")) FoundLong = CurrentClassCmd[0];
                                        //if (CurrentClassCmd[0].Contains("long_") && !CurrentClassCmd[0].Contains("//long_")) FoundLong = CurrentClassCmd[0];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return FoundLong;
        }

        private int GetLineFromThisHEXCmd(int StartIndex, string ThisHEXCmd, bool GoingDown)
        {
            //search for matching cmd exmple: '77FF'

            if (GoingDown)
            {
                //Search after (down)
                for (int i = StartIndex; i < AllLines.Length; i++) if (GetHEXBytes(AllLines[i]) == ThisHEXCmd) return i;
            }
            else
            {
                //Search before (up)
                for (int i = StartIndex; i > 0; i--) if (GetHEXBytes(AllLines[i]) == ThisHEXCmd) return i;
            }
            return -99999;
        }

        private int GetLineMatchingThisString(int StartIndex, string ThisStrCmd, bool GoingDown)
        {
            //search for matching cmd exmple: '77FF'

            if (GoingDown)
            {
                //Search after (down)
                for (int i = StartIndex; i < AllLines.Length; i++) if (AllLines[i].Contains(ThisStrCmd)) return i;
            }
            else
            {
                //Search before (up)
                for (int i = StartIndex; i > 0; i--) if (AllLines[i].Contains(ThisStrCmd)) return i;
            }
            return -99999;
        }

        //This Function is the same as GetAppliedLocation()
        /*private string GetHEXLocation(string ThisLine)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 909D415F
            //this get location '1843'

            if (ThisLine.Contains(";"))
            {
                string[] SplitLine = ThisLine.Split(';');
                string LocationsDecs = SplitLine[1];

                if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);

                if (LocationsDecs.Contains(" "))
                {
                    string[] SplitDescLine = LocationsDecs.Split(' ');
                    return SplitDescLine[0];
                }
            }
            return "";
        }*/

        private string GetHEXCMD(string ThisLine)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 909D415F
            //this return the byte 77FF from the cmd 'LB     A, #0FFh'

            //LB CMD
            if (ThisLine.Contains("LB")
                && ThisLine.Contains("A")
                && ThisLine.Contains("#")
                && ThisLine.Contains("h"))
            {
                string[] SplitLine = ThisLine.Split('#');
                return "77" + SplitLine[1].Substring(1, 2).ToUpper() ;
            }

            return GetHEXBytes(ThisLine);
        }

        private string GetHEXBytes(string ThisLine)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 77FF
            //this get the hex bytes '77FF'in the description

            if (ThisLine.Contains(";"))
            {
                string[] SplitLine = ThisLine.Split(';');
                string LocationsDecs = SplitLine[1];
                string TestttLinee = ThisLine.Substring(0, 4);  //check for 'tbl_'

                if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);

                LocationsDecs = LocationsDecs.Replace("\t", " ");

                if (LocationsDecs.Contains(" "))
                {
                    string[] SplitDescLine = LocationsDecs.Split(' ');

                    if (TestttLinee != "tbl_")
                    {
                        if (SplitDescLine.Length >= 5) return SplitDescLine[4]; //this one should return always
                        if (SplitDescLine.Length >= 4) return SplitDescLine[3];
                        if (SplitDescLine.Length >= 3) return SplitDescLine[2];
                        if (SplitDescLine.Length >= 2) return SplitDescLine[1];
                        if (SplitDescLine.Length >= 1) return SplitDescLine[0];
                    }
                    else
                    {
                        if (SplitDescLine.Length >= 1) return SplitDescLine[0]; //return '1843' for line that start with 'tbl_'
                    }
                }
                else if (LocationsDecs.Length == 4)
                {
                    return LocationsDecs;
                }
            }
            return "Null";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DecryptDPJump = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            AlreadyFixed = false;
            FilePath = textBox1.Text;
            if (chk_Do111.Checked)
            {
                if (chk_FreeSpace.Checked) FreeUpSpace();
            }

            if (chk_ASM.Checked)
            {

                if (Path.GetExtension(textBox1.Text) == ".bin" || Path.GetExtension(textBox1.Text) == ".asm")
                {
                    if (Path.GetExtension(textBox1.Text) == ".bin") DASM_Bin();
                    if (Path.GetExtension(textBox1.Text) == ".asm") ASM_Bin(true);
                }
            }
            SaveSettings();
            LogThis("-----------------------------------------------------");
            LogThis("DONE");
            if (chk_Quit.Checked) this.Close();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) textBox1.Text = openFileDialog1.FileName;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }


        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################

        private void SetExtrasDatalogInfosInASM()
        {
            LogThis("-----------------------------------------------------");
            LogThis("GENERATING DATALOG INFOS...");
            AllLines = File.ReadAllLines(FilePath); //reading .asm only

            ResetLastTable();
            for (int i = 0; i < AllLines.Length; i++)
            {
                //dont do parameters region
                if (AllLines[i].Substring(0, 8) == txtLastTable.Text) i = AllLines.Length;

                if (i != AllLines.Length) AllLines[i] += GetLineDatalogInfos(AllLines[i]);
            }

            //Save
            File.Create(FilePath).Dispose();
            File.WriteAllLines(FilePath, AllLines);
        }

        private string GetLineDatalogInfos(string ThisLine)
        {
            //Console.WriteLine(ThisLine);

            List<string> DatalogInfosName = new List<string>();
            List<string> DatalogInfosTxt = new List<string>();

            if (File.Exists(RAMDescPath))
            {
                string[] AllRAMLines = File.ReadAllLines(RAMDescPath);
                if (AllRAMLines.Length > 0)
                {
                    for (int i = 0; i < AllRAMLines.Length; i++)
                    {
                        if (AllRAMLines[i].Contains(","))
                        {
                            string[] SplittedRAM = AllRAMLines[i].Split(',');
                            if (SplittedRAM.Length >= 2)
                            {
                                if (SplittedRAM[0] != "" && SplittedRAM[1] != "")
                                {
                                    string RamAddr = SplittedRAM[1].Replace("\t", "");
                                    RamAddr = RamAddr.Replace(" ", "");
                                    RamAddr = RamAddr.Replace("h.", "h).");

                                    DatalogInfosName.Add(SplittedRAM[0]);
                                    DatalogInfosTxt.Add(RamAddr);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                DatalogInfosName.Add("Ignition Cut"); DatalogInfosTxt.Add("000E6h");
                DatalogInfosName.Add("Possible Ignition Cut Related"); DatalogInfosTxt.Add("0E6h");
                DatalogInfosName.Add("Possible Fuel Cut Related"); DatalogInfosTxt.Add("00124h");
                DatalogInfosName.Add("Set RPM Cut Flag"); DatalogInfosTxt.Add("00218h");
                DatalogInfosName.Add("Reset RPM Cut Flag"); DatalogInfosTxt.Add("00228h");

                DatalogInfosName.Add("Auto/Manual Config Byte"); DatalogInfosTxt.Add("0011Eh).3");

                DatalogInfosName.Add("Possible Vtec Flag"); DatalogInfosTxt.Add("00127h");
                DatalogInfosName.Add("Vtec (Vtec Output Flag)"); DatalogInfosTxt.Add("00127h).1");
                DatalogInfosName.Add("Vtec (Pressure Output Flag??)"); DatalogInfosTxt.Add("00127h).2");
                DatalogInfosName.Add("Vtec (Min VSS Flag)"); DatalogInfosTxt.Add("00131h).0");
                DatalogInfosName.Add("Vtec (Flag set above Vtec RPM)"); DatalogInfosTxt.Add("00131h).1");
                DatalogInfosName.Add("Vtec (Flag set above some unspecified RPM)"); DatalogInfosTxt.Add("00131h).2");
                DatalogInfosName.Add("Rev Cut Set RPM"); DatalogInfosTxt.Add("00192h");
                DatalogInfosName.Add("Rev Cut Reset RPM"); DatalogInfosTxt.Add("00194h");
                DatalogInfosName.Add("Fuel/Ignition Column"); DatalogInfosTxt.Add("001D2h");
                DatalogInfosName.Add("Low Cam Row"); DatalogInfosTxt.Add("001D8h");
                DatalogInfosName.Add("High Cam Row"); DatalogInfosTxt.Add("001D9h");

                DatalogInfosName.Add("PSP Input (Power Steering Pressure)"); DatalogInfosTxt.Add("00210h).3");
                DatalogInfosName.Add("AC Switch Input"); DatalogInfosTxt.Add("00211h).2");
                //DatalogInfosName.Add("Indicate MAP sensor Problem"); DatalogInfosTxt.Add("00212h).2");
                //DatalogInfosName.Add("Indicate Others MAP sensor Problem"); DatalogInfosTxt.Add("00212h).4");
                //DatalogInfosName.Add("Indicate TPS sensor Problem"); DatalogInfosTxt.Add("00212h).6");
                DatalogInfosName.Add("VTEC Enable Flag"); DatalogInfosTxt.Add("00216h).4");
                DatalogInfosName.Add("O2 Heater Enable Flag"); DatalogInfosTxt.Add("00216h).6");
                DatalogInfosName.Add("PA (Baro) Enable Flag"); DatalogInfosTxt.Add("00227h).4");
                DatalogInfosName.Add("Knock Sensor Enable Flag"); DatalogInfosTxt.Add("00227h).6");
                DatalogInfosName.Add("VTEC VSS Check Enable Flag"); DatalogInfosTxt.Add("0022Dh).1");

                DatalogInfosName.Add("Current RPM(1byte form)"); DatalogInfosTxt.Add("00236h");
                DatalogInfosName.Add("Knock Retard"); DatalogInfosTxt.Add("00244h");
                DatalogInfosName.Add("Ignition Final (Table+Corr)"); DatalogInfosTxt.Add("00247h");
                DatalogInfosName.Add("Ignition Adv/Retard Related"); DatalogInfosTxt.Add("00248h");
                DatalogInfosName.Add("Current Gear"); DatalogInfosTxt.Add("0024Fh");
                DatalogInfosName.Add("Controls ADC MUX Channel"); DatalogInfosTxt.Add("00257h");
                DatalogInfosName.Add("Current Target Idle RPM"); DatalogInfosTxt.Add("0025Ah");

                DatalogInfosName.Add("Injector Duration"); DatalogInfosTxt.Add("003A0h");
                DatalogInfosName.Add("PA (Baro) Sensor"); DatalogInfosTxt.Add("003CDh");
                DatalogInfosName.Add("ELD??"); DatalogInfosTxt.Add("003D2h");

                DatalogInfosName.Add("82C55 PortA Inputs"); DatalogInfosTxt.Add("00f00h");
                DatalogInfosName.Add("82C55 PortB Outputs"); DatalogInfosTxt.Add("01f00h");
                DatalogInfosName.Add("82C55 PortC Outputs"); DatalogInfosTxt.Add("02f00h");
                DatalogInfosName.Add("82C55 Command Register"); DatalogInfosTxt.Add("03f00h");

                DatalogInfosName.Add("Memory mapped Inputs"); DatalogInfosTxt.Add("04700h");

                DatalogInfosName.Add("A/C Clutch Output"); DatalogInfosTxt.Add("P0.0");
                DatalogInfosName.Add("Purge Canister Output"); DatalogInfosTxt.Add("P0.1");
                DatalogInfosName.Add("IAB Output"); DatalogInfosTxt.Add("P0.5");
                DatalogInfosName.Add("Left VTEC Transistor Output (IC14)"); DatalogInfosTxt.Add("P1.0");
                DatalogInfosName.Add("Right VTEC Transistor Output (IC13)"); DatalogInfosTxt.Add("P1.1");

                //##########################################################################

                DatalogInfosName.Add("Map Sensor Volt"); DatalogInfosTxt.Add("0BBh");
                DatalogInfosName.Add("PA Sensor"); DatalogInfosTxt.Add("0BCh");
                DatalogInfosName.Add("Some PA/Map Value"); DatalogInfosTxt.Add("0BEh");
                DatalogInfosName.Add("Adjusted Map Sensor Volt??"); DatalogInfosTxt.Add("0BFh");

                DatalogInfosName.Add("RPM Sensor"); DatalogInfosTxt.Add("0C4h");
                DatalogInfosName.Add("RPM Related"); DatalogInfosTxt.Add("0C5h");
                DatalogInfosName.Add("VSS Sensor"); DatalogInfosTxt.Add("0CCh");

                DatalogInfosName.Add("Current TPS"); DatalogInfosTxt.Add("0D1h");
                DatalogInfosName.Add("TPS Sensor"); DatalogInfosTxt.Add("0D4h");
                DatalogInfosName.Add("TPS Delta"); DatalogInfosTxt.Add("0D5h");
                DatalogInfosName.Add("Sensor Input into ADCR5"); DatalogInfosTxt.Add("0D7h");
                DatalogInfosName.Add("IAT Sensor"); DatalogInfosTxt.Add("0D8h");
                DatalogInfosName.Add("ECT Sensor"); DatalogInfosTxt.Add("0D9h");
                DatalogInfosName.Add("O2 Sensor"); DatalogInfosTxt.Add("0DAh");
                DatalogInfosName.Add("IACV Sensor"); DatalogInfosTxt.Add("0DBh");

                //##########################################################################

                //		(IgnitionCut = 03B1.2)(FuelCut = 03B1.4 || 03B1.5)
                DatalogInfosName.Add("PostFuel"); DatalogInfosTxt.Add("003B1h).0");
                DatalogInfosName.Add("SCC"); DatalogInfosTxt.Add("003B1h).1");
                DatalogInfosName.Add("Ignition Cut"); DatalogInfosTxt.Add("003B1h).2");
                DatalogInfosName.Add("VTSM"); DatalogInfosTxt.Add("003B1h).3");
                DatalogInfosName.Add("Fuel Cut #1"); DatalogInfosTxt.Add("003B1h).4");
                DatalogInfosName.Add("Fuel Cut #2"); DatalogInfosTxt.Add("003B1h).5");
                DatalogInfosName.Add("InAT Shift #1"); DatalogInfosTxt.Add("003B1h).6");
                DatalogInfosName.Add("InAT Shift #2"); DatalogInfosTxt.Add("003B1h).7");

                DatalogInfosName.Add("ParkN"); DatalogInfosTxt.Add("003B0h).0");
                DatalogInfosName.Add("Brake Switch"); DatalogInfosTxt.Add("003B0h).1");
                DatalogInfosName.Add("ACC Switch"); DatalogInfosTxt.Add("003B0h).2");
                DatalogInfosName.Add("VTP"); DatalogInfosTxt.Add("003B0h).3");
                DatalogInfosName.Add("Start"); DatalogInfosTxt.Add("003B0h).4");
                DatalogInfosName.Add("SCC"); DatalogInfosTxt.Add("003B0h).5");
                DatalogInfosName.Add("VTS Feedback"); DatalogInfosTxt.Add("003B0h).6");
                DatalogInfosName.Add("PSP"); DatalogInfosTxt.Add("003B0h).7");

                DatalogInfosName.Add("Fuel Pump"); DatalogInfosTxt.Add("003B2h).0");
                DatalogInfosName.Add("IAB"); DatalogInfosTxt.Add("003B2h).2");
                DatalogInfosName.Add("Fan Control"); DatalogInfosTxt.Add("003B2h).4");
                DatalogInfosName.Add("AtlCtrl"); DatalogInfosTxt.Add("003B2h).5");
                DatalogInfosName.Add("Purge"); DatalogInfosTxt.Add("003B2h).6");
                DatalogInfosName.Add("AC"); DatalogInfosTxt.Add("003B2h).7");

                DatalogInfosName.Add("MIL"); DatalogInfosTxt.Add("003B3h).5");
                DatalogInfosName.Add("O2 Heater"); DatalogInfosTxt.Add("003B3h).6");
                DatalogInfosName.Add("VTEC"); DatalogInfosTxt.Add("003B3h).7");

                DatalogInfosName.Add("FTL Launch"); DatalogInfosTxt.Add("00408h).0");
                DatalogInfosName.Add("FTS"); DatalogInfosTxt.Add("00408h).1");
                DatalogInfosName.Add("EBC"); DatalogInfosTxt.Add("00408h).2");
                DatalogInfosName.Add("EBC Hi"); DatalogInfosTxt.Add("00408h).3");
                DatalogInfosName.Add("GPO1"); DatalogInfosTxt.Add("00408h).4");
                DatalogInfosName.Add("GPO2"); DatalogInfosTxt.Add("00408h).5");
                DatalogInfosName.Add("GPO3"); DatalogInfosTxt.Add("00408h).6");
                DatalogInfosName.Add("BST"); DatalogInfosTxt.Add("00408h).7");

                DatalogInfosName.Add("FTL (Launch)"); DatalogInfosTxt.Add("00409h).0");
                DatalogInfosName.Add("Antilag"); DatalogInfosTxt.Add("00409h).1");
                DatalogInfosName.Add("FTS"); DatalogInfosTxt.Add("00409h).2");
                DatalogInfosName.Add("Boost Cut"); DatalogInfosTxt.Add("00409h).3");
                DatalogInfosName.Add("EBC"); DatalogInfosTxt.Add("00409h).4");
                DatalogInfosName.Add("Secondary Map"); DatalogInfosTxt.Add("00409h).5");
                DatalogInfosName.Add("Fan Control"); DatalogInfosTxt.Add("00409h).6");
                DatalogInfosName.Add("VTEC"); DatalogInfosTxt.Add("00409h).7");

                DatalogInfosName.Add("GPO1"); DatalogInfosTxt.Add("00410h).0");
                DatalogInfosName.Add("GPO2"); DatalogInfosTxt.Add("00410h).1");
                DatalogInfosName.Add("GPO3"); DatalogInfosTxt.Add("00410h).2");
                DatalogInfosName.Add("BST Stage2"); DatalogInfosTxt.Add("00410h).3");
                DatalogInfosName.Add("BST Stage3"); DatalogInfosTxt.Add("00410h).4");
                DatalogInfosName.Add("BST Stage4"); DatalogInfosTxt.Add("00410h).5");
                DatalogInfosName.Add("Overheat Protect"); DatalogInfosTxt.Add("00410h).6");
                DatalogInfosName.Add("Lean Protect"); DatalogInfosTxt.Add("00410h).7");

                //##########################################################################

                DatalogInfosName.Add("IAT"); DatalogInfosTxt.Add("003CCh");
                DatalogInfosName.Add("ECU O2 Volt"); DatalogInfosTxt.Add("003CAh");
                DatalogInfosName.Add("TPS Volt"); DatalogInfosTxt.Add("003A4h");
                DatalogInfosName.Add("Inputs Options1"); DatalogInfosTxt.Add("003B1h");
                DatalogInfosName.Add("Columns Related"); DatalogInfosTxt.Add("001E7h");
                DatalogInfosName.Add("Rows Related"); DatalogInfosTxt.Add("001E8h");
                DatalogInfosName.Add("Col/Rows Related"); DatalogInfosTxt.Add("001DFh");
                DatalogInfosName.Add("MIL Byte1"); DatalogInfosTxt.Add("0011Ah"); DatalogInfosName.Add("MIL Byte1 Extra"); DatalogInfosTxt.Add("00212h");
                DatalogInfosName.Add("MIL Byte2"); DatalogInfosTxt.Add("0011Bh"); DatalogInfosName.Add("MIL Byte2 Extra"); DatalogInfosTxt.Add("00213h");
                DatalogInfosName.Add("MIL Byte3"); DatalogInfosTxt.Add("0011Ch"); DatalogInfosName.Add("MIL Byte3 Extra"); DatalogInfosTxt.Add("00214h");
                DatalogInfosName.Add("MIL Byte4"); DatalogInfosTxt.Add("0011Dh"); DatalogInfosName.Add("MIL Byte4 Extra"); DatalogInfosTxt.Add("00215h");
                DatalogInfosName.Add("Injector Value"); DatalogInfosTxt.Add("0019Eh");
                DatalogInfosName.Add("Ignition Final"); DatalogInfosTxt.Add("0035Bh");
                DatalogInfosName.Add("Ignition Table"); DatalogInfosTxt.Add("00246h");
                DatalogInfosName.Add("Inputs Options2"); DatalogInfosTxt.Add("003B0h");
                DatalogInfosName.Add("Outputs Options1"); DatalogInfosTxt.Add("003B2h");
                DatalogInfosName.Add("Outputs Options2"); DatalogInfosTxt.Add("003B3h");
                DatalogInfosName.Add("ELD Volt"); DatalogInfosTxt.Add("003D2h");
                DatalogInfosName.Add("Batt Volt"); DatalogInfosTxt.Add("003D1h");
                DatalogInfosName.Add("ECT FC"); DatalogInfosTxt.Add("00168h");
                DatalogInfosName.Add("O2 short"); DatalogInfosTxt.Add("00158h");
                DatalogInfosName.Add("O2 long"); DatalogInfosTxt.Add("00304h");
                DatalogInfosName.Add("IAT FC"); DatalogInfosTxt.Add("0015Ch");
                DatalogInfosName.Add("VE FC"); DatalogInfosTxt.Add("00162h");
                DatalogInfosName.Add("ECT IC"); DatalogInfosTxt.Add("00412h");
                DatalogInfosName.Add("IAT IC"); DatalogInfosTxt.Add("00252h");
                DatalogInfosName.Add("Gear IC"); DatalogInfosTxt.Add("00253h");
                DatalogInfosName.Add("Gear FC"); DatalogInfosTxt.Add("0023Bh");
                DatalogInfosName.Add("Inputs Options3"); DatalogInfosTxt.Add("00408h");
                DatalogInfosName.Add("Options Active"); DatalogInfosTxt.Add("00409h");
                DatalogInfosName.Add("EBC Baseduty"); DatalogInfosTxt.Add("0041Bh");
                DatalogInfosName.Add("EBC Duty"); DatalogInfosTxt.Add("0041Ch");
                DatalogInfosName.Add("EBC Target"); DatalogInfosTxt.Add("0041Ah");
                DatalogInfosName.Add("Options Outputs Active"); DatalogInfosTxt.Add("00410h");
                DatalogInfosName.Add("EGR Volt"); DatalogInfosTxt.Add("067h");
                DatalogInfosName.Add("B6 Volt"); DatalogInfosTxt.Add("003D5h");
                DatalogInfosName.Add("IACV Duty"); DatalogInfosTxt.Add("00382h");
            }

            for (int i = 0; i < DatalogInfosTxt.Count; i++)
            {
                if (ThisLine.Contains(DatalogInfosTxt[i])
                    || ThisLine.Contains(DatalogInfosTxt[i].ToLower())
                    || ThisLine.Contains(DatalogInfosTxt[i].ToUpper()))
                {
                    string ErrTxt = "";
                    if (DatalogInfosName[i].Contains("MIL Byte")) ErrTxt = ApplyErrorCodeDesc(ThisLine);
                    return "\t###\tROM INFO=" + DatalogInfosName[i] + "\t" + ErrTxt;
                }
            }

            return "";
        }

        private string ApplyErrorCodeDesc(string ThisLine)
        {
            int CodeNumber = 0;
            //Console.WriteLine(ThisLine);
            if (ThisLine.Contains("0011ah).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("0011ah).") + 8, 1)) + 1;
            if (ThisLine.Contains("0011bh).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("0011bh).") + 8, 1)) + 1 + 8;
            if (ThisLine.Contains("0011ch).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("0011ch).") + 8, 1)) + 1 + 8 + 8;
            if (ThisLine.Contains("0011dh).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("0011dh).") + 8, 1)) + 1 + 8 + 8 + 8;

            if (ThisLine.Contains("00212h).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("00212h).") + 8, 1)) + 1;
            if (ThisLine.Contains("00213h).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("00213h).") + 8, 1)) + 1 + 8;
            if (ThisLine.Contains("00214h).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("00214h).") + 8, 1)) + 1 + 8 + 8;
            if (ThisLine.Contains("00215h).")) CodeNumber = int.Parse(ThisLine.Substring(ThisLine.IndexOf("00215h).") + 8, 1)) + 1 + 8 + 8 + 8;

            string SetResetText = "";
            if (ThisLine.Contains("JBS")) SetResetText = "Set ";
            if (ThisLine.Contains("JBR")) SetResetText = "Reset ";

            return SetResetText + "CEL Code" + this.method_3_GetErrorCode(CodeNumber) + "\t" + this.method_4_GetErrorText(this.method_3_GetErrorCode(CodeNumber));
        }

        private int method_3_GetErrorCode(int int_0)
        {
            switch (int_0)
            {
                case 24:
                    return 30;

                case 25:
                    return 31;

                case 26:
                    return 36;

                case 27:
                    return 41;

                case 28:
                    return 43;

                case 29:
                    return 45;
            }
            if (int_0 > 29)
            {
                throw new Exception("Bitlocation did not convert to errorcode");
            }
            return int_0;
        }

        private string method_4_GetErrorText(int int_0)
        {
            switch (int_0)
            {
                case 1:
                    return "O2A - Oxygen Sensor #1";

                case 2:
                    return "O2B - Oxygen Sensor #2";

                case 3:
                    return "MAP - Manifold Absolute Pressure Sensor";

                case 4:
                    return "CKP - Crank Position Sensor";

                case 5:
                    return "MAP - Manifold Absolute Pressure Sensor";

                case 6:
                    return "ECT - Engine Coolant Sensor";

                case 7:
                    return "TPS - Throttle Position Sensor";

                case 8:
                    return "TDC - Top Dead Center Sensor";

                case 9:
                    return "CYP - Cylinder/Cam Sensor";

                case 10:
                    return "IAT - Intake Air Temperature Sensor";

                case 12:
                    return "EGR - Exhaust Gas Recirculation Lift Valve";

                case 13:
                    return "BARO - Atmospheric Pressure Sensor";

                case 14:
                    return "IAC - Idle Air Control Valve";

                case 15:
                    return "ICM - Ignition Control Module";

                case 16:
                    return "Fuel Injectors";

                case 17:
                    return "VSS - Vehicule Speed Sensor";

                case 19:
                    return "Automatic Transmission Lockup Control Valve";

                case 20:
                    return "ELD - Electrical Load Detector";

                case 21:
                    return "VTS - VTEC Solenoid";

                case 22:
                    return "VTP - VTEC Pressure Valve";

                case 23:
                    return "Knock Sensor";

                case 30:
                    return "Automatic Transmission A Signal";

                case 31:
                    return "Automatic Transmission B Signal";

                case 36:
                    return "Traction Control";

                case 41:
                    return "PO2H - Primary Oxygen Sensor Heater";

                case 43:
                    return "Fuel Supply System";

                case 45:
                    return "Fuel System too Rich or Lean";
            }
            return "Unknow error";
        }


        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //#####################################################################################################################################################
        //ALL FUNCTIONS BELLOW ARE FUNCTION OF FREE UP SPACE

        public bool IsFreeSpaceModded()
        {
            bool IsFreeModded = false;

            if (File.Exists(FilePath))
            {
                AllLines = File.ReadAllLines(FilePath);

                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Contains("ORIGINAL TABLE")
                        //|| AllLines[i].Contains("TOTAL FREE BYTES")
                        || AllLines[i].Contains("SINGLE WORD")
                        || AllLines[i].Contains("SINGLE ARRAY")
                        || AllLines[i].Contains("PARAM FROM")) IsFreeModded = true;
                }
            }

            return IsFreeModded;
        }

        public void FreeUpSpace()
        {
            /*tbl_620e:       DB  000h ; 620E
             tbl_620f:       DB  000h,045h,000h ; 620F*/

            //tbl_5f2f

            FreeBytesAmount = 0;

            //Only Free up the space if its not done yet
            if (!IsFreeSpaceModded())
            {
                //Reset Location bug, if its not already free up make sure the last table is set at 'tbl_5ef3'
                if (txtLastTable.Text != LastTableBCK)
                {
                    if (MessageBox.Show("Last Table aren't set to '" + LastTableBCK + "'\nDo you want to set it to '" + LastTableBCK + "'?", "Honda Binary", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                        txtLastTable.Text = LastTableBCK;
                }

                if (File.Exists(FilePath))
                {
                    LogThis("-----------------------------------------------------");
                    LogThis("CREATING FREE SPACE...");
                    AllLines = File.ReadAllLines(FilePath);

                    for (int i = AllLines.Length - 1; i > 0; i--)
                    {
                        //Console.WriteLine("here index=" + i + ", Line=" + AllLines[i]);

                        //Check for all single byte parameters
                        if (chk_SingleByte.Checked)
                        {
                            if (AllLines[i].Contains(":") && !AllLines[i].Contains(",") && AllLines[i].Contains("DB"))
                            {
                                Method = "Byte";

                                //split 'tbl_620e' and the rest
                                string[] SearchSplit = AllLines[i].Split(':');
                                if (SearchSplit[1].Contains(";"))
                                {
                                    //this replace 'DB  000h ; 620E' to --> 000h (byte)
                                    SearchSplit[1] = SearchSplit[1].Replace("\t", "");
                                    SearchSplit[1] = SearchSplit[1].Replace(" ", "");
                                    SearchSplit[1] = SearchSplit[1].Replace("DB", "");

                                    string[] SearchByteSplit = SearchSplit[1].Split(';');

                                    //Search 'tbl_620e' and byte '000h'
                                    SearchAndReplaceByte(SearchSplit[0], SearchByteSplit[0].Substring(0, SearchByteSplit[0].Length - 1).ToUpper() + "h");
                                }
                            }
                        }

                        if (chk_Word.Checked)
                        {
                            //Check for DW (word bytes array, this is 2bytes)
                            //we are cheking if the word array is used as a single byte (the 2nd byte are never used)
                            if (AllLines[i].Contains(":") && AllLines[i].Contains("DW"))
                            {
                                Method = "Word";

                                //split 'tbl_5f43' and the rest
                                string[] SearchSplit = AllLines[i].Split(':');
                                if (SearchSplit[1].Contains(";"))
                                {
                                    //tbl_6101: DW  int_timer_0_overflow; 6101 8922
                                    //tbl_6107: DW  label_19cb       ; 6107 CB19

                                    //this replace 'DW  0ff00h ; 5F44' to --> 000h (byte) ... the byte ff is not in count
                                    if (!SearchSplit[1].Contains("_") && SearchSplit[1].Contains("h") && SearchSplit[1].Contains("0"))
                                    {
                                        SearchSplit[1] = SearchSplit[1].Replace("\t", "");
                                        SearchSplit[1] = SearchSplit[1].Replace(" ", "");
                                        SearchSplit[1] = SearchSplit[1].Replace("DW", "");

                                        //0ff00h
                                        string[] SearchByteSplit = SearchSplit[1].Split(';');

                                        //Search 'tbl_5f43' and byte '000h'
                                        SearchAndReplaceByte(SearchSplit[0], "0" + SearchByteSplit[0].Substring(3, 2).ToUpper() + "h");
                                    }
                                    //this replace 'DW  int_timer_0_overflow; 6101 8922' to --> 089h (byte) ... the byte 22 is not in count
                                    if (SearchSplit[1].Contains("_") && !SearchSplit[1].Contains("h"))
                                    {
                                        //8922
                                        string[] SplitLine = SearchSplit[1].Split(';');
                                        string LocationsDecs = SplitLine[1];
                                        if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);
                                        string[] SplitDescLine = LocationsDecs.Split(' ');

                                        //Search 'tbl_5f43' and byte '000h'
                                        //Console.WriteLine("Search=" + SearchSplit[0] + "\t\tByte=" + "0" + SplitDescLine[1].Substring(0, 2) + "h");
                                        SearchAndReplaceByte(SearchSplit[0], "0" + SplitDescLine[1].Substring(0, 2).ToUpper() + "h");
                                    }
                                }
                            }
                        }

                        if (chk_Array.Checked)
                        {
                            //Check for bytes array with comma
                            //we are cheking if the byte array is used as a single byte (the 2nd bytes and more are never used)
                            if (AllLines[i].Contains(":") && AllLines[i].Contains(",") && AllLines[i].Contains("DB"))
                            {
                                Method = "Array";
                                //Console.WriteLine(AllLines[i]);

                                //split 'tbl_5fa8' and the rest
                                string[] SearchSplit = AllLines[i].Split(':');
                                if (SearchSplit[1].Contains(";"))
                                {
                                    //this replace 'DB  0E7h,0FDh,02Ch,070h ; 5FA8' to --> 0E7h (byte)
                                    SearchSplit[1] = SearchSplit[1].Replace("\t", "");
                                    SearchSplit[1] = SearchSplit[1].Replace(" ", "");
                                    SearchSplit[1] = SearchSplit[1].Replace("DB", "");

                                    //extract '0E7h,0FDh,02Ch,070h'
                                    string[] SearchByteSplit = SearchSplit[1].Split(';');

                                    //extract '0E7h'
                                    string[] SearchByteSplitIn = SearchByteSplit[0].Split(',');

                                    //Search 'tbl_5fa8' and byte '0E7h'
                                    //Console.WriteLine("Search=" + SearchSplit[0] + "\t\tByte=" + SearchByteSplitIn[0]);
                                    SearchAndReplaceByte(SearchSplit[0], SearchByteSplitIn[0].Substring(0, SearchByteSplitIn[0].Length - 1).ToUpper() + "h");
                                }
                            }
                        }

                        if (AllLines[i].Contains(txtLastTable.Text)) i = 0;
                    }

                    //Set Locations/Relocate Unused
                    SetTableLocations();
                    if (chk_RelocUnused.Checked) RelocateUnusedTables();

                    //Add all free bytes
                    AddFreeBytes(true, false);

                    //File.Create(FilePath).Dispose();
                    //File.WriteAllLines(FilePath, AllLines);
                }
            }
        }

        private void SetTableLocations()
        {
            for (int i = AllLines.Length - 1; i > 0; i--)
            {
                if (AllLines[i].Contains("tbl_") && AllLines[i].Contains(":"))
                {
                    //split 'tbl_5f43' and the rest
                    string[] SearchSplit = AllLines[i].Split(':');
                    AllLines[i] += "\t\tORIGINAL TABLE=" + SearchSplit[0];
                }

                //if (AllLines[i].Contains(txtLastTable.Text)) i = 0;
            }

            //Save Changes
            File.Create(FilePath).Dispose();
            File.WriteAllLines(FilePath, AllLines);
        }

        private void RelocateUnusedTables()
        {
            LogThis("-----------------------------------------------------");
            LogThis("RELOCATING UNUSED BYTES TO FREE SPACE");
            //FreeBytesAmount = 0;
            List<int> LinesUnused = new List<int>();
            int LinesUnusedCount = 0;

            //Get Free Bytes Amount and Lines that wont be used anymore
            for (int i = AllLines.Length - 1; i > 0; i--)
            {
                if (AllLines[i].Contains("RELOCATED AT="))
                {
                    //remove single byte
                    if (!AllLines[i].Contains("SINGLE WORD") && !AllLines[i].Contains("SINGLE ARRAY"))
                    {
                        FreeBytesAmount++;
                        LinesUnused.Add(i);
                        LinesUnusedCount++;
                    }
                    //remove single word (2bytes)
                    if (AllLines[i].Contains("SINGLE WORD"))
                    {
                        FreeBytesAmount++;
                        FreeBytesAmount++;
                        LinesUnused.Add(i);
                        LinesUnusedCount++;
                    }
                    //remove single array (get bytes count)
                    if (AllLines[i].Contains("SINGLE ARRAY"))
                    {
                        FreeBytesAmount += GetBytesCountFromArray(AllLines[i]);
                        LinesUnused.Add(i);
                        LinesUnusedCount++;
                    }
                }

                if (AllLines[i].Contains(txtLastTable.Text)) i = 0;
            }

            //Remake All Lines
            string[] AllLines_Buff = AllLines;
            AllLines = new string[AllLines_Buff.Length - LinesUnusedCount];
            int DoingCurrentLine = 0;
            for (int i = 0; i < AllLines_Buff.Length; i++)
            {
                if (CanDoThisLine(i, LinesUnused))
                {
                    AllLines[DoingCurrentLine] = AllLines_Buff[i];
                    DoingCurrentLine++;
                }
            }
        }

        private bool CanDoThisLine(int ThisLine, List<int> LinesUnused)
        {
            bool CanDo = true;
            for (int i = 0; i < LinesUnused.Count; i++) if (LinesUnused[i] == ThisLine) CanDo = false;

            return CanDo;
        }

        private int GetBytesCountFromArray(string ThisLine)
        {
            //This get the bytes count in a byte array
            //exemple this return the number 4 from '0E7h,0FDh,02Ch,070h'
            if (ThisLine.Contains("DB") && ThisLine.Contains(","))
            {
                ThisLine = ThisLine.Replace("\t", "");
                ThisLine = ThisLine.Replace(" ", "");
                ThisLine = ThisLine.Replace("DB", "");

                //extract '0E7h,0FDh,02Ch,070h'
                string[] SearchByteSplit = ThisLine.Split(';');

                //extract '0E7h'
                string[] SearchByteSplitIn = SearchByteSplit[0].Split(',');

                return SearchByteSplitIn.Length;
            }
            return 0;
        }

        private void AddFreeBytes(bool AddingDesc, bool RemovingBytes)
        {
            if (FreeBytesAmount > 0)
            {
                LogThis("-----------------------------------------------------");
                if (!RemovingBytes) LogThis("CREATING " + FreeBytesAmount + " FREE BYTES OF SPACE");
                else LogThis("REMOVING " + FreeBytesAmount + " BYTES");
                List<string> LinesList = new List<string>();

                ResetLastTable();

                //store all main program line to the list
                int ParamStartLocation = 0;
                int CurrentFound = 0;
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Contains(txtLastTable.Text) && CurrentFound == LastTableFoundCount - 1)
                    {
                        ParamStartLocation = i;
                        i = AllLines.Length;
                    }

                    if (i != AllLines.Length)
                    {
                        if (AllLines[i].Contains(txtLastTable.Text)) CurrentFound++;
                        //Console.WriteLine(AllLines[i]);
                        LinesList.Add(AllLines[i]);
                        //Console.WriteLine(LinesList[LinesList.Count - 1]);
                    }
                }

                //store free bytes between main program (ending point) and parameter region
                int FreeLineCount = (int)(FreeBytesAmount / 8);
                int LastLineCount = FreeBytesAmount;
                if (FreeLineCount >= 1) LastLineCount = FreeBytesAmount - (FreeLineCount * 8);

                if (LastLineCount > 0) FreeLineCount++;

                //Console.WriteLine("Adding Free Bytes= " + FreeBytesAmount + "\tLine Count=" + FreeLineCount + "\tLast Line Count=" + LastLineCount + "\tStart at=" + ParamStartLocation);
                for (int i = 0; i < FreeLineCount; i++)
                {
                    if (i != FreeLineCount - 1)
                    {
                        if (!RemovingBytes)
                        {
                            LinesList.Add(CreateFreeByteString(8));
                            //Console.WriteLine(LinesList[LinesList.Count - 1]);
                        }
                        else LinesList.RemoveAt(LinesList.Count - 1);    //removing 8 bytes
                    }
                    else
                    {
                        if (LastLineCount != 0)
                        {
                            if (!RemovingBytes)
                            {
                                string strrrgrgLine = CreateFreeByteString(LastLineCount);
                                if (AddingDesc) strrrgrgLine += "\tTOTAL FREE BYTES=" + FreeBytesAmount;
                                LinesList.Add(strrrgrgLine);
                            }
                            else
                            {
                                LinesList.RemoveAt(LinesList.Count - 1);    //removing 8 bytes but remaking remaining
                                LinesList.Add(CreateFreeByteString(LastLineCount) + "     ");
                                //Console.WriteLine(LinesList[LinesList.Count - 1]);
                            }
                        }
                        else if (AddingDesc && !RemovingBytes) LinesList.Add("\t\t\t\t\t;      \tTOTAL FREE BYTES=" + FreeBytesAmount);
                    }
                }

                //store all parameters line to the list
                for (int i = ParamStartLocation; i < AllLines.Length; i++) LinesList.Add(AllLines[i]);

                //Remake list into string array
                AllLines = new string[LinesList.Count];
                for (int i = 0; i < AllLines.Length; i++) AllLines[i] = LinesList[i];

                File.Create(FilePath).Dispose();
                File.WriteAllLines(FilePath, AllLines);
            }
        }

        private string CreateFreeByteString(int AmountOfFreeBytes)
        {
            string FBytes = "                DB  ";
            for (int i = 0; i < AmountOfFreeBytes; i++)
            {
                if (i != 0) FBytes += ",";
                FBytes += "0FFh";
            }
            FBytes += " ; ";
            return FBytes;
        }

        private void SearchAndReplaceByte(string SearchLocation, string SearchByte)
        {
            /*label_1843:     LB      A, #0ffh               ; 4126 0 208 180 7700
            /*label_1843:     LB      A, #0ffh               ; 4126 0 208 180 7700
                              ;LCB     A, tbl_5f41            ; 1843 0 100 280 909D415F
            */
            //SearchLocation Exemple: tbl_5f41
            //SearchByte Exemple: 0ffh
            //LogThis("REPLACING TABLE: '" + SearchLocation + "' BYTE: '" + SearchByte + "' CODE: 'LCB     A, " + SearchLocation + "' --> 'LB      A, #" + SearchByte + "'");


            string AppliedLocations = "\t\tTABLE=" + SearchLocation + "\t\tRELOCATED AT=";
            int CurrentFound = 0;       //CurrentFound != (LastTableFoundCount - 1)
            int DoneCount = 0;

            int UsedCount = 0;

            //Check for count (dont do double used locations)
            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains(SearchLocation))
                {
                    if (AllLines[i].Contains("LCB")
                        && AllLines[i].Contains("A")
                        && AllLines[i].Contains(",")
                        && !AllLines[i].Contains("[")
                        && !AllLines[i].Contains("]"))
                    {
                        UsedCount++;

                        //Dont mind about the 1st found result
                        if (AllLines[i].Contains(txtLastTable.Text))
                        {
                            //if (CurrentFound != LastTableFoundCount - 1) CurrentFound++;
                            //else i = AllLines.Length;
                            if (CurrentFound == LastTableFoundCount - 1) i = AllLines.Length;
                            else CurrentFound++;
                        }
                    }
                }
            }

            CurrentFound = 0;

            //dont do double used locations (only one used location)
            if (UsedCount == 1)
            {
                //Change all within the baserom main program region
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Contains(SearchLocation))
                    {
                        if (AllLines[i].Contains("LCB")
                            && AllLines[i].Contains("A")
                            && AllLines[i].Contains(",")
                            && !AllLines[i].Contains("[")
                            && !AllLines[i].Contains("]"))
                        {
                            AllLines[i] = AllLines[i].Replace("LCB", "LB ");

                            int TestLenght = GetDescHEXLenght(AllLines[i]);
                            if (TestLenght != 8) MessageBox.Show("ERROR IN LENGHT AT LINE NUMBER= " + i + "\tLENGHT IS=" + TestLenght);

                            string NewLine = AllLines[i].Substring(0, AllLines[i].IndexOf(SearchLocation));
                            NewLine += "#" + SearchByte.ToLower() + "   ";
                            NewLine += AllLines[i].Substring(AllLines[i].LastIndexOf(SearchLocation) + 8);

                            AllLines[i] = NewLine;

                            //Console.WriteLine("Line=" + AllLines[i]);

                            if (DoneCount > 0) AppliedLocations += ", ";
                            AppliedLocations += GetAppliedLocation(AllLines[i]);

                            AllLines[i] = SetDescHEX(AllLines[i], SearchByte);
                            AllLines[i] += "\t\tPARAM FROM=" + SearchLocation;
                            //AllLines[i] += "\t\tRELOCATED AT=" + GetAppliedLocation(AllLines[i]);

                            LogThis("REPLACED TABLE AT LINE: " + i + " (" + SearchLocation + ")");
                            DoneCount++;
                            FreeBytesAmount += 2;
                        }

                        //Dont mind about the 1st found result
                        if (AllLines[i].Contains(txtLastTable.Text))
                        {
                            //if (CurrentFound != LastTableFoundCount - 2) CurrentFound++;
                            //else i = AllLines.Length;
                            if (CurrentFound == LastTableFoundCount - 1) i = AllLines.Length;
                            else CurrentFound++;
                        }
                    }
                }

                //Add others method infos
                if (Method == "Word") AppliedLocations += "\t\t**SINGLE WORD**";
                if (Method == "Array") AppliedLocations += "\t\t**SINGLE ARRAY**";

                //Add all location applied to the parameters region
                for (int i = AllLines.Length - 1; i > 0; i--)
                {
                    if (AllLines[i].Contains(SearchLocation))
                    {
                        string RemadeLine = "                DB  " + SearchByte + " ; ";
                        if (Method == "Word") RemadeLine = "                DB  " + SearchByte + ",0FFh ; ";
                        if (Method == "Array")
                        {
                            //Console.WriteLine(AllLines[i]);
                            //separe 'tbl_5f41' and other infos
                            string[] SearchSplit = AllLines[i].Split(':');
                            RemadeLine = "         " + SearchSplit[1];

                            //add '**SINGLE ARRAY**' to all line bottom this one that was from this array
                            int CheckLine = i + 1;
                            while (!AllLines[CheckLine].Contains("tbl_")
                                && !AllLines[CheckLine].Contains("TABLE=")
                                && !AllLines[CheckLine].Contains("RELOCATED AT="))
                            {
                                AllLines[CheckLine] += AppliedLocations;
                                CheckLine++;
                            }
                        }

                        //make the line
                        AllLines[i] = RemadeLine;
                        AllLines[i] += AppliedLocations;
                    }
                    if (AllLines[i].Contains(txtLastTable.Text)) i = 0;
                }
            }

            //Set Desc for those that are used more than once but its a single byte used (not a word or array)
            if (UsedCount > 1)
            {
                if (Method == "Word" || Method == "Array")
                {
                    AppliedLocations = "";
                    if (Method == "Word") AppliedLocations += "\t\t**SINGLE WORD BUT USED MORE THAN ONCE**";
                    if (Method == "Array") AppliedLocations += "\t\t**SINGLE ARRAY BUT USED MORE THAN ONCE**";

                    for (int i = AllLines.Length - 1; i > 0; i--)
                    {
                        if (AllLines[i].Contains(SearchLocation)) AllLines[i] += AppliedLocations;
                        if (AllLines[i].Contains(txtLastTable.Text)) i = 0;
                    }
                }
            }
        }

        private string GetAppliedLocation(string ThisLine)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 909D415F
            //this return 1843

            if (ThisLine.Contains(";"))
            {
                string[] SplitLine = ThisLine.Split(';');
                string LocationsDecs = SplitLine[1];

                if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);

                if (LocationsDecs.Contains(" "))
                {
                    string[] SplitDescLine = LocationsDecs.Split(' ');
                    return SplitDescLine[0].Substring(0,4);
                }
            }
            return "";
        }

        private string SetDescHEX(string ThisLine, string SearchByte)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 909D415F
            //This reset '909D415F' into '77FF'

            string NewLine = "";
            if (ThisLine.Contains(";"))
            {
                string[] SplitLine = ThisLine.Split(';');
                string LocationsDecs = SplitLine[1];

                if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);

                if (LocationsDecs.Contains(" "))
                {
                    string TNewByte = SearchByte.Substring(1);
                    TNewByte = TNewByte.Replace("h", "");

                    string[] SplitDescLine = LocationsDecs.Split(' ');
                    NewLine += SplitLine[0];
                    NewLine += "; ";
                    NewLine += SplitDescLine[0] + " ";
                    NewLine += SplitDescLine[1] + " ";
                    NewLine += SplitDescLine[2] + " ";
                    NewLine += SplitDescLine[3] + " ";
                    NewLine += "77" + TNewByte;

                    if (SplitDescLine.Length >= 6)
                    {
                        for (int i = 0; i < SplitDescLine.Length - 5; i++) NewLine += SplitDescLine[5 + i];
                    }
                }
            }
            return NewLine;
        }

        private int GetDescHEXLenght(string ThisLine)
        {
            //label_1843:     LB     A, #0FFh      ; 1843 0 100 280 909D415F
            //This get the lenght of '909D415F'

            if (ThisLine.Contains(";"))
            {
                string[] SplitLine = ThisLine.Split(';');
                string LocationsDecs = SplitLine[1];

                if (LocationsDecs[0] == ' ') LocationsDecs = LocationsDecs.Substring(1);

                if (LocationsDecs.Contains(" "))
                {
                    string[] SplitDescLine = LocationsDecs.Split(' ');
                    if (SplitDescLine.Length >= 5)
                    {
                        return SplitDescLine[4].Length;
                    }
                }
            }
            return 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            txtLastTable.Text = button2.Text;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            txtLastTable.Text = button3.Text;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            FilePath = textBox1.Text;
            if (Path.GetExtension(textBox1.Text) == ".bin" || Path.GetExtension(textBox1.Text) == ".asm")
            {
                if (Path.GetExtension(textBox1.Text) == ".bin")
                {
                    //Copy BIN (CREATE BACKUP)
                    File.Create(FilePath).Dispose();
                    File.WriteAllBytes(FilePath, File.ReadAllBytes(Application.StartupPath + @"\LastBIN_BACKUP.bin"));
                    LogThis("BACKUP LOADED '" + Path.GetFileName(FilePath) + "'");
                }
                if (Path.GetExtension(textBox1.Text) == ".asm")
                {
                    //Copy ASM (CREATE BACKUP)
                    File.Create(FilePath).Dispose();
                    File.WriteAllBytes(FilePath, File.ReadAllBytes(Application.StartupPath + @"\LastASM_BACKUP.asm"));
                    LogThis("BACKUP LOADED '" + Path.GetFileName(FilePath) + "'");
                }
            }
        }

        private void Chk_Do111_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Enabled = chk_Do111.Checked;
        }

        private void Chk_FreeSpace_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = chk_FreeSpace.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilePath = textBox1.Text;
            if (FilePath.Contains(".asm"))
            {
                SetExtrasDatalogInfosInASM();
                LogThis("-----------------------------------------------------");
                LogThis("DONE");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void chk_Word_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
