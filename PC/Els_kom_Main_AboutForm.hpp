#pragma once

namespace Els_kom {
  namespace Forms {

    /// <summary>
    /// Summary for Els_kom_Main_AboutForm
    /// </summary>
    public ref class AboutForm : public System::Windows::Forms::Form
    {
    public:
      AboutForm(void);

    protected:
      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      ~AboutForm();
    private: Els_kom_Core::Controls::AboutControl^  aboutControl1;
    public: static System::Windows::Forms::Label^ Label1;
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
        this->Label1 = (gcnew System::Windows::Forms::Label());
        this->aboutControl1 = (gcnew Els_kom_Core::Controls::AboutControl());
        this->SuspendLayout();
        // 
        // Label1
        // 
        this->Label1->AutoSize = true;
        this->Label1->Location = System::Drawing::Point(137, 201);
        this->Label1->Name = L"Label1";
        this->Label1->Size = System::Drawing::Size(13, 14);
        this->Label1->TabIndex = 12;
        this->Label1->Text = L"0";
        this->Label1->Visible = false;
        // 
        // aboutControl1
        // 
        this->aboutControl1->Location = System::Drawing::Point(0, 0);
        this->aboutControl1->Name = L"aboutControl1";
        this->aboutControl1->Size = System::Drawing::Size(382, 220);
        this->aboutControl1->TabIndex = 13;
        this->aboutControl1->CloseForm += gcnew System::EventHandler(this, &AboutForm::aboutControl1_CloseForm);
        // 
        // AboutForm
        // 
        this->AutoScaleDimensions = System::Drawing::SizeF(6, 14);
        this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
        this->AcceptButton = this->aboutControl1->cmdOK;
        this->BackColor = System::Drawing::SystemColors::Control;
        this->ClientSize = System::Drawing::Size(382, 227);
        this->CancelButton = this->aboutControl1->cmdOK;
        this->Controls->Add(this->Label1);
        this->Controls->Add(this->aboutControl1);
        this->Cursor = System::Windows::Forms::Cursors::Default;
        this->Font = (gcnew System::Drawing::Font(L"Arial", 8, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
          static_cast<System::Byte>(0)));
        this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
        this->Location = System::Drawing::Point(3, 25);
        this->MaximizeBox = false;
        this->MinimizeBox = false;
        this->Name = L"AboutForm";
        this->RightToLeft = System::Windows::Forms::RightToLeft::No;
        this->ShowInTaskbar = false;
        this->StartPosition = System::Windows::Forms::FormStartPosition::CenterParent;
        this->Text = L"About";
        this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &AboutForm::AboutForm_FormClosing);
        this->Load += gcnew System::EventHandler(this, &AboutForm::AboutForm_Load);
        this->ResumeLayout(false);
        this->PerformLayout();

      }
#pragma endregion
    private: System::Void AboutForm_Load(System::Object^  sender, System::EventArgs^  e);
    private: System::Void AboutForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e);
    private: System::Void aboutControl1_CloseForm(System::Object^  sender, System::EventArgs^  e);
    };
  }
}
