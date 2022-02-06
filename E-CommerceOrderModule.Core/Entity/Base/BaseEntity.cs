using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_CommerceOrderModule.Core.Entity.ModelEnums;

namespace E_CommerceOrderModule.Core.Entity.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Status = Status.NewRecord;
            UpdateDate = DateTime.Now;
            UploadDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        private Status _status = Status.Active;
        public virtual Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        private DateTime? _uploadDate = null;
        public virtual DateTime UploadDate
        {
            get
            {
                return _uploadDate.HasValue ? Convert.ToDateTime(_uploadDate.Value) : Convert.ToDateTime(DateTime.Now);
            }
            set
            {
                _uploadDate = Convert.ToDateTime(value);
            }
        }

        private DateTime? _updateDate = null;
        public virtual DateTime UpdateDate
        {
            get
            {
                return _updateDate.HasValue ? Convert.ToDateTime(_updateDate.Value) : Convert.ToDateTime(DateTime.Now);
            }
            set
            {
                _updateDate = Convert.ToDateTime(value);
            }
        }
    }
}
