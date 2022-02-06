using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Common
{
    [DataContract(Name = "Result_{0}")]
    [Serializable]
    public class Result<T> : Result
    {
        #region Constructor

        public Result()
        {
        }

        public Result(bool resultStatus)
        {
            if (resultStatus)
            {
                SetTrue();
            }
            else
            {
                SetFalse();
            }

            ResultStatus = resultStatus;
        }

        #endregion

        [DataMember]
        public T ResultObject { get; set; }
    }

    [Serializable]
    [DataContract(Name = "Result")]
    public class Result
    {
        #region Global Variables
        /// <summary>
        /// Result message
        /// </summary>
        [DataMember]
        public string ResultMessage { get; set; }
        /// <summary>
        /// Result code 
        /// </summary>
        [DataMember]
        public short ResultCode { get; set; }
        /// <summary>
        /// Result status
        /// </summary>
        [DataMember]
        public bool ResultStatus { get; set; }
        /// <summary>
        /// Result inner message. Use this variable to set exception messages
        /// </summary>
        [DataMember]
        public string ResultInnerMessage { get; set; }


        /// <summary>
        ///Result returnurl
        /// </summary>
        [DataMember]
        public string ReturnUrl { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Result default constructor
        /// </summary>
        public Result()
        {

        }

        public Result(bool isStatus)
        {
            if (isStatus)
            {
                SetTrue();
            }
            else
            {
                SetFalse();
            }

        }

        #endregion

        /// <summary>
        /// Set result object to the successfull state with default values
        /// </summary>
        public void SetTrue()
        {
            SetTrue(StaticValue._defaultSuccessCode, StaticValue._defaultSuccessMessage);
        }

        /// <summary>
        /// Set result object to the successfull state with given result message
        /// </summary>
        /// <param name="resultMessage">Result successfull message</param>
        public void SetTrue(string resultMessage)
        {
            SetTrue(StaticValue._defaultSuccessCode, resultMessage);
        }

        /// <summary>
        /// Set result object to the successfull state with given result code
        /// </summary>
        /// <param name="resultCode">Result successfull code</param>
        public void SetTrue(short resultCode)
        {
            SetTrue(resultCode, StaticValue._defaultSuccessMessage);
        }

        /// <summary>
        /// Set result object to successfull state with given result code and result message
        /// </summary>
        /// <param name="resultCode">Result successfull code</param>
        /// <param name="resultMessage">Result successfull message</param>
        public void SetTrue(short resultCode, string resultMessage)
        {
            ResultStatus = true;
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }


        /// <summary>
        /// Set result object to the failed state with default values
        /// </summary>
        public void SetFalse()
        {
            SetFalse(StaticValue._defaultErrorCode, StaticValue._defaultErrorMessage);
        }

        /// <summary>
        /// Set result object to the failed state with given result message
        /// </summary>
        /// <param name="resultMessage">Result failed message</param>
        public void SetFalse(string resultMessage)
        {
            SetFalse(StaticValue._defaultErrorCode, resultMessage);
        }

        /// <summary>
        /// Set result object to the failed state with given result code
        /// </summary>
        /// <param name="resultCode">Result failed code</param>
        public void SetFalse(short resultCode)
        {
            SetFalse(resultCode, StaticValue._defaultErrorMessage);
        }

        /// <summary>
        /// Set result object to failed state with given result code and result message
        /// </summary>
        /// <param name="resultCode">Result failed code</param>
        /// <param name="resultMessage">Result failed message</param>
        public void SetFalse(short resultCode, string resultMessage)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
            ResultStatus = false;
        }

        public void SetFalse(short resultCode, string resultMessage, string returnUrl)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
            ResultStatus = false;
            ReturnUrl = returnUrl;
        }
    }
}
