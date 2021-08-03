using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CreateFolder
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        public string userName = System.Environment.UserName;
        private string tempDir = System.Environment.GetEnvironmentVariable("TEMP");
        private string mariadb = string.Empty;
        private string currPath = System.IO.Directory.GetCurrentDirectory();
        private Dictionary<string, List<string>> allDict = new Dictionary<string, List<string>>();
        private string ftrackIni = @"M:/Users/TD/config/Ftrack.ini";
        private string locPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\.OrcenRise";
        private string pythonBin;
        public string LocPy()
        {
            string locPython = "D:\\Python27\\python.exe";
            if (File.Exists(locPython))
                pythonBin = locPython;
            else
                pythonBin = "M:\\Users\\TD\\Netless\\production\\studio\\tools\\Python27\\python.exe";
            return pythonBin;
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            LocPy();
            this.Text = "Create Directorys Hierarchy From CSV File of Project " + Application.ProductVersion.ToString();
            
        }
      

        /// <summary>
        /// 加载根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadBTN_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (DialogResult.OK == fbd.ShowDialog())
            {
                rootCBB.Text = fbd.SelectedPath.ToString();
                bool hasRoot=false;
                for (int i = 0; i < rootCBB.Items.Count; i++)
                {
                    if(rootCBB.Items[i].ToString()==fbd.SelectedPath.ToString())
                    {
                        hasRoot = true;
                        break;
                    }
                }

                if (!hasRoot)
                   rootCBB.Items.Add(fbd.SelectedPath.ToString());
            }

        }

        private void add0BTN_Click(object sender, EventArgs e)
        {
            if (add0TB.Text != "")
            {
                foreach(ListViewItem item in shotTaskLV.Items)
                {
                    if(item.Text.ToString()==add0TB.Text.ToString())
                    {
                        MessageBox.Show("The Name is Already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                shotTaskLV.Items.Add(add0TB.Text.ToString());
            }
        }

        private void remove0BTN_Click(object sender, EventArgs e)
        {
            if(shotTaskLV.SelectedItems.Count>0)
            {
                DialogResult result = MessageBox.Show("The Items Will be Deleted!\r\nAre You Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result==DialogResult.Yes)
                {
                    foreach(ListViewItem item in shotTaskLV.Items)
                    {
                        foreach(ListViewItem selItem in shotTaskLV.SelectedItems)
                            shotTaskLV.Items.Remove(selItem);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select A Item,Please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void selall0BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in shotTaskLV.Items)
            {
                item.Checked = true;
            }
        }

        private void selin0BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in shotTaskLV.Items)
            {
                item.Checked = !(item.Checked);
            }
        }

        private void add1BTN_Click(object sender, EventArgs e)
        {
            if (add1TB.Text != "")
            {
                foreach (ListViewItem item in shotFolderLV.Items)
                {
                    if (item.Text.ToString() == add1TB.Text.ToString())
                    {
                        MessageBox.Show("The Name is Already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                shotFolderLV.Items.Add(add1TB.Text.ToString());
            }
        }

        private void remove1BTN_Click(object sender, EventArgs e)
        {
            if (shotFolderLV.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("The Items Will be Deleted!\r\nAre You Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in shotFolderLV.Items)
                    {
                        foreach (ListViewItem selItem in shotFolderLV.SelectedItems)
                            shotFolderLV.Items.Remove(selItem);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select A Item,Please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void selall1BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in shotFolderLV.Items)
            {
                item.Checked = true;
            }
        }

        private void selin1BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in shotFolderLV.Items)
            {
                item.Checked = !(item.Checked);
            }
        }

        private void add2BTN_Click(object sender, EventArgs e)
        {
            if (add2TB.Text != "")
            {
                foreach (ListViewItem item in astTaskLV.Items)
                {
                    if (item.Text.ToString() == add2TB.Text.ToString())
                    {
                        MessageBox.Show("The Name is Already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                astTaskLV.Items.Add(add2TB.Text.ToString());
            }
        }

        private void remove2BTN_Click(object sender, EventArgs e)
        {
            if (astTaskLV.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("The Items Will be Deleted!\r\nAre You Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in astTaskLV.Items)
                    {
                        foreach (ListViewItem selItem in astTaskLV.SelectedItems)
                            astTaskLV.Items.Remove(selItem);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select A Item,Please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void selall2BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in astTaskLV.Items)
            {
                item.Checked = true;
            }
        }

        private void selin2BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in astTaskLV.Items)
            {
                item.Checked = !(item.Checked);
            }
        }
        private void add3BTN_Click(object sender, EventArgs e)
        {
            if (add3TB.Text != "")
            {
                foreach (ListViewItem item in astFolderLV.Items)
                {
                    if (item.Text.ToString() == add3TB.Text.ToString())
                    {
                        MessageBox.Show("The Name is Already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                astFolderLV.Items.Add(add3TB.Text.ToString());
            }
        }
        private void remove3BTN_Click(object sender, EventArgs e)
        {
            if (astFolderLV.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("The Items Will be Deleted!\r\nAre You Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in astFolderLV.Items)
                    {
                        foreach (ListViewItem selItem in astFolderLV.SelectedItems)
                            astFolderLV.Items.Remove(selItem);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select A Item,Please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void selall3BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in astFolderLV.Items)
            {
                item.Checked = true;
            }
        }

        private void selin3BTN_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in astFolderLV.Items)
            {
                item.Checked = !(item.Checked);
            }
        }
        /// <summary>
        /// 选择一个project,只显示它的所有层级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void projLB_SelectedValueChanged(object sender, EventArgs e)
        {
            if(allDict!=null && allDict.Count>0)
            {
                seqLB.Items.Clear();
                shotLB.Items.Clear();
                astNameLB.Items.Clear();

                foreach (string item in allDict["Episode"])
                {
                    if (item.Split('>')[0].ToString() == projectMTB.Text.ToString())
                        epiLB.Items.Add(item);
                }
                foreach (string item in allDict["Sequence"])
                {
                    if(item.Split('>')[0].ToString()==projectMTB.Text.ToString())
                        seqLB.Items.Add(item);
                }
                foreach (string item in allDict["Shot"])
                {
                    if (item.Split('>')[0].ToString() == projectMTB.Text.ToString())
                        shotLB.Items.Add(item);
                }
                foreach (string item in allDict["Asset"])
                {
                    if (item.Split('>')[0].ToString() == projectMTB.Text.ToString())
                        astNameLB.Items.Add(item);
                }
            }
        }
        /// <summary>
        /// 打开帮助窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutBTN_Click(object sender, EventArgs e)
        {
            AboutBox abtFrm = new AboutBox();
            abtFrm.ShowDialog();
        }
        /// <summary>
        /// 导入Json到界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shotImportBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json File(*.json)|*.json";
            ofd.Title = "Import a Json file";
            DialogResult result=ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string jsnFile = ofd.FileName;
                string jsnTxt = File.ReadAllText(jsnFile);
                try
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dictionary<string, List<string>>));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsnTxt));
                    Dictionary<string, List<string>> jsonObject = (Dictionary<string, List<string>>)ser.ReadObject(ms);
                    allDict["Projects"] = jsonObject["Projects"];
                    allDict["Episode"] = jsonObject["Episode"];
                    allDict["Sequence"] = jsonObject["Sequence"];
                    allDict["Shot"] = jsonObject["Shot"];
                    allDict["Asset"] = jsonObject["Asset"];
                    allDict["shotTask"] = jsonObject["shotTask"];
                    allDict["astTask"] = jsonObject["astTask"];
                    allDict["shotFolder"] = jsonObject["shotFolder"];
                    allDict["astFolder"] = jsonObject["astFolder"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()+"\r\nSelect a Vaild Json File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (allDict != null && allDict.Count > 0)
                {
                    seqLB.Items.Clear();
                    shotLB.Items.Clear();
                    astNameLB.Items.Clear();
                    shotTaskLV.Items.Clear();
                    astTaskLV.Items.Clear();
                    shotFolderLV.Items.Clear();
                    astFolderLV.Items.Clear();
                    if (allDict["Projects"][0]!=null)
                    {
                        projectMTB.Text = allDict["Projects"][0];
                    }
                    foreach (string item in allDict["Episode"])
                    {
                        epiLB.Items.Add(allDict["Episode"]);
                    }
                    foreach (string item in allDict["Sequence"])
                    {
                        seqLB.Items.Add(item);
                    }
                    foreach (string item in allDict["Shot"])
                    {
                        shotLB.Items.Add(item);
                    }
                    foreach (string item in allDict["Asset"])
                    {
                        astNameLB.Items.Add(item);
                    }
                    foreach (string info in allDict["shotTask"])
                    {
                        ListViewItem item=new ListViewItem();
                        item.Text=info.Split('&')[0];
                        if (info.Split('&')[1] == "True")
                            item.Checked = true;
                        else
                            item.Checked = false;
                        shotTaskLV.Items.Add(item);
                    }
                    foreach (string info in allDict["astTask"])
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = info.Split('&')[0];
                        if (info.Split('&')[1] == "True")
                            item.Checked = true;
                        else
                            item.Checked = false;
                        astTaskLV.Items.Add(item);
                    }
                    foreach (string info in allDict["shotFolder"])
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = info.Split('&')[0];
                        if (info.Split('&')[1] == "True")
                            item.Checked = true;
                        else
                            item.Checked = false;
                        shotFolderLV.Items.Add(item);
                    }
                    foreach (string info in allDict["astFolder"])
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = info.Split('&')[0];
                        if (info.Split('&')[1] == "True")
                            item.Checked = true;
                        else
                            item.Checked = false;
                        astFolderLV.Items.Add(item);
                    }
                    
                }
                else 
                {
                    MessageBox.Show("Select a Vaild Json File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void allDictEx()
        {
            List<string> projL = new List<string>();
            List<string> epL = new List<string>();
            List<string> seqL = new List<string>();
            List<string> shotL = new List<string>();
            List<string> astNameL = new List<string>();
            List<string> shotTaskL = new List<string>();
            List<string> shotFolderL = new List<string>();
            List<string> astTaskL = new List<string>();
            List<string> astFolderL = new List<string>();
            if (projectMTB.Text != string.Empty || projectMTB.Text != null)
            {
                projL.Add(projectMTB.Text.ToString());
            }
            allDict["Projects"] = projL;
            foreach (string item in epiLB.Items)
            {
                epL.Add(item);
            }
            allDict["Episode"] = epL;
            foreach (string item in seqLB.Items)
            {
                seqL.Add(item);
            }
            allDict["Sequence"] = seqL;
            foreach (string item in shotLB.Items)
            {
                shotL.Add(item);
            }
            allDict["Shot"] = shotL;
            foreach (string item in astNameLB.Items)
            {
                astNameL.Add(item);
            }
            allDict["Asset"] = astNameL;
            foreach (ListViewItem item in shotTaskLV.Items)
            {
                shotTaskL.Add(item.Text.ToString() + "&" + item.Checked.ToString());
            }
            allDict["shotTask"] = shotTaskL;
            foreach (ListViewItem item in astTaskLV.Items)
            {
                astTaskL.Add(item.Text.ToString() + "&" + item.Checked.ToString());
            }
            allDict["astTask"] = astTaskL;
            foreach (ListViewItem item in shotFolderLV.Items)
            {
                shotFolderL.Add(item.Text.ToString() + "&" + item.Checked.ToString());
            }
            allDict["shotFolder"] = shotFolderL;
            foreach (ListViewItem item in astFolderLV.Items)
            {
                astFolderL.Add(item.Text.ToString() + "&" + item.Checked.ToString());
            }
            allDict["astFolder"] = astFolderL;
        }
        /// <summary>
        /// 导出界面上的List到Json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shotExportBTN_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json File(*.json)|*.json";
            sfd.Title = "Export a Json file";
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                allDictEx();

                string jsnFile = sfd.FileName;
                string jsonTxt = "";
                if (allDict != null && allDict.Count > 0)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dictionary<string, List<string>>));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ser.WriteObject(ms, allDict);
                        jsonTxt = Encoding.Default.GetString(ms.ToArray());
                    }
                    File.WriteAllText(jsnFile,jsonTxt);
                }
                else
                {
                    MessageBox.Show("Open a Vaild Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 创建shot的目录层级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shotCreateBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if(!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(allDict==null || allDict.Count<1)
            {
                //MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Import Project Data From a csv File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null || projectMTB.Text==string.Empty )
            {
                MessageBox.Show("Input A Project Name,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> createDirL = new List<string>();
            createDirL.Add(rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString());
            string shotWork = rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\shot";
            createDirL.Add(shotWork);
            foreach (string item in epiLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add(shotWork + "\\" + item.Split('>')[3]);
            }
            foreach(string item in seqLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add(shotWork + "\\" + item.Split('>')[3] + "\\" + item.Split('>')[4]);
            }
            foreach (string item in shotLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add(shotWork + "\\" + item.Split('>')[3] + "\\" + item.Split('>')[4] + "\\" + item.Split('>')[5]);
                foreach (ListViewItem titm in shotTaskLV.Items)
                {
                    if (titm.Checked)
                    {
                        createDirL.Add(shotWork + "\\" + item.Split('>')[3] + "\\" + item.Split('>')[4] + "\\" + item.Split('>')[5] + "\\" + titm.Text.ToString());
                        foreach(ListViewItem vitm in shotFolderLV.Items)
                        {
                            if(vitm.Checked)
                            {
                                createDirL.Add(shotWork + "\\" + item.Split('>')[3] + "\\" + item.Split('>')[4] + "\\" + item.Split('>')[5] + "\\" + titm.Text.ToString() + "\\" + vitm.Text.ToString());
                            }
                        }
                    }
                }
            }
           
            CreateAllDir(createDirL.ToArray());
        }
        private void CreateAllDir(string[] dirA)
        {
            int good = 0;
            int bad = 0;
            foreach (string path in dirA)
            {
                string zpath = path.Replace(">", "/");
                if (!Directory.Exists(zpath))
                {
                    try
                    {
                        Directory.CreateDirectory(zpath);
                        good++;
                    }
                    catch (Exception ex)
                    {
                        DialogResult result= MessageBox.Show(ex.ToString() + "\r\nCreate Directory Failed! Escape?\r\n" + zpath, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        bad++;
                        if (result == DialogResult.Yes)
                            return;
                    }
                }
            }
            MessageBox.Show( "Create Directory: " + good.ToString()+"\r\nFailed: "+bad.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void DelSpecDir(string[] dirA)
        {
            int good = 0;
            int bad = 0;
            foreach (string path in dirA)
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        DeleteFolderAll(path);
                        Directory.Delete(path);
                        good++;
                    }
                    catch (Exception ex)
                    {
                        DialogResult result = MessageBox.Show(ex.ToString() + "\r\nDelete Directory Failed! Escape?\r\n" + path, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        bad++;
                        if (result == DialogResult.Yes)
                            return;
                    }
                }
            }
            MessageBox.Show("Delete Directory: " + good.ToString() + "\r\nFailed: " + bad.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
        }
        public static void DeleteFolderAll(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                    DeleteFolder(d);////递归删除子文件夹
                Directory.Delete(d);
            }
        }
        private void exitBTN_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 创建assets目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void astCreateBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (allDict == null || allDict.Count < 1)
            {
                MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null||projectMTB.Text==string.Empty)
            {
                MessageBox.Show("Input A Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> createDirL = new List<string>();
            createDirL.Add(rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString());
            string astWork = rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\asset";
            createDirL.Add(astWork);
            
            foreach (string item in astTypeLB.Items)
            {
                createDirL.Add(astWork + "\\" + item);
                for (int i = 0; i < astNameLB.Items.Count; i++)
                {
                    string[] astNameL = astNameLB.Items[i].ToString().Split('>');
                    if (item == astNameL[3])
                    {
                        string astName = astWork + "\\" + item + "\\" + astNameL[astNameL.Count() - 1];
                        createDirL.Add(astName);
                        foreach (ListViewItem titm in astTaskLV.Items)
                        {
                            if (titm.Checked)
                            {
                                createDirL.Add(astName + "\\" + titm.Text.ToString());
                                foreach (ListViewItem vitm in astFolderLV.Items)
                                {
                                    if (vitm.Checked)
                                    {
                                        createDirL.Add(astName + "\\" + titm.Text.ToString() + "\\" + vitm.Text.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
           

            CreateAllDir(createDirL.ToArray());
        }
        /// <summary>
        /// 删除未勾选的 shot_work下的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shotDelBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (allDict == null || allDict.Count < 1)
            {
                MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null)
            {
                MessageBox.Show("Input A Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> delDirL = new List<string>();
            string shotWork = rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\shot" ;
            foreach (string item in shotLB.Items)
            {
                foreach (ListViewItem titm in shotTaskLV.Items)
                {
                    if (!titm.Checked)
                    {
                        delDirL.Add(shotWork + "\\" + item.Split('>')[1] + "\\" + item.Split('>')[2] + "\\" + item.Split('>')[3] + "\\" + titm.Text.ToString());
                    }

                    foreach (ListViewItem vitm in shotFolderLV.Items)
                    {
                        if (!vitm.Checked)
                        {
                            delDirL.Add(shotWork + "\\" + item.Split('>')[1] + "\\" + item.Split('>')[2] + "\\" + item.Split('>')[3] + "\\" + titm.Text.ToString() + "\\" + vitm.Text.ToString());
                        }
                    }
                        
                }
            }

            DelSpecDir(delDirL.ToArray());
        }
        /// <summary>
        /// 删除未勾选的 asset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void astDelBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (allDict == null || allDict.Count < 1)
            {
                MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null)
            {
                MessageBox.Show("Input A Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> delDirL = new List<string>();
            string astWork = rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\asset";

            foreach (string item in astTypeLB.Items)
            {
                delDirL.Add(astWork+"\\"+item);
                for (int i = 0; i < astNameLB.Items.Count; i++)
                {
                    string[] astNameL=astNameLB.Items[i].ToString().Split('>');
                    if (item == astNameL[1])
                    {
                        string astName = astWork + "\\" + astNameL[astNameL.Count() - 1];
                        delDirL.Add(astName);
                        foreach (ListViewItem titm in astTaskLV.Items)
                        {
                            if (!titm.Checked)
                            {
                                delDirL.Add(astName + "\\" + titm.Text.ToString());
                            }
                            foreach (ListViewItem vitm in astFolderLV.Items)
                            {
                                if (!vitm.Checked)
                                {
                                    delDirL.Add(astName + "\\" + titm.Text.ToString() + "\\" + vitm.Text.ToString());
                                }
                            }
                        }
                    }
                }
            }
            DelSpecDir(delDirL.ToArray());
        }

        private void shotPvzBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (allDict == null || allDict.Count < 1)
            {
                MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null || projectMTB.Text==string.Empty)
            {
                MessageBox.Show("Input A Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> createDirL = new List<string>();
            string shotWork = "=>" + rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\shot" ;
            createDirL.Add(shotWork);
            foreach (string item in epiLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add("->" + item.Split('>')[3]);
            }
            foreach (string item in seqLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add("->" + item.Split('>')[4]);
            }
            foreach (string item in shotLB.Items)
            {
                if (item.Split('>')[0] == projectMTB.Text.ToString())
                    createDirL.Add("->" + item.Split('>')[3] +"\\"+ item.Split('>')[4] + "\\" + item.Split('>')[5]);
                foreach (ListViewItem titm in shotTaskLV.Items)
                {
                    if (titm.Checked)
                    {
                        createDirL.Add("->" + item.Split('>')[3] +"\\"+ item.Split('>')[4] + "\\" + item.Split('>')[5] + "\\" + titm.Text.ToString());
                        foreach (ListViewItem vitm in shotFolderLV.Items)
                        {
                            if (vitm.Checked)
                            {
                                createDirL.Add("->" + item.Split('>')[3] +"\\"+ item.Split('>')[4] + "\\" + item.Split('>')[5] + "\\" + titm.Text.ToString() + "\\" + vitm.Text.ToString());
                            }
                        }
                    }
                }
            }
            if (!Directory.Exists(locPath))
                Directory.CreateDirectory(locPath);
            File.WriteAllLines(locPath + "\\createFolderShotTmp.inf", createDirL);
            runNote(locPath + "\\createFolderShotTmp.inf");
        }

        private void runNote(string file)
        {
            Process p = new Process();
            string args = file;
            p.StartInfo.Arguments = args;
            p.StartInfo.FileName = "notepad";//要执行的程序名称
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;//可能接受来自调用程序的输入信息 
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息 
            p.StartInfo.CreateNoWindow = false;//不显示程序窗口 
            //p.StartInfo.Arguments = cmdAll;//“/C”表示执行完命令后马上退出
            p.Start();//启动程序
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();

        }

        private void astPvzBTN_Click(object sender, EventArgs e)
        {
            string rootPath = rootCBB.Text.ToString();
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("Set a Vaild Root Path for Project,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (allDict == null || allDict.Count < 1)
            {
                MessageBox.Show("Connect to MariaDB,or Import Project Data From a Json File,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (projectMTB.Text == null || projectMTB.Text==string.Empty)
            {
                MessageBox.Show("Input A Project Name,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> createDirL = new List<string>();
            string astWork = "=>" + rootPath.TrimEnd('/') + "\\" + projectMTB.Text.ToString() + "\\production\\asset" ;
            createDirL.Add(astWork);

            foreach (string item in astTypeLB.Items)
            {
                createDirL.Add("->" + item);
                for (int i = 0; i < astNameLB.Items.Count; i++)
                {
                    string[] astNameL = astNameLB.Items[i].ToString().Split('>');
                    if (astNameL.Count() < 2)
                    {
                        MessageBox.Show("Need More Asset Name,First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; 
                    }
                    if (item == astNameL[3])
                    {
                        string astName = astWork + "\\" + item + "\\" + astNameL[astNameL.Count() - 1];
                        createDirL.Add(astName);
                        foreach (ListViewItem titm in astTaskLV.Items)
                        {
                            if (titm.Checked)
                            {
                                createDirL.Add("->" + item + "\\" + astNameL[astNameL.Count() - 1] +"\\"+ titm.Text.ToString());
                                foreach (ListViewItem vitm in astFolderLV.Items)
                                {
                                    if (vitm.Checked)
                                    {
                                        createDirL.Add("->" + item + "\\" +astNameL[astNameL.Count() - 1]+"\\"+ titm.Text.ToString() + "\\" + vitm.Text.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!Directory.Exists(locPath))
                Directory.CreateDirectory(locPath);
            File.WriteAllLines(locPath + "\\createFolderAstTmp.inf", createDirL);
            runNote(locPath + "\\createFolderAstTmp.inf");
        }

        /// <summary>
        /// import a csv file to list all sequence and shot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shotCsvBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
             ofd.Filter = "CSV File(*.csv)|*.csv";
            ofd.Title = "Import a CSV file";
            DialogResult result=ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string csvFile = ofd.FileName;
                try
                {
                    FileStream fs = new FileStream(csvFile, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    string proj = projectMTB.Text.ToString();
                    //string episode = "ep"+Convert.ToInt32(epNUD.Value).ToString("D3");
                    allDict["Projects"] = new List<string>{};
                    List<string> epL = new List<string>();
                    List<string> seqL = new List<string>();
                    List<string> shotL = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        string lineStr = sr.ReadLine();
                        lineStr = lineStr.Trim();
                        if (lineStr == null || lineStr.Length == 0)
                            continue;
                        string[] values = lineStr.Split(',');
                        if (!epL.Contains(proj + ">production>shot>"+values[0]))
                        {
                            epL.Add(proj + ">production>shot>" + values[0]);
                        }
                        if (!seqL.Contains(proj + ">production>shot>" + values[0] + ">" + values[1]))
                        {
                            seqL.Add(proj + ">production>shot>" + values[0] + ">" + values[1]);
                        }
                        if (!shotL.Contains(proj + ">production>shot>" + values[0] + ">" + values[1] + ">" + values[2]))
                        {
                            shotL.Add(proj + ">production>shot>" + values[0] + ">" + values[1] + ">" + values[2]);
                        }

                    }
                    fs.Close();
                    allDict["Sequence"] = seqL;
                    allDict["Shot"] = shotL;
                    if (allDict != null && allDict.Count > 0)
                    {
                        seqLB.Items.Clear();
                        shotLB.Items.Clear();
                        foreach (string item in allDict["Sequence"])
                        {
                            seqLB.Items.Add(item);
                        }
                        foreach (string item in allDict["Shot"])
                        {
                            shotLB.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\r\nSelect a Vaild CSV File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
        /// <summary>
        /// import a csv file to list all assets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assetCsvBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV File(*.csv)|*.csv";
            ofd.Title = "Import a CSV file";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string csvFile = ofd.FileName;
                try
                {
                    FileStream fs = new FileStream(csvFile, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    string proj = projectMTB.Text.ToString();
                    allDict["Projects"] = new List<string> { };
                    List<string> typeL = new List<string>();
                    List<string> assetL = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        string lineStr = sr.ReadLine();
                        lineStr = lineStr.Trim();
                        if (lineStr == null || lineStr.Length == 0)
                            continue;
                        string[] values = lineStr.Split(',');
                        if (!typeL.Contains(values[0]))
                        {
                            typeL.Add(values[0]);
                        }
                        if (!assetL.Contains(proj + ">production>asset>" + values[0] + ">" + values[1]))
                        {
                            assetL.Add(proj + ">production>asset>" + values[0] + ">" + values[1]);
                        }

                    }
                    fs.Close();
                    allDict["AssetType"] = typeL;
                    allDict["Asset"] = assetL;
                    if (allDict != null && allDict.Count > 0)
                    {
                        astTypeLB.Items.Clear();
                        astNameLB.Items.Clear();
                        foreach (string item in allDict["AssetType"])
                        {
                            astTypeLB.Items.Add(item);
                        }
                        foreach (string item in allDict["Asset"])
                        {
                            astNameLB.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\r\nSelect a Vaild CSV File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
        /// <summary>
        /// 从ftrack读取已经建立好的层级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ftkInfoBTN_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> argDict = new Dictionary<string, string>();
            if (!File.Exists(ftrackIni))
            {
                MessageBox.Show("Error: Ftrack config file is not exists!\n" + ftrackIni, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] ftrackTxtL = File.ReadAllLines(ftrackIni);
            List<string> keyL=new List<string>{"[URL]","[KEY]","[USR]"};
            foreach (string line in ftrackTxtL)
            {
                int firstIndex = line.IndexOf("]")+1;
                foreach (string key in keyL)
                {
                    string head = line.Substring(0, firstIndex);
                    if (head == key)
                    {
                        argDict.Add(key, line.Replace(key, ""));
                        break;
                    }
                }

            }
            
            argDict.Add("pyFile", currPath + "/getDir.py");
            argDict.Add("astJson",tempDir + "/allAssetDir.json");
            argDict.Add("shtJson", tempDir + "/allShotDir.json");
            if (!runPyCmd(argDict))
            {
                MessageBox.Show("Error#100 :连接ftrack服务器失败，程序将自动退出！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                loadFtrackJson(argDict["astJson"], argDict["shtJson"]);
                allDictEx();
            }
            return;
        }

        private void clearLV()
        {
            seqLB.Items.Clear();
            shotLB.Items.Clear();
            astNameLB.Items.Clear();
            astTypeLB.Items.Clear();
            astTaskLV.Clear();
            shotTaskLV.Clear();
            epiLB.Items.Clear();
        }
        

        private void loadFtrackJson(string astjsonfile,string shtjsonfile)
        {
            if (!File.Exists(astjsonfile) || !File.Exists(shtjsonfile))
            {
                MessageBox.Show("Error: Ftrack Json file is not exists!\n" + astjsonfile+"\n"+shtjsonfile, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clearLV();
            string jsnTxt = File.ReadAllText(astjsonfile);
            string projName = projectMTB.Text.ToString();
            //read asset json
            try
            {
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(jsnTxt);
                List<string> kindL=new List<string>();
                List<string> assetL=new List<string>();
                if (jsonObject != null)
                {

                    foreach (var obj0 in jsonObject)
                    {
                        kindL.Add(obj0.Key);
                        astTypeLB.Items.Add(obj0.Key);

                        string myvalue = obj0.Value.ToString();
                        if (myvalue == "")
                            continue;
                        JObject objVal = (JObject)obj0.Value;
                        foreach (var obj1 in objVal)
                        {
                            assetL.Add(obj0.Key + ">" + obj1.Key);
                            astNameLB.Items.Add(projName + ">production>asset>"+ obj0.Key + ">" + obj1.Key);
                            if (obj1.Value.ToString() == "")
                                continue;
                            JArray objTsk = (JArray)obj1.Value;
                            foreach (JValue tsk in objTsk)
                            {
                                string task = tsk.ToString();
                                List<string> astTasklvL = getastlv();
                                if (!astTasklvL.Contains(task))
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.Text = task;
                                    item.Checked = true;
                                    astTaskLV.Items.Add(item);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\r\nSelect a Vaild Asset Json File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // read shot json
            jsnTxt = File.ReadAllText(shtjsonfile);
            try
            {
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(jsnTxt);
                List<string> epL = new List<string>();
                List<string> seqL = new List<string>();
                List<string> shtL = new List<string>();
                if (jsonObject != null)
                {
                    foreach (var obj0 in jsonObject)
                    {
                        epL.Add(obj0.Key);
                        epiLB.Items.Add(projName + ">production>shot>" +obj0.Key);
                        string myvalue = obj0.Value.ToString();
                        if (myvalue == "")
                            continue;
                        JObject objVal = (JObject)obj0.Value;
                        foreach (var obj1 in objVal)
                        {
                            seqL.Add(obj0.Key + ">" + obj1.Key);
                            seqLB.Items.Add(projName + ">production>shot>" + obj0.Key + ">" + obj1.Key);
                            myvalue = obj1.Value.ToString();
                            if (myvalue == "")
                                continue;
                            JObject shtVal = (JObject)obj1.Value;
                            foreach (var obj2 in shtVal)
                            {
                                shtL.Add(obj0.Key + ">" + obj1.Key+">"+obj2.Key);
                                shotLB.Items.Add(projName + ">production>shot>" + obj0.Key + ">" + obj1.Key + ">" + obj2.Key);
                                if (obj2.Value.ToString() == "")
                                    continue;
                                JArray objTsk = (JArray)obj2.Value;
                                foreach (JValue tsk in objTsk)
                                {
                                    string task = tsk.ToString();
                                    List<string> shotTasklvL = getshtlv();
                                    if (!shotTasklvL.Contains(task))
                                    {
                                        ListViewItem item = new ListViewItem();
                                        item.Text = task;
                                        item.Checked = true;
                                        shotTaskLV.Items.Add(item);
                                    }
                                }

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\r\nSelect a Vaild Shot Json File,Please!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> getshtlv()
        {
            List<string> shtTasklvL = new List<string>();
            foreach (ListViewItem item in shotTaskLV.Items)
            {
                shtTasklvL.Add(item.Text);
            }
            return shtTasklvL;
        }
        private List<string> getastlv()
        {
            List<string> astTasklvL=new List<string>();
             foreach (ListViewItem item in astTaskLV.Items)
             {
                    astTasklvL.Add(item.Text);
              }
            return astTasklvL;
        }
        private bool runPyCmd(Dictionary<string, string> argDict)
        {
            pythonBin = LocPy();
            //string taskJson = locPath + "/taskJsn_" + userName + ".json";
            string[] dirJsonL = new string[] { argDict["astJson"], argDict["shtJson"] };
            foreach (string dirJson in dirJsonL)
            {
                if (File.Exists(dirJson))
                    File.Delete(dirJson);
            }
            string pyFile = argDict["pyFile"];
            string projName = projectMTB.Text.ToString();
            Process p = new Process();

            string args = " " + pyFile + " " + argDict["[URL]"] + " " + argDict["[KEY]"] + " " + argDict["[USR]"] + " " + projName ;
            //MessageBox.Show(args);
            p.StartInfo.Arguments = args;
            //MessageBox.Show(pythonBin);
            p.StartInfo.FileName = pythonBin;//要执行的程序名称
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;//可能接受来自调用程序的输入信息 
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息 
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口 
            //p.StartInfo.Arguments = cmdAll;//“/C”表示执行完命令后马上退出
            p.Start();//启动程序
            string strRst = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            string[] outTxtL = strRst.Split('\n');
            foreach (string outTxt in outTxtL)
            {
                if (outTxt.Contains(">>>Information: Export Task "))
                {
                    //MessageBox.Show(outTxt);
                    return true;
                }
            }
            return false;
        }
    }
}
