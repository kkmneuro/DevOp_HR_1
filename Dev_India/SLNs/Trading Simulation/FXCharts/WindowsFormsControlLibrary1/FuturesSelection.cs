using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MarketData;


namespace SelectionControl
{
    public partial class FuturesSelection : UserControl
    {

        public Future Future { get; set; }

        public EventHandler SelectionChanged;

        public FuturesSelection()
        {
            InitializeComponent();
            treeView1.ExpandAll();

            TreeNode tn = treeView1.Nodes[0].Nodes[0].Nodes[0];
            treeView1.SelectedNode = tn; // treeView1.Nodes["Node21"];  // by default EUR/USD
            treeView2.ExpandAll();
            ChangeColorNodesRecursive(treeView2.Nodes[0]);
            treeView3.ExpandAll();
            ChangeColorNodesRecursive(treeView3.Nodes[0]);
            treeView4.ExpandAll();
            ChangeColorNodesRecursive(treeView4.Nodes[0]);
            treeView5.ExpandAll();
            ChangeColorNodesRecursive(treeView5.Nodes[0]);

            Future = new Future(treeView1.SelectedNode.Text);
        }


        public void ChangeColorNodesRecursive(TreeNode oParentNode)
        {

            oParentNode.BackColor = Color.Black;
            oParentNode.ForeColor = Color.White;
            // Start recursion on all subnodes.
            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                ChangeColorNodesRecursive(oSubNode);
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!treeView1.Nodes[0].Nodes[0].Nodes.Contains(e.Node))
            {
                TreeNode tn = treeView1.Nodes[0].Nodes[0].Nodes[0];
                treeView1.SelectedNode = tn; // treeView1.Nodes["Node21"];  // by default EUR/USD

            }

            Future = new Future(treeView1.SelectedNode.Text);
            if (SelectionChanged != null) SelectionChanged(sender, e);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        
        new public void Focus()
        {
            this.treeView1.Focus();
        }
    }
}
