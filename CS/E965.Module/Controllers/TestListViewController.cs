using System;
using System.IO;
using System.Linq;
using FileSystemData;
using DevExpress.ExpressApp;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using FileSystemData.BusinessObjects;
using DevExpress.Persistent.Validation;

namespace E965.Module.Controllers {
    public partial class TestListViewController : ViewController {
        private const int MaxTestersCount = 10;
        private const string STR_Testfile = "Test.file";
        private SimpleAction testAction;
        public TestListViewController() {
            testAction = new SimpleAction(this, "Test", "View");
            TargetViewType = ViewType.ListView;
            testAction.Execute += testAction_Execute;
        }
        void testAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            testAction.Caption = "Test is running...";
            Task[] tasks = new Task[MaxTestersCount];
            for (int i = 0; i < MaxTestersCount; i++) {
                tasks[i] = Task.Factory.StartNew(() => {
                    for (int j = 0; j < MaxTestersCount; j++) {
                        Stream testStream = GetType().Assembly.GetManifestResourceStream("E965.Module.Controllers.Test.file");
                        using (IObjectSpace os = Application.CreateObjectSpace(typeof(FileSystemStoreObject))) {
                            try {
                                IFileData theObject = os.CreateObject<FileSystemStoreObject>();
                                if (!((IEmptyCheckable)theObject).IsEmpty)
                                    throw new Exception("Must be empty");
                                theObject.LoadFromStream(STR_Testfile, testStream);
                                if (theObject.FileName != STR_Testfile)
                                    throw new Exception("Must have file name");
                                os.CommitChanges();
                                if (theObject.Size != 44)
                                    throw new Exception("Must have size 44");
                                if (((IEmptyCheckable)theObject).IsEmpty)
                                    throw new Exception("Must be non-empty");

                            } catch {
                                os.Rollback();
                                throw;
                            }
                        }
                    }
                }
                );
            }
            try {
                Task.WaitAll(tasks);
                View.ObjectSpace.Refresh();
                testAction.Caption = "Succeeded";
            } catch (Exception ex) {
                testAction.Caption = "Failed";
                Tracing.Tracer.LogError(ex);
            }
            testAction.Enabled["Run only once"] = false;
        }
    }
}