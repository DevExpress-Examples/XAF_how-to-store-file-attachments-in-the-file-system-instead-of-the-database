namespace E965.Win {
    partial class E965WindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
			this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
			this.module3 = new E965.Module.E965Module();
			this.module5 = new DevExpress.ExpressApp.Validation.ValidationModule();
			this.module6 = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
			this.module7 = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
			this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.fileAttachmentsWindowsFormsModule1 = new DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule();
			this.fileSystemDataModule1 = new FileSystemData.FileSystemDataModule();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// module5
			// 
			this.module5.AllowValidationDetailsAccess = true;
			this.module5.IgnoreWarningAndInformationRules = false;
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=E965;Integrated Security=SSPI;Pooling=false";
			this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
			// 
			// E965WindowsFormsApplication
			// 
			this.ApplicationName = "E965";
			this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
			this.Connection = this.sqlConnection1;
			this.Modules.Add(this.module1);
			this.Modules.Add(this.module2);
			this.Modules.Add(this.module6);
			this.Modules.Add(this.securityModule1);
			this.Modules.Add(this.fileSystemDataModule1);
			this.Modules.Add(this.module3);
			this.Modules.Add(this.module5);
			this.Modules.Add(this.module7);
			this.Modules.Add(this.fileAttachmentsWindowsFormsModule1);
			this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.E965WindowsFormsApplication_DatabaseVersionMismatch);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private E965.Module.E965Module module3;
        private DevExpress.ExpressApp.Validation.ValidationModule module5;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule module6;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule module7;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule fileAttachmentsWindowsFormsModule1;
        private FileSystemData.FileSystemDataModule fileSystemDataModule1;
    }
}
