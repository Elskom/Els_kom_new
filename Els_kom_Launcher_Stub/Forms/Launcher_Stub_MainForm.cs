using System.Windows.Forms;

namespace Els_kom_Launcher_Stub.Forms
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
            Els_kom_Core.Classes.ExecutionManager.RunElswordLauncher();
            //System.Threading.Thread tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher);
            //tr4.Start();
        }
    }
}
