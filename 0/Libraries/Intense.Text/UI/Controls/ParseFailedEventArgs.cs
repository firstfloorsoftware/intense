using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.UI.Controls
{
    /// <summary>
    /// Provides data for parse failed events.
    /// </summary>
    public class ParseFailedEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseFailedEventArgs"/> class.
        /// </summary>
        /// <param name="error"></param>
        public ParseFailedEventArgs(Exception error)
        {
            if (error == null) {
                throw new ArgumentNullException("error");
            }
            this.Error = error;
        }

        /// <summary>
        /// Gets the exception details of the parse failure.
        /// </summary>
        public Exception Error { get; private set; }
    }
}
