using System;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    internal sealed class RequireVersionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly bool _throwException;
        /// <summary>
        /// 
        /// </summary>
        private readonly int _versionNumber;

        // This is a positional argument
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireVersionAttribute"/> class.
        /// </summary>
        /// <param name="versionNumber">The version number.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <remarks></remarks>
        public RequireVersionAttribute(int versionNumber, bool throwException = false)
        {
            _versionNumber = versionNumber;
            _throwException = throwException;
        }

        /// <summary>
        /// Gets the version number.
        /// </summary>
        /// <remarks></remarks>
        public int VersionNumber
        {
            get { return _versionNumber; }
        }

        /// <summary>
        /// Gets a value indicating whether [throw exception].
        /// </summary>
        /// <remarks></remarks>
        public bool ThrowException
        {
            get { return _throwException; }
        }
    }
}