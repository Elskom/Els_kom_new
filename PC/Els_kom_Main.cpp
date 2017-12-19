#include "Els_kom_Main_MainForm.hpp"

using namespace System;
using namespace System::Windows::Forms;

[STAThread]
int main() {
  Application::EnableVisualStyles();
  Application::SetCompatibleTextRenderingDefault(false);
  Application::Run(gcnew Els_kom::Forms::MainForm());
  return 0;
}
