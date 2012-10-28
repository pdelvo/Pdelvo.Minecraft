using System;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class RemoteInterfaceAbortedEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteInterfaceAbortedEventArgs"/> class.
        /// </summary>
        /// <remarks></remarks>
        public RemoteInterfaceAbortedEventArgs()
        {
            CancelReason = RemoteInterfaceAbortReason.ThreadAborted;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteInterfaceAbortedEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <remarks></remarks>
        public RemoteInterfaceAbortedEventArgs(Exception ex)
        {
            CancelReason = RemoteInterfaceAbortReason.Exception;
            Exception = ex;
        }

        /// <summary>
        /// Gets the cancel reason.
        /// </summary>
        /// <remarks></remarks>
        public RemoteInterfaceAbortReason CancelReason { get; private set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        /// <remarks></remarks>
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public enum RemoteInterfaceAbortReason
    {
        /// <summary>
        /// 
        /// </summary>
        ThreadAborted,
        /// <summary>
        /// 
        /// </summary>
        Exception
    }
}