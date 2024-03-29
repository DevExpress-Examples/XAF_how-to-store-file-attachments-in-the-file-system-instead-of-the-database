﻿using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class StandardFileDataDemo : BaseObject {
        public StandardFileDataDemo(Session session) : base(session) { }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData File {
            get { return GetPropertyValue<FileData>("File"); }
            set { SetPropertyValue<FileData>("File", value); }
        }
    }
}
