using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ListView_TreeView_Directory_View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.TopNode.Nodes.Clear();
            // lấy ổ đĩa trên máy tính
            string[] drives = Environment.GetLogicalDrives();
            foreach (string var in drives)
            {
                // Thêm node vào treeview
                TreeNode node = new TreeNode(var);
                node.Text = var;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 2;

                treeView1.TopNode.Nodes.Add(node);
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Text == "My Computer") return;

                // thêm vào treeview
                e.Node.Nodes.Clear();
                string fullpath = e.Node.Text;
                TreeNode _node = e.Node.Parent;
                while (_node.Parent != null)
                {
                    if (_node.Parent.Text == "My Computer")
                    {
                        fullpath = _node.Text + fullpath;
                        break;
                    }
                    else
                        fullpath = _node.Text + "\\" + fullpath;

                    _node = _node.Parent;
                }

                string[] folders = Directory.GetDirectories(fullpath);
                foreach (string var in folders)
                {
                    string[] fs = var.Split('\\');
                    // Thêm node vào treeview
                    TreeNode node = new TreeNode(fs[fs.Length - 1]);
                    node.Text = fs[fs.Length - 1];
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;

                    e.Node.Nodes.Add(node);
                }

                // thêm vào view
                listView1.Items.Clear();
                string[] files = Directory.GetFiles(fullpath);
                //get jpg file
                label1.Text = "JPG: " + Directory.GetFiles(fullpath, "*.jpg", SearchOption.AllDirectories).Length.ToString();

                foreach (string var in files)
                {
                    // thêm vào view
                    ListViewItem item = new ListViewItem(Path.GetFileName(var), 0);
                    item.SubItems.Add(File.GetCreationTime(var).ToLongDateString());
                    string[] type = var.Split('.');
                    item.SubItems.Add(type[type.Length - 1] + " file type");

                    listView1.Items.Add(item);
                }
            }
            catch
            {
            }
        }
    }
}