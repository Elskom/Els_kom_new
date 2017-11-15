#pragma once
#include "LoadResources.h"
#include "resource.h"
#include "Els_kom_Main_AboutForm.h"
#include "Els_kom_Main_SettingsForm.h"

namespace Els_kom {
	namespace Forms {

		using namespace LoadResources;
		using namespace System;
		using namespace System::ComponentModel;
		using namespace System::Collections;
		using namespace System::Windows::Forms;
		using namespace System::Data;
		using namespace System::Drawing;
		using namespace System::Security::Permissions;

		/// <summary>
		/// Summary for MainForm
		/// </summary>
		public ref class MainForm : public System::Windows::Forms::Form
		{
		public:
			MainForm(void)
			{
				InitializeComponent();
				//
				//TODO: Add the constructor code here
				//
			}

		protected:
			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			~MainForm()
			{
				if (components)
				{
					delete components;
				}
			}
		private: Els_kom_Core::Controls::MainControl^  mainControl1;
		protected:
		public: Form^ aboutfrm;
		public: Form^ settingsfrm;

		/* Allows the user to enable or disable their event
		 * handler at will if they only want it to sometimes
		 * fire. */
		public: bool Enablehandlers;
		private: const int WM_SYSCOMMAND = 0x0112;
		private: const int SC_MAXIMIZE = 0xf030;
		private: const int SC_MINIMIZE = 0xf020;
		private: const int SC_RESTORE = 0xf120;

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
		[SecurityPermission(SecurityAction::Demand, Flags = SecurityPermissionFlag::UnmanagedCode)]
		virtual void WndProc(Message% m) override {
			if (Enablehandlers && m.Msg == WM_SYSCOMMAND)
			{
				if (m.WParam.ToInt32() == SC_MINIMIZE)
				{
					this->Hide();
					m.Result = IntPtr::Zero;
					return;
				}
				else if (m.WParam.ToInt32() == SC_MAXIMIZE)
				{
					this->Show();
					this->Activate();
					m.Result = IntPtr::Zero;
					return;
				}
				else if (m.WParam.ToInt32() == SC_RESTORE)
				{
					this->Show();
					this->Activate();
					m.Result = IntPtr::Zero;
					return;
				}
			}
			Form::WndProc(m);
		}

		private: System::Void MainForm_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) {
			bool Cancel = e->Cancel;
			// CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
			e->Cancel = Cancel;
		}

		private: System::Void MainForm_Load(System::Object^  sender, System::EventArgs^  e) {
			this->Hide();
			this->mainControl1->NotifyIcon1->Visible = false;
			this->ShowInTaskbar = false;
			bool previnstance;
			this->Icon = GetIconResource(IDI_MAINICON);
			this->mainControl1->NotifyIcon1->Icon = this->Icon;
			this->mainControl1->NotifyIcon1->Text = this->Text;
			previnstance = Els_kom_Core::Classes::Process::IsElsKomRunning();
			if (Els_kom_Core::Classes::Version::version != L"1.4.9.8") {
				MessageBox::Show(L"Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", L"Error!", MessageBoxButtons::OK, MessageBoxIcon::Error);
				this->Close();
			}
			if (previnstance == true)
			{
				MessageBox::Show(L"Sorry, Only 1 Instance is allowed at a time.", L"Error!", MessageBoxButtons::OK, MessageBoxIcon::Error);
				this->Close();
			}
			else
			{
				this->mainControl1->LoadControl();
				this->Show();
			}
		}

		private: System::Void MainForm_MouseLeave(System::Object^  sender, System::EventArgs^  e) {
			this->mainControl1->Label1->Text = L"";
		}

		private: System::Void mainControl1_CloseForm(System::Object^  sender, System::EventArgs^  e) {
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

		private: System::Void mainControl1_MinimizeForm(System::Object^  sender, System::EventArgs^  e) {
			if (!this->ShowInTaskbar)
			{
				this->Hide();
			}
			this->WindowState = FormWindowState::Minimized;
		}

		private: System::Void mainControl1_TaskbarShow(System::Object^  sender, Els_kom_Core::Classes::ShowTaskbarEvent^  e) {
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

		private: System::Void mainControl1_TrayNameChange(System::Object^  sender, System::EventArgs^  e) {
			this->mainControl1->NotifyIcon1->Text = this->Text;
		}

		private: System::Void mainControl1_TrayClick(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {
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
						if (this->WindowState == FormWindowState::Minimized)
						{
							this->WindowState = FormWindowState::Normal;
							this->Activate();
						}
						else
						{
							this->WindowState = FormWindowState::Minimized;
						}
					}
					else if (this->mainControl1->NotifyIcon1->Visible == true)
					{
						if (this->WindowState == FormWindowState::Minimized)
						{
							this->WindowState = FormWindowState::Normal;
							this->Show();
						}
						else
						{
							this->Hide();
							this->WindowState = FormWindowState::Minimized;
						}
					}
				}
			}
		}

		private: System::Void mainControl1_AboutForm(System::Object^  sender, System::EventArgs^  e) {
			aboutfrm = gcnew Els_kom::Forms::AboutForm();
			aboutfrm->ShowDialog();
		}

		private: System::Void mainControl1_ConfigForm(System::Object^  sender, System::EventArgs^  e) {
			settingsfrm = gcnew Els_kom::Forms::SettingsForm();
			settingsfrm->ShowDialog();
		}

		private: System::Void mainControl1_ConfigForm2(System::Object^  sender, System::EventArgs^  e) {
			MessageBox::Show(L"Welcome to Els_kom." + System::Environment::NewLine + L"Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System::Environment::NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", MessageBoxButtons::OK, MessageBoxIcon::Information);
			settingsfrm = gcnew Els_kom::Forms::SettingsForm();
			settingsfrm->ShowDialog();
		}
		};
	}
}
