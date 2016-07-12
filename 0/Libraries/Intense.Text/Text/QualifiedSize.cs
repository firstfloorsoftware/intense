using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;

namespace Intense.Text
{
    /// <summary>
    /// Defines a qualified double size.
    /// </summary>
    public struct QualifiedSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualifiedSize"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="qualifier"></param>
        public QualifiedSize(double value, Qualifier qualifier = Qualifier.Pixel)
        {
            this.Value = value;
            this.Qualifier = qualifier;
        }

        /// <summary>
        /// Gets the double value.
        /// </summary>
        public double Value { get; private set; }
        /// <summary>
        /// Gets or sets the qualifier.
        /// </summary>
        public Qualifier Qualifier { get; private set; }

        /// <summary>
        /// Returns the value in pixels.
        /// </summary>
        /// <returns></returns>
        public double ToPixelSize()
        {
            var dpi = 96d;

            if (this.Qualifier == Qualifier.Centimeter) {
                return dpi / 2.54 * this.Value;
            }
            if (this.Qualifier == Qualifier.Inch) {
                return dpi * this.Value;
            }
            if (this.Qualifier == Qualifier.Pixel) {
                return this.Value;
            }
            if (this.Qualifier == Qualifier.Point) {
                return dpi / 72 * this.Value;
            }

            throw new NotSupportedException();   
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var value = this.Value.ToString(CultureInfo.InvariantCulture);
            var q = "px";

            if (this.Qualifier == Qualifier.Centimeter) q = "cm";
            else if (this.Qualifier == Qualifier.Inch) q = "in";
            else if (this.Qualifier == Qualifier.Point) q = "pt";

            return value + q;
        }

        /// <summary>
        /// Retrieves the zero qualified size.
        /// </summary>
        public static QualifiedSize Zero
        {
            get { return new QualifiedSize(); }
        }

        /// <summary>
        /// Attempts to parse a qualified size from given string value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string value, out QualifiedSize result)
        {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            result = new QualifiedSize();

            value = value.Trim();
            if (value.Length == 0) {
                return false;
            }

            var i = 0; 
            while(char.IsDigit(value, i) || value[i] == '.') {
                i++;
            }

            double dblVal;
            if (!double.TryParse(value.Substring(0, i), out dblVal)) {
                return false;
            }

            Qualifier qVal;
            var q = value.Substring(i).Trim();
            if (q.Length == 0 || q.Equals("px", StringComparison.OrdinalIgnoreCase)) {
                qVal = Qualifier.Pixel; 
            }
            else if (q.Equals("cm", StringComparison.OrdinalIgnoreCase)) {
                qVal = Qualifier.Centimeter;
            }
            else if (q.Equals("in", StringComparison.OrdinalIgnoreCase)) {
                qVal = Qualifier.Inch;
            }
            else if (q.Equals("pt", StringComparison.OrdinalIgnoreCase)) {
                qVal = Qualifier.Point;
            }
            else {
                return false;
            }

            result = new QualifiedSize(dblVal, qVal);
            return true;
        }
    }
}
