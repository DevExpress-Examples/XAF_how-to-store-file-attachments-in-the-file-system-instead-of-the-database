using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;

namespace E965.Win {
    public partial class E965WindowsFormsApplication : WinApplication {
        public E965WindowsFormsApplication() {
            InitializeComponent();
            DelayedViewItemsInitialization = true;
            /// To open the linked file without creating a temporary one (and keep any changes to it after closing the program), handle the FileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram event and call the e.FileData.SaveToStream method and pass null (nothing in VB.NET) as a parameter.
            fileAttachmentsWindowsFormsModule1.CustomOpenFileWithDefaultProgram += new EventHandler<DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs>(fileAttachmentsWindowsFormsModule1_CustomOpenFileWithDefaultProgram);
        }
        private void fileAttachmentsWindowsFormsModule1_CustomOpenFileWithDefaultProgram(object sender, DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs e) {
            if ((e.FileData is FileSystemData.BusinessObjects.FileSystemLinkObject)
                || (e.FileData is FileSystemData.BusinessObjects.FileSystemStoreObject)) {
                e.FileData.SaveToStream(null);
                e.Handled = true;
            }
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, true);
        }
        private void E965WindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
			e.Updater.Update();
			e.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " +
                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
            }
#endif
        }
    }
}
