#include "LoadResources.hpp"
#include "resource.hpp"
#include "Els_kom_Main_SettingsForm.hpp"

using namespace LoadResources;

namespace Els_kom {
  namespace Forms {
    SettingsForm::SettingsForm(void)
    {
      InitializeComponent();
      //
      //TODO: Add the constructor code here
      //
    }

    SettingsForm::~SettingsForm()
    {
      if (components)
      {
        delete components;
      }
    }

    System::Void SettingsForm::SettingsForm_Load(System::Object^  sender, System::EventArgs^  e) {
      this->Icon = GetIconResource(IDI_MAINICON);
    }

    System::Void SettingsForm::SettingsForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) {
      this->settingsControl1->SaveSettings();
    }

    System::Void SettingsForm::settingsControl1_CloseForm(System::Object^  sender, System::EventArgs^  e) {
      this->Close();
    }
  }
}
