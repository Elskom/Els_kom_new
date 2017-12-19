#include "LoadResources.hpp"
#include "resource.hpp"
#include "Els_kom_Main_AboutForm.hpp"

using namespace LoadResources;

namespace Els_kom {
  namespace Forms {
    AboutForm::AboutForm(void)
    {
      InitializeComponent();
      //
      //TODO: Add the constructor code here
      //
    }

    AboutForm::~AboutForm()
    {
      if (components)
      {
        delete components;
      }
    }

    System::Void AboutForm::AboutForm_Load(System::Object^  sender, System::EventArgs^  e) {
      this->Label1->Text = "1";
      this->Icon = GetIconResource(IDI_MAINICON);
      this->aboutControl1->picIcon->Image = Get48x48IconResource(IDI_MAINICON)->ToBitmap();
      this->aboutControl1->Picture1->Image = GetImageResource(IDR_RCDATA1, 10);
      this->aboutControl1->Picture2->Image = GetImageResource(IDR_RCDATA2, 10);
    }

    System::Void AboutForm::AboutForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) {
      this->Label1->Text = "0";
    }

    System::Void AboutForm::aboutControl1_CloseForm(System::Object^  sender, System::EventArgs^  e) {
      this->Close();
    }
  }
}
