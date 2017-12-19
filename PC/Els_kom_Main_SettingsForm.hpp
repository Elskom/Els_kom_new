#pragma once

namespace Els_kom {
  namespace Forms {

    /// <summary>
    /// Summary for SettingsForm
    /// </summary>
    public ref class SettingsForm : public System::Windows::Forms::Form
    {
    public:
      SettingsForm(void);

    protected:
      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      ~SettingsForm();
    private: Els_kom_Core::Controls::SettingsControl^  settingsControl1;
    protected:

    private:
      /// <summary>
      /// Required designer variable.
      /// </summary>
      System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      void InitializeComponent(void)
      {
        this->settingsControl1 = (gcnew Els_kom_Core::Controls::SettingsControl());
        this->SuspendLayout();
        // 
        // settingsControl1
        // 
        this->settingsControl1->Location = System::Drawing::Point(12, 10);
        this->settingsControl1->Name = L"settingsControl1";
        this->settingsControl1->Size = System::Drawing::Size(510, 104);
        this->settingsControl1->TabIndex = 10;
        this->settingsControl1->CloseForm += gcnew System::EventHandler(this, &SettingsForm::settingsControl1_CloseForm);
        // 
        // SettingsForm
        // 
        this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
        this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
        this->ClientSize = System::Drawing::Size(531, 124);
        this->Controls->Add(this->settingsControl1);
        this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
        this->HelpButton = true;
        this->MaximizeBox = false;
        this->MinimizeBox = false;
        this->Name = L"SettingsForm";
        this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
        this->Text = L"Settings";
        this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &SettingsForm::SettingsForm_FormClosing);
        this->Load += gcnew System::EventHandler(this, &SettingsForm::SettingsForm_Load);
        this->ResumeLayout(false);
      }
#pragma endregion
    private: System::Void SettingsForm_Load(System::Object^  sender, System::EventArgs^  e);
    private: System::Void SettingsForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e);
    private: System::Void settingsControl1_CloseForm(System::Object^  sender, System::EventArgs^  e);
    };
  }
}
