using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Toolbar.UserControll
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class UCToolbar : UserControl
    {
        public UCToolbar()
        {
            InitializeComponent();
            selectfolders(@"C:\Users\Akuma\source\repos\Toolbar\Toolbar\UserControll");
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tcAll.SelectedIndex.ToString().Equals("3"))
            {
                Window yourParentWindow = Window.GetWindow(this);
                yourParentWindow.Close();
            }
            Button[] allBtns = new Button[16] { btn_programm_1, btn_programm_2, btn_programm_3, btn_programm_4, btn_programm_5, btn_programm_6, btn_programm_7, btn_programm_8, btn_games_1, btn_games_2, btn_games_3, btn_games_4, btn_games_5, btn_games_6, btn_games_7, btn_games_8};
            Image[] allImgs = new Image[16] { img_programm_1, img_programm_2, img_programm_3, img_programm_4, img_programm_5, img_programm_6, img_programm_7, img_programm_8, img_games_1, img_games_2, img_games_3, img_games_4, img_games_5, img_games_6, img_games_7, img_games_8 };

            for (int i = 0; i < allBtns.Length; i++)
            {
                SqlConection conn = new SqlConection(Properties.Resources.Connection);
                conn.Open();
                DataTable dt = conn.GetDataTable("SELECT pfad, img FROM TablePfade WHERE btn = '" + allBtns[i].Name + "'");
                foreach (DataRow row in dt.Rows)
                {
                    allBtns[i].ToolTip = row.ItemArray[0].ToString();
                    FileInfo fi = new FileInfo(@"C:\Users\Akuma\source\repos\Toolbar\Toolbar\UserControll\" + row.ItemArray[1].ToString());
                    if (fi.Exists)
                    {
                        allImgs[i].Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"C:\Users\Akuma\source\repos\Toolbar\Toolbar\UserControll\" + row.ItemArray[1].ToString());
                    }
                }
            }
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = openFileDialog.FileName;
        }
        public void selectfolders(string filename)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(filename);

            FileInfo[] info = dirInfo.GetFiles("*.*");
            foreach (FileInfo f in info)
            {
                ProgrammImg.Items.Add(f.Name);
                GameImg.Items.Add(f.Name);
            }


        }

        public void btn_programm_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var source = btn.ToolTip.ToString();
            Process.Start(source.Replace('/', '\\'));
        }

        private void CbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            String cbItem = cb.SelectedItem.ToString();
            String btnName = getBtnName(cbItem, cb);


            SqlConection conn = new SqlConection(Properties.Resources.Connection);
            conn.Open();
            DataTable dt = conn.GetDataTable("SELECT pfad, img FROM TablePfade WHERE btn = '" + btnName + "'");
            foreach (DataRow row in dt.Rows)
            {
                if (cb.Name.Equals("cb_Programm") && row.ItemArray[0] != null)
                {
                    ProgrammPfad.Text = row.ItemArray[0].ToString();
                    int exist = 0;
                    foreach (String cbi in ProgrammImg.Items)
                    {
                        if (cbi.Equals(row.ItemArray[1].ToString()))
                        {
                            exist++;
                        }
                    }
                    if (exist > 0)
                    {
                        ProgrammImg.Text = row.ItemArray[1].ToString();
                    }
                    else
                    {
                        ProgrammImg.Items.Add(row.ItemArray[1].ToString());
                        ProgrammImg.Text = row.ItemArray[1].ToString();
                    }

                }
                else if (cb.Name.Equals("cb_Games") && row.ItemArray[0] != null)
                {
                    GamePfad.Text = row.ItemArray[0].ToString(); int exist = 0;
                    foreach (String cbi in ProgrammImg.Items)
                    {
                        if (cbi.Equals(row.ItemArray[1].ToString()))
                        {
                            exist++;
                        }
                    }
                    if (exist > 0)
                    {
                        GameImg.Text = row.ItemArray[1].ToString();
                    }
                    else
                    {
                        GameImg.Items.Add(row.ItemArray[1].ToString());
                        GameImg.Text = row.ItemArray[1].ToString();
                    }
                }
            }
        }

        private string getBtnName(string cbItem, ComboBox cb)
        {
            string btnName = null;
            if (cb.Name.Equals("cb_Programm"))
            {
                if (cbItem.Contains("btn_1"))
                {
                    btnName = "btn_programm_1";
                }
                else if (cbItem.Contains("btn_2"))
                {
                    btnName = "btn_programm_2";
                }
                else if (cbItem.Contains("btn_3"))
                {
                    btnName = "btn_programm_3";
                }
                else if (cbItem.Contains("btn_4"))
                {
                    btnName = "btn_programm_4";
                }
                else if (cbItem.Contains("btn_5"))
                {
                    btnName = "btn_programm_5";
                }
                else if (cbItem.Contains("btn_6"))
                {
                    btnName = "btn_programm_6";
                }
                else if (cbItem.Contains("btn_7"))
                {
                    btnName = "btn_programm_7";
                }
                else if (cbItem.Contains("btn_8"))
                {
                    btnName = "btn_programm_8";
                }
            }
            else if (cb.Name.Equals("cb_Games"))
            {
                if (cbItem.Contains("btn_1"))
                {
                    btnName = "btn_games_1";
                }
                else if (cbItem.Contains("btn_2"))
                {
                    btnName = "btn_games_2";
                }
                else if (cbItem.Contains("btn_3"))
                {
                    btnName = "btn_games_3";
                }
                else if (cbItem.Contains("btn_4"))
                {
                    btnName = "btn_games_4";
                }
                else if (cbItem.Contains("btn_5"))
                {
                    btnName = "btn_games_5";
                }
                else if (cbItem.Contains("btn_6"))
                {
                    btnName = "btn_games_6";
                }
                else if (cbItem.Contains("btn_7"))
                {
                    btnName = "btn_games_7";
                }
                else if (cbItem.Contains("btn_8"))
                {
                    btnName = "btn_games_8";
                }
            }
            return btnName;
        }

        private void btnSafe_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            SqlConection conn = new SqlConection(Properties.Resources.Connection);
            conn.Open();
            if (btn.Name.Equals("safe_Programm"))
            {
                var source = ProgrammPfad.Text;
                String cbItem = cb_Programm.SelectedItem.ToString();
                String btnName = getBtnName(cbItem, cb_Programm);
                conn.SetData("UPDATE TablePfade SET pfad = '" + source.Replace('\\', '/') + "', img = '" + ProgrammImg.Text + "' WHERE btn = '" + btnName + "'");
            }
            else
            {
                var source = GamePfad.Text;
                String cbItem = cb_Games.SelectedItem.ToString();
                String btnName = getBtnName(cbItem, cb_Games);
                conn.SetData("UPDATE TablePfade SET pfad = '" + source.Replace('\\', '/') + "', img = '" + GameImg.Text + "' WHERE btn = '" + btnName + "'");
            }
        }

        private void btn_save_img_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Text != "")
            {
                string sourceFile = txtEditor.Text;
                string[] file = sourceFile.Split('\\');
                string destinationFile = @"C:\Users\Akuma\source\repos\Toolbar\Toolbar\UserControll\" + file[^1];

                // To move a file or folder to a new location:
                System.IO.File.Move(sourceFile, destinationFile);

            }
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            SqlConection conn = new SqlConection(Properties.Resources.Connection);
            conn.Open();
            if (btn.Name.Equals("delete_Programm"))
            {
                var source = ProgrammPfad.Text;
                String cbItem = cb_Programm.SelectedItem.ToString();
                String btnName = getBtnName(cbItem, cb_Programm);
                conn.SetData("UPDATE TablePfade SET pfad = '', img = '' WHERE btn = '" + btnName + "'");
            }
            else
            {
                var source = GamePfad.Text;
                String cbItem = cb_Games.SelectedItem.ToString();
                String btnName = getBtnName(cbItem, cb_Games);
                conn.SetData("UPDATE TablePfade SET pfad = '', img = '' WHERE btn = '" + btnName + "'");
            }
        }
    }
}
