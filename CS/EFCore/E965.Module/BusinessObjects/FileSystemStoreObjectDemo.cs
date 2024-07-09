using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using FileSystemData.BusinessObjects;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileSystemStoreObjectDemo : BaseObject {
        [ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public virtual FileSystemStoreObject File { get; set; }

        public override void OnSaving() {
            base.OnSaving();
            if (ObjectSpace.IsObjectToDelete(this) && File != null) {
                ObjectSpace.Delete(File);
                File.Clear();
            }
        }
    }
}
