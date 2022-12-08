using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class StandardFileDataDemo : BaseObject {
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public virtual FileData File { get; set; }
    }
}
