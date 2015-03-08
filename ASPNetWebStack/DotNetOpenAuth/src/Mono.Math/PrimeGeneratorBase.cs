// <auto-generated />
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace Mono.Math.Prime.Generator {

	//[CLSCompliant(false)]
	internal abstract class PrimeGeneratorBase {

		public virtual ConfidenceFactor Confidence {
			get {
				return ConfidenceFactor.Medium;
			}
		}

		public virtual Prime.PrimalityTest PrimalityTest {
			get {
				return new Prime.PrimalityTest (PrimalityTests.SmallPrimeSppTest);
			}
		}

		public virtual int TrialDivisionBounds {
			get { return 4000; }
		}

		/// <summary>
		/// Performs primality tests on bi, assumes trial division has been done.
		/// </summary>
		/// <param name="bi">A BigInteger that has been subjected to and passed trial division</param>
		/// <returns>False if bi is composite, true if it may be prime.</returns>
		/// <remarks>The speed of this method is dependent on Confidence</remarks>
		protected bool PostTrialDivisionTests (BigInteger bi)
		{
			return PrimalityTest (bi, this.Confidence);
		}

		public abstract BigInteger GenerateNewPrime (int bits);
	}
}
