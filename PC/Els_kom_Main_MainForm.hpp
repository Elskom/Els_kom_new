#pragma once

namespace Els_kom {
  namespace Forms {

    /// <summary>
    /// Summary for MainForm
    /// </summary>
    public ref class MainForm : public System::Windows::Forms::Form
    {
    public:
      MainForm(void);

    protected:
      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      ~MainForm();
    private: Els_kom_Core::Controls::MainControl^  mainControl1;
    protected:
    private: System::Windows::Forms::Form^ aboutfrm;
    private: System::Windows::Forms::Form^ settingsfrm;

    /* Allows the user to enable or disable their event
     * handler at will if they only want it to sometimes
     * fire. */
    private: bool Enablehandlers;

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
        this->mainControl1 = (gcnew Els_kom_Core::Controls::MainControl());
        this->SuspendLayout();
        // 
        // mainControl1
        // 
        this->mainControl1->Location = System::Drawing::Point(0, 0);
        this->mainControl1->Name = L"mainControl1";
        this->mainControl1->Size = System::Drawing::Size(304, 146);
        this->mainControl1->TabIndex = 8;
        this->mainControl1->MinimizeForm += gcnew System::EventHandler(this, &MainForm::mainControl1_MinimizeForm);
        this->mainControl1->CloseForm += gcnew System::EventHandler(this, &MainForm::mainControl1_CloseForm);
        this->mainControl1->TrayNameChange += gcnew System::EventHandler(this, &MainForm::mainControl1_TrayNameChange);
        this->mainControl1->TaskbarShow += gcnew System::EventHandler<Els_kom_Core::Classes::ShowTaskbarEvent^ >(this, &MainForm::mainControl1_TaskbarShow);
        this->mainControl1->TrayClick += gcnew System::EventHandler<System::Windows::Forms::MouseEventArgs^ >(this, &MainForm::mainControl1_TrayClick);
        this->mainControl1->ConfigForm += gcnew System::EventHandler(this, &MainForm::mainControl1_ConfigForm);
        this->mainControl1->ConfigForm2 += gcnew System::EventHandler(this, &MainForm::mainControl1_ConfigForm2);
        this->mainControl1->AboutForm += gcnew System::EventHandler(this, &MainForm::mainControl1_AboutForm);
        // 
        // MainForm
        // 
        this->AutoScaleDimensions = System::Drawing::SizeF(6, 14);
        this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
        this->BackColor = System::Drawing::SystemColors::Control;
        this->ClientSize = System::Drawing::Size(304, 146);
        this->Controls->Add(this->mainControl1);
        this->Cursor = System::Windows::Forms::Cursors::Default;
        this->Font = (gcnew System::Drawing::Font(L"Arial", 8, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
          static_cast<System::Byte>(0)));
        this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
        this->Location = System::Drawing::Point(3, 23);
        this->MaximizeBox = false;
        this->Name = L"MainForm";
        this->RightToLeft = System::Windows::Forms::RightToLeft::No;
        this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
        this->Text = L"Els_kom v1.4.9.8";
        this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &MainForm::MainForm_FormClosing);
        this->Load += gcnew System::EventHandler(this, &MainForm::MainForm_Load);
        this->MouseLeave += gcnew System::EventHandler(this, &MainForm::MainForm_MouseLeave);
        this->ResumeLayout(false);

      }
#pragma endregion
    protected:
    [System::Security::Permissions::SecurityPermission(System::Security::Permissions::SecurityAction::Demand, Flags = System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode)]
    virtual void WndProc(System::Windows::Forms::Message% m) override;
    private: System::Void MainForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e);
    private: System::Void MainForm_Load(System::Object^  sender, System::EventArgs^  e);
    private: System::Void MainForm_MouseLeave(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_CloseForm(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_MinimizeForm(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_TaskbarShow(System::Object^  sender, Els_kom_Core::Classes::ShowTaskbarEvent^  e);
    private: System::Void mainControl1_TrayNameChange(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_TrayClick(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e);
    private: System::Void mainControl1_AboutForm(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_ConfigForm(System::Object^  sender, System::EventArgs^  e);
    private: System::Void mainControl1_ConfigForm2(System::Object^  sender, System::EventArgs^  e);
    };
  }
}
