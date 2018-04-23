using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using FileSystemData.BusinessObjects;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileSystemStoreObjectDemo : BaseObject {
        public FileSystemStoreObjectDemo(Session session) : base(session) { }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public FileSystemStoreObject File {
            get { return GetPropertyValue<FileSystemStoreObject>("File"); }
            set { SetPropertyValue<FileSystemStoreObject>("File", value); }
        }
    }
}
