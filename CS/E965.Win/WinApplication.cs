using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.ExpressApp.Xpo;

namespace E965.Win {
    public partial class E965WindowsFormsApplication : WinApplication {
        public E965WindowsFormsApplication() {
			InitializeComponent();
			SplashScreen = new DXSplashScreen(typeof(XafSplashScreen), new DefaultOverlayFormOptions());
            DelayedViewItemsInitialization = true;
            // To open the linked file without creating a temporary one (and keep any changes to it after closing the program), handle the FileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram event and call the e.FileData.SaveToStream method and pass null (nothing in VB.NET) as a parameter.
            fileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram += new EventHandler<DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs>(fileAttachmentsWindowsFormsModule_CustomOpenFileWithDefaultProgram);
        }
        private void fileAttachmentsWindowsFormsModule_CustomOpenFileWithDefaultProgram(object sender, DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs e) {
            if((e.FileData is FileSystemData.BusinessObjects.FileSystemLinkObject)
                || (e.FileData is FileSystemData.BusinessObjects.FileSystemStoreObject)) {
                e.FileData.SaveToStream(null);
                e.Handled = true;
            }
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, true), false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void E965WindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
                e.Languages.Add(userLanguageName);
            }
        }
        private void E965WindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

				if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
					message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
				}
				throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
