namespace Intense.Text.Parsers
{
    /// <summary>
    /// Represents a single token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Represents the token that marks the end of the input.
        /// </summary>
        public static readonly Token End = new Token(string.Empty, Lexer.TokenEnd);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Token"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tokenType">Type of the token.</param>
        public Token(string value, int tokenType)
        {
            this.Value = value;
            this.TokenType = tokenType;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; internal set; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>The type.</value>
        public int TokenType { get; internal set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.TokenType + ": " + this.Value;
        }
    }
}

