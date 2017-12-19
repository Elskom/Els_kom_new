#include "LoadResources.hpp"
#include "resource.hpp"
#include "Els_kom_Main_MainForm.hpp"
#include "Els_kom_Main_AboutForm.hpp"
#include "Els_kom_Main_SettingsForm.hpp"

using namespace LoadResources;

enum class SYSCOMMANDS {
  WM_SYSCOMMAND = 0x0112,
  SC_MAXIMIZE = 0xf030,
  SC_MINIMIZE = 0xf020,
  SC_RESTORE = 0xf120,
};

namespace Els_kom {
  namespace Forms {
    MainForm::MainForm(void)
    {
      InitializeComponent();
      //
      //TODO: Add the constructor code here
      //
    }

    MainForm::~MainForm()
    {
      if (components)
      {
        delete components;
      }
    }

    void MainForm::WndProc(System::Windows::Forms::Message% m) {
      if (Enablehandlers && m.Msg == (int)SYSCOMMANDS::WM_SYSCOMMAND)
      {
        if (m.WParam.ToInt32() == (int)SYSCOMMANDS::SC_MINIMIZE)
        {
          this->Hide();
          m.Result = System::IntPtr::Zero;
          return;
        }
        else if (m.WParam.ToInt32() == (int)SYSCOMMANDS::SC_MAXIMIZE)
        {
          this->Show();
          this->Activate();
          m.Result = System::IntPtr::Zero;
          return;
        }
        else if (m.WParam.ToInt32() == (int)SYSCOMMANDS::SC_RESTORE)
        {
          this->Show();
          this->Activate();
          m.Result = System::IntPtr::Zero;
          return;
        }
      }
      Form::WndProc(m);
    }

    System::Void MainForm::MainForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) {
      bool Cancel = e->Cancel;
      // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
      e->Cancel = Cancel;
    }

    System::Void MainForm::MainForm_Load(System::Object^  sender, System::EventArgs^  e) {
      this->Hide();
      this->mainControl1->NotifyIcon1->Visible = false;
      this->ShowInTaskbar = false;
      bool previnstance;
      this->Icon = GetIconResource(IDI_MAINICON);
      this->mainControl1->NotifyIcon1->Icon = this->Icon;
      this->mainControl1->NotifyIcon1->Text = this->Text;
      previnstance = Els_kom_Core::Classes::ExecutionManager::IsElsKomRunning();
      if (Els_kom_Core::Classes::Version::version != L"1.4.9.8") {
        Els_kom_Core::Classes::MessageManager::ShowError(L"Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", L"Error!");
        this->Close();
      }
      if (previnstance == true)
      {
        Els_kom_Core::Classes::MessageManager::ShowError(L"Sorry, Only 1 Instance is allowed at a time.", L"Error!");
        this->Close();
      }
      else
      {
        this->mainControl1->LoadControl();
        this->Show();
      }
    }

    System::Void MainForm::MainForm_MouseLeave(System::Object^  sender, System::EventArgs^  e) {
      this->mainControl1->Label1->Text = L"";
    }

    System::Void MainForm::mainControl1_CloseForm(System::Object^  sender, System::EventArgs^  e) {
      if (aboutfrm != nullptr)
      {
        aboutfrm->Close();
      }
      if (settingsfrm != nullptr)
      {
        settingsfrm->Close();
      }
      this->Close();
    }

    System::Void MainForm::mainControl1_MinimizeForm(System::Object^  sender, System::EventArgs^  e) {
      if (!this->ShowInTaskbar)
      {
        this->Hide();
      }
      this->WindowState = System::Windows::Forms::FormWindowState::Minimized;
    }

    System::Void MainForm::mainControl1_TaskbarShow(System::Object^  sender, Els_kom_Core::Classes::ShowTaskbarEvent^  e) {
      if (e->value == L"0") // Taskbar only!!!
      {
        this->mainControl1->NotifyIcon1->Visible = false;
        this->ShowInTaskbar = true;
      }
      if (e->value == L"1") // Tray only!!!
      {
        this->mainControl1->NotifyIcon1->Visible = true;
        this->ShowInTaskbar = false;
      }
      if (e->value == L"2") // Both!!!
      {
        this->mainControl1->NotifyIcon1->Visible = true;
        this->ShowInTaskbar = true;
      }
      if (!this->ShowInTaskbar)
      {
        this->Enablehandlers = true;
      }
      else
      {
        this->Enablehandlers = false;
      }
    }

    System::Void MainForm::mainControl1_TrayNameChange(System::Object^  sender, System::EventArgs^  e) {
      this->mainControl1->NotifyIcon1->Text = this->Text;
    }

    System::Void MainForm::mainControl1_TrayClick(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {
      if (Els_kom::Forms::AboutForm::Label1 != nullptr && Els_kom::Forms::AboutForm::Label1->Text == L"1")
      {
        //I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
      }
      else
      {
        if (e->Button == System::Windows::Forms::MouseButtons::Left)
        {
          if (this->ShowInTaskbar == true)
          {
            if (this->WindowState == System::Windows::Forms::FormWindowState::Minimized)
            {
              this->WindowState = System::Windows::Forms::FormWindowState::Normal;
              this->Activate();
            }
            else
            {
              this->WindowState = System::Windows::Forms::FormWindowState::Minimized;
            }
          }
          else if (this->mainControl1->NotifyIcon1->Visible == true)
          {
            if (this->WindowState == System::Windows::Forms::FormWindowState::Minimized)
            {
              this->WindowState = System::Windows::Forms::FormWindowState::Normal;
              this->Show();
            }
            else
            {
              this->Hide();
              this->WindowState = System::Windows::Forms::FormWindowState::Minimized;
            }
          }
        }
      }
    }

    System::Void MainForm::mainControl1_AboutForm(System::Object^  sender, System::EventArgs^  e) {
      aboutfrm = gcnew Els_kom::Forms::AboutForm();
      aboutfrm->ShowDialog();
    }

    System::Void MainForm::mainControl1_ConfigForm(System::Object^  sender, System::EventArgs^  e) {
      settingsfrm = gcnew Els_kom::Forms::SettingsForm();
      settingsfrm->ShowDialog();
    }

    System::Void MainForm::mainControl1_ConfigForm2(System::Object^  sender, System::EventArgs^  e) {
      Els_kom_Core::Classes::MessageManager::ShowInfo(L"Welcome to Els_kom." + System::Environment::NewLine + L"Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System::Environment::NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
      settingsfrm = gcnew Els_kom::Forms::SettingsForm();
      settingsfrm->ShowDialog();
    }
  }
}
