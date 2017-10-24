using System.Windows.Forms;

namespace Els_kom_Test_Mods_Stub.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

#warning This Application is deprecated and will be removed in future versions.
        void MainForm_Load(object sender, System.EventArgs e)
		{
            Els_kom_Core.Classes.ExecutionManager.RunElswordDirectly();
            //System.Threading.Thread tr3 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordDirectly);
            //tr3.Start();
        }
    }
}
